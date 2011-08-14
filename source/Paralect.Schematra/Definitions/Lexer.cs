using System;
using System.IO;
using Irony.Parsing;
using Paralect.Schematra.Definitions;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra.Definitions
{
    public class Lexer
    {
        /// <summary>
        /// Current node where parsing now located
        /// </summary>
        private ParseTreeNode _currentNode;

        /// <summary>
        /// Compile specified files
        /// </summary>
        public CompilationDefinition BuildCompilationDefinition(String[] filePaths)
        {
            if (filePaths.Length <= 0)
                throw new SchematraException("Files were not specified");

            foreach (var filePath in filePaths)
            {
                var tree = BuildParseTree(filePath);

                try
                {
                    var compilation = new CompilationDefinition();

                    ParseUnit(tree, compilation);

                    return compilation;
                }
                catch (Exception ex)
                {
                    var errorMessage = "Unaxpected exception while parsing schema file";

                    if (_currentNode != null)
                        errorMessage = BuildErrorMessage(ex.Message, filePath, _currentNode.Span.Location.Line + 1, _currentNode.Span.Location.Column);

                    throw new SchematraException(errorMessage);
                }
            }

            throw new SchematraException("Files were not specified");
        }

        /// <summary>
        /// Parsing of unit nonterm
        /// </summary>
        private void ParseUnit(ParseTree tree, CompilationDefinition compilation)
        {
            var unitDefinition = new UnitDefinition();

            foreach (var child in tree.Root.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_defs:
                        ParseDefs(child, unitDefinition, "");
                        break;
                    case SchematraGrammer.term_using_defs:
                        ParseUsingDefs(child, unitDefinition);
                        break;
                }
            }

            compilation.Units.Add(unitDefinition);            
        }

        /// <summary>
        /// Parsing of unit-defs nonterm
        /// </summary>
        private void ParseUsingDefs(ParseTreeNode node, UnitDefinition unitDefinition)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_using_def:
                        ParseUsingDef(child, unitDefinition);
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of using-def nonterm
        /// </summary>
        private void ParseUsingDef(ParseTreeNode node, UnitDefinition unitDefinition)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_using_def_name:
                        unitDefinition.Usings.Add(child.Token.ValueString);
                        break;
                }
            }            
        }

        /// <summary>
        /// Parsing of defs nonterm
        /// </summary>
        private void ParseDefs(ParseTreeNode node, UnitDefinition unitDefinition, String space)
        {
            _currentNode = node;

            if (node.Term.Name != SchematraGrammer.term_defs)
                return;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_def:
                        ParseDef(child, unitDefinition, space);
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of def nonterm
        /// </summary>
        private void ParseDef(ParseTreeNode node, UnitDefinition unitDefinition, String space)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def:
                        ParseSchemaDef(child, unitDefinition, space);
                        break;
                    case SchematraGrammer.term_namespace_def:
                        ParseNamespaceDef(child, unitDefinition, space);
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of namespace-def nonterm
        /// </summary>
        private void ParseNamespaceDef(ParseTreeNode node, UnitDefinition unitDefinition, String space)
        {
            _currentNode = node;

            var namespaceText = String.Empty;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_namespace_def_name:
                        namespaceText = child.Token.ValueString;
                        break;
                    case SchematraGrammer.term_defs:
                        ParseDefs(child, unitDefinition, Utils.ConcatNamespaces(space, namespaceText));
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of schema-def nonterm
        /// </summary>
        private void ParseSchemaDef(ParseTreeNode node, UnitDefinition unitDefinition, String space)
        {
            _currentNode = node;

            var schemadef = new RecordDefinition();

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_name:
                        schemadef.Name = child.Token.Text;
                        schemadef.Namespace = space;
                        break;

                    case SchematraGrammer.term_schema_def_options:
                        ParseSchemaDefOptions(child, schemadef);
                        break;

                    case SchematraGrammer.term_schema_def_body:
                        ParseSchemaDefBody(child, schemadef);
                        break;
                }
            }

            unitDefinition.TypeDefinitions.Add(schemadef);
        }

        /// <summary>
        /// Parsing of schema-def-body nonterm
        /// </summary>
        private void ParseSchemaDefBody(ParseTreeNode node, RecordDefinition schemadef)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_field:
                        var fieldDefinition = new FieldDefinition();
                        ParseField(child, fieldDefinition);
                        schemadef.FieldDefinitions.Add(fieldDefinition);
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of field nonterm
        /// </summary>
        private void ParseField(ParseTreeNode node, FieldDefinition fieldDefinition)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                if (child.Term == null || child.Term.Name == null)
                    continue;

                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_field_index:
                        fieldDefinition.Index = Int32.Parse(child.Token.Value.ToString());
                        break;

                    case SchematraGrammer.term_field_qualifier:
                        fieldDefinition.SetQualifierByString(child.ChildNodes[0].Term.Name);
                        break;

                    case SchematraGrammer.term_field_nullable:
                        fieldDefinition.Nullable = child.ChildNodes.Count > 0;
                        break;

                    case SchematraGrammer.term_field_schema:
                        fieldDefinition.TypeName = child.Token.Value.ToString();
                        break;

                    case SchematraGrammer.term_field_name:
                        fieldDefinition.Name = child.Token.Value.ToString();
                        break;

                    case SchematraGrammer.term_field_init_value:
                        break;
                }
            }            
        }

        /// <summary>
        /// Parsing of schema-def-options nonterm
        /// </summary>
        private void ParseSchemaDefOptions(ParseTreeNode node, RecordDefinition schemadef)
        {
            _currentNode = node;

            if (node.ChildNodes.Count <= 0)
                return;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_option:
                        ParseSchemaDefOption(child, schemadef);
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of schema-def-option nonterm
        /// </summary>
        private void ParseSchemaDefOption(ParseTreeNode node, RecordDefinition schemadef)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_tagged_option:
                        ParseSchemaDefTaggedOption(child, schemadef);
                        break;
                    case SchematraGrammer.term_schema_def_extends_option:
                        ParseSchemaDefExtendsOption(child, schemadef);
                        break;
                }
            }            
        }

        /// <summary>
        /// Parsing of schema-def-extends-option nonterm
        /// </summary>
        private void ParseSchemaDefExtendsOption(ParseTreeNode node, RecordDefinition schemadef)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_extends:
                        schemadef.Extends = child.Token.ValueString;
                        break;
                }
            }
        }

        /// <summary>
        /// Parsing of schema-def-tagged-option nonterm
        /// </summary>
        private void ParseSchemaDefTaggedOption(ParseTreeNode node, RecordDefinition schemadef)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_tag:

                        var guidText = child.Token.ValueString;
                        schemadef.Tag = Guid.Parse(guidText);
                        break;
                }
            }
        }

        /// <summary>
        /// Builds error message that explains error and shows position in file with this error
        /// </summary>
        private String BuildErrorMessage(String message, String filePath, Int32 line, Int32 column)
        {
            var location = String.Format("({0},{1})", line, column);
            var errorMessage = message + " at " + Environment.NewLine + "\t" + filePath + location;

            return errorMessage;
        }

        /// <summary>
        /// Build ParseTree from the file
        /// </summary>
        public ParseTree BuildParseTree(String filePath)
        {
            var source = File.ReadAllText(filePath);
            return BuildParseTree(source, filePath);
        }

        /// <summary>
        /// Build Parse tree from the String
        /// </summary>
        private ParseTree BuildParseTree(String source, String filePath)
        {
            var grammer = new SchematraGrammer();
            var tree = grammer.ParseTree(source);

            if (tree.HasErrors() && tree.ParserMessages.Count > 0)
            {
                var message = tree.ParserMessages[0];
                var error = BuildErrorMessage(message.Message, filePath, message.Location.Line + 1, message.Location.Column);
                throw new Exception(error);
            }

            return tree;
        }
    }
}