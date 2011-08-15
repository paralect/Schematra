using System;
using System.Collections.Generic;
using System.IO;
using Irony.Parsing;
using Paralect.Schematra.Definitions;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra
{
    public class Context
    {
        /// <summary>
        /// Type context
        /// </summary>
        private TypeContext _typeContext { get; set; }

        /// <summary>
        /// List of usings
        /// </summary>
        private readonly List<String> _usings = new List<String>();

        /// <summary>
        /// Precached value of usings
        /// </summary>
        private String[] _usingsArray;

        /// <summary>
        /// Usings array
        /// </summary>
        public String[] Usings
        {
            get
            {
                if (_usingsArray == null)
                {
                    _usingsArray = _usings.ToArray();
                }

                return _usingsArray;
            }
        }

        /// <summary>
        /// Type context
        /// </summary>
        public TypeContext TypeContext
        {
            get { return _typeContext; }
        }

        public Context(TypeContext typeContext)
        {
            _typeContext = typeContext;
        }

        /// <summary>
        /// Add using type full name
        /// </summary>
        /// <param name="usingFullName"></param>
        public void AddUsing(String usingFullName)
        {
            _usings.Add(usingFullName);
        }
    }

    public class Lexer
    {
        /// <summary>
        /// Current node where parsing now located
        /// </summary>
        private ParseTreeNode _currentNode;

        public TypeContext Build(String[] filePaths)
        {
            if (filePaths.Length <= 0)
                throw new SchematraException("Files were not specified");

            var typeContext = new TypeContext();
            var context = new Context(typeContext);

            foreach (var filePath in filePaths)
            {
                var tree = BuildParseTree(filePath);

                try
                {
                    ParseUnit(tree, context);
                    return typeContext;
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

        private void ParseUnit(ParseTree tree, Context context)
        {
            foreach (var child in tree.Root.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_defs:
                        ParseDefs(child, context, "");
                        break;
                    case SchematraGrammer.term_using_defs:
                        ParseUsingDefs(child, context);
                        break;
                }
            }
        }

        private void ParseUsingDefs(ParseTreeNode node, Context context)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_using_def:
                        ParseUsingDef(child, context);
                        break;
                }
            }            
        }

        private void ParseUsingDef(ParseTreeNode node, Context context)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_using_def_name:
                        context.AddUsing(child.Token.ValueString);
                        break;
                }
            }                
        }

        private void ParseDefs(ParseTreeNode node, Context context, string space)
        {
            _currentNode = node;

            if (node.Term.Name != SchematraGrammer.term_defs)
                return;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_def:
                        ParseDef(child, context, space);
                        break;
                }
            }            
        }

        private void ParseDef(ParseTreeNode node, Context context, string space)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def:
                        ParseSchemaDef(child, context, space);
                        break;
                    case SchematraGrammer.term_namespace_def:
                        ParseNamespaceDef(child, context, space);
                        break;
                }
            }
        }

        private void ParseNamespaceDef(ParseTreeNode node, Context context, string space)
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
                        ParseDefs(child, context, Utils.ConcatNamespaces(space, namespaceText));
                        break;
                }
            }            
        }

        private void ParseSchemaDef(ParseTreeNode node, Context context, string space)
        {
            _currentNode = node;

            var recordBuilder = new RecordTypeBuilder(context.TypeContext);

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_name:
                        recordBuilder.SetName(child.Token.Text, space);
                        break;

                    case SchematraGrammer.term_schema_def_options:
                        ParseSchemaDefOptions(child, context, recordBuilder);
                        break;

                    case SchematraGrammer.term_schema_def_body:
                        ParseSchemaDefBody(child, context, recordBuilder);
                        break;
                }
            }

            context.TypeContext.AddType(recordBuilder.Create());
        }

        private void ParseSchemaDefBody(ParseTreeNode node, Context context, RecordTypeBuilder recordBuilder)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_field:
                        ParseField(child, context, recordBuilder);
                        break;
                }
            }            
        }

        private void ParseSchemaDefOptions(ParseTreeNode node, Context context, RecordTypeBuilder recordBuilder)
        {
            _currentNode = node;

            if (node.ChildNodes.Count <= 0)
                return;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_option:
                        ParseSchemaDefOption(child, context, recordBuilder);
                        break;
                }
            }            
        }

        private void ParseSchemaDefOption(ParseTreeNode node, Context context, RecordTypeBuilder recordBuilder)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_tagged_option:
                        ParseSchemaDefTaggedOption(child, recordBuilder);
                        break;
                    case SchematraGrammer.term_schema_def_extends_option:
                        ParseSchemaDefExtendsOption(child, context, recordBuilder);
                        break;
                }
            }
        }

        private void ParseSchemaDefExtendsOption(ParseTreeNode node, Context context, RecordTypeBuilder recordBuilder)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_extends:
                        recordBuilder.SetBaseType(new TypeResolver(child.Token.ValueString, context.Usings));
                        break;
                }
            }            
        }

        private void ParseSchemaDefTaggedOption(ParseTreeNode node, RecordTypeBuilder recordBuilder)
        {
            _currentNode = node;

            foreach (var child in node.ChildNodes)
            {
                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_schema_def_tag:

                        var guidText = child.Token.ValueString;
                        recordBuilder.SetTag(Guid.Parse(guidText));
                        break;
                }
            }            
        }

        /// <summary>
        /// Parsing of field nonterm
        /// </summary>
        private void ParseField(ParseTreeNode node, Context context, RecordTypeBuilder recordBuilder)
        {
            _currentNode = node;

            Int32 index = 0;
            FieldQualifier qualifier = FieldQualifier.Optional;
            String typeName = "";
            String name = "";

            foreach (var child in node.ChildNodes)
            {
                if (child.Term == null || child.Term.Name == null)
                    continue;

                switch (child.Term.Name)
                {
                    case SchematraGrammer.term_field_index:
                        index = Int32.Parse(child.Token.Value.ToString());
                        break;

                    case SchematraGrammer.term_field_qualifier:
                        qualifier = GetQualifierByString(child.ChildNodes[0].Term.Name);
                        break;

                    case SchematraGrammer.term_field_nullable:
                        //                        fieldDefinition.Nullable = child.ChildNodes.Count > 0;
                        break;

                    case SchematraGrammer.term_field_schema:
                        typeName = child.Token.Value.ToString();
                        break;

                    case SchematraGrammer.term_field_name:
                        name = child.Token.Value.ToString();
                        break;

                    case SchematraGrammer.term_field_init_value:
                        break;
                }
            }

            recordBuilder.AddField(index, name, new TypeResolver(typeName, context.Usings), qualifier);
        }

        /// <summary>
        /// Set qualifier by string 
        /// </summary>
        public FieldQualifier GetQualifierByString(String qualifier)
        {
            switch (qualifier)
            {
                case "optional": return FieldQualifier.Optional;
                case "required": return FieldQualifier.Required;
                case "repeated": return FieldQualifier.Repeated;
                case "historic": return FieldQualifier.Historic;
            }

            throw new SchematraException("Missed field qualifier, should be \"optional\" or \"required\" but \"{0}\" was found", qualifier);
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