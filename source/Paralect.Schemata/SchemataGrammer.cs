using System;
using Irony.Parsing;

namespace Paralect.Schemata
{
    public class SchemataGrammer : Grammar
    {
        #region Terms constants

        public const String term_field_index        = "field-index";
        public const String term_field_name         = "field-name";
        public const string term_field_schema       = "field-schema";
        public const string term_field              = "field";
        public const string term_field_nullable     = "field-nullable";
        public const string term_field_init_value   = "field-init-value";
        public const string term_field_qualifier    = "field-qualifier";
        public const string term_schema_def         = "schema-def";
        public const string term_schema_def_option  = "schema-def-option";
        public const string term_schema_def_tagged_option = "schema-def-tagged-option";
        public const string term_schema_def_extends_option = "schema-def-extends-option";
        public const string term_schema_def_options = "schema-def-options";
        public const string term_schema_def_body    = "schema-def-body";
        public const string term_schema_def_name    = "schema-def-name";
        public const string term_schema_def_extends = "schema-def-extends";
        public const string term_schema_def_tag     = "schema-def-tag";
        public const string term_defs               = "defs";
        public const string term_def                = "def";
        public const string term_unit               = "unit";
        public const string term_namespace_def      = "namespace-def";
        public const string term_namespace_def_name = "namespace-def-name";
        public const string term_using_defs         = "using-defs";
        public const string term_using_def          = "using-def";
        public const string term_using_def_name     = "using-def-name";

        #endregion

        public SchemataGrammer() : base(false)
        {
            const string dotNotationWordPattern     = @"[\w\._]+";
            const string singleWordPattern          = @"[\w_]+";
            const string integerNumberPattern       = "[0-9]+";

            // ******** //
            // Comments //
            // ******** //

            var SingleLineComment   = new CommentTerminal("single-line-comment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            var DelimitedComment    = new CommentTerminal("multi-line-comment", "/*", "*/");
            NonGrammarTerminals.Add(SingleLineComment);
            NonGrammarTerminals.Add(DelimitedComment);

            
            // ************* //
            // Scalar values // 
            // ************* //

            RegexBasedTerminal word         = new RegexBasedTerminal("word", singleWordPattern),
                               identifier   = new RegexBasedTerminal("identifier", dotNotationWordPattern);

            StringLiteral stringValue       = TerminalFactory.CreateCSharpString("string-value");
            NumberLiteral numberValue       = TerminalFactory.CreateCSharpNumber("number-value");
            
            NonTerminal value               = new NonTerminal("value");
            value.Rule                      = (numberValue | stringValue);

            
            // **************** //
            // Field grammer //
            // **************** //

            RegexBasedTerminal fieldIndex   = new RegexBasedTerminal(term_field_index, integerNumberPattern),
                               fieldName    = new RegexBasedTerminal(term_field_name, singleWordPattern),
                               fieldSchema  = new RegexBasedTerminal(term_field_schema, dotNotationWordPattern);

            NonTerminal field               = new NonTerminal(term_field),
                        fieldNullable       = new NonTerminal(term_field_nullable),
                        fieldInitValue      = new NonTerminal(term_field_init_value),
                        fieldQualifier      = new NonTerminal(term_field_qualifier);
                        
            fieldNullable.Rule              = ToTerm("nullable") | Empty;

            field.Rule                      = fieldIndex + ":" + fieldQualifier + fieldNullable + fieldSchema + 
                                               fieldName + fieldInitValue + ";";

            fieldInitValue.Rule             = (ToTerm("=") + value) | Empty;
            fieldQualifier.Rule             = ToTerm("optional") | "required" | "repeated" | "historic";


            // ************** //
            // Schema grammer //
            // ************** //

            StringLiteral schemaDefTag          = TerminalFactory.CreateCSharpString(term_schema_def_tag);
            RegexBasedTerminal schemaDefExtends = new RegexBasedTerminal(term_schema_def_extends, dotNotationWordPattern);
            RegexBasedTerminal schemaDefName = new RegexBasedTerminal(term_schema_def_name, singleWordPattern);

            NonTerminal schemaDef               = new NonTerminal(term_schema_def),
                        schemaDefOption         = new NonTerminal(term_schema_def_option),
                        schemaDefTaggedOption   = new NonTerminal(term_schema_def_tagged_option),
                        schemaDefExtendsOption  = new NonTerminal(term_schema_def_extends_option),
                        schemaDefOptions        = new NonTerminal(term_schema_def_options),
                        schemaDefBody           = new NonTerminal(term_schema_def_body);

            schemaDefTaggedOption.Rule          = ToTerm("tagged") + schemaDefTag;
            schemaDefExtendsOption.Rule         = ToTerm("extends") + schemaDefExtends;

            schemaDefOption.Rule                = schemaDefTaggedOption | schemaDefExtendsOption;
            schemaDefOptions.Rule               = MakeStarRule(schemaDefOptions, schemaDefOption);
            schemaDef.Rule                      = ToTerm("record") + schemaDefName + schemaDefOptions + "{" + schemaDefBody + "}";
            schemaDefBody.Rule                  = MakeStarRule(schemaDefBody, field);
                        

            // ************* //
            // Using grammer //
            // ************* //

            RegexBasedTerminal usingDefName     = new RegexBasedTerminal(term_using_def_name, dotNotationWordPattern);

            NonTerminal usingDefs               = new NonTerminal(term_using_defs),
                        usingDef                = new NonTerminal(term_using_def);

            usingDef.Rule                       = ToTerm("using") + usingDefName + ";";
            usingDefs.Rule                      = MakeStarRule(usingDefs, usingDef);

            // *************** //
            // Unit grammer //
            // *************** //

            RegexBasedTerminal namespaceDefName = new RegexBasedTerminal(term_namespace_def_name, dotNotationWordPattern);

            NonTerminal unit                = new NonTerminal(term_unit),
                        def                 = new NonTerminal(term_def),
                        defs                = new NonTerminal(term_defs),
                        namespaceDef        = new NonTerminal(term_namespace_def);

            unit.Rule                       = usingDefs + defs;
            defs.Rule                       = MakeStarRule(defs, def);
            def.Rule                        = schemaDef | namespaceDef;
            namespaceDef.Rule               = ToTerm("namespace") + namespaceDefName + "{" + defs + "}";

            this.Root = unit;

            MarkPunctuation("enum", "a", "=", "[", "]", "record", "{", "}", ",", ";", ":", "namespace", "tagged", "extends", "schema", "using");
        }

        public ParseTree ParseTree(string sourceCode)
        {
            LanguageData language = new LanguageData(this);
            Irony.Parsing.Parser parser = new Irony.Parsing.Parser(language);
            ParseTree parseTree = parser.Parse(sourceCode);
            return parseTree;
        }

        public bool IsValid(string sourceCode)
        {
            LanguageData language = new LanguageData(this);
            Irony.Parsing.Parser parser = new Irony.Parsing.Parser(language);
            ParseTree parseTree = parser.Parse(sourceCode);

            Validate(parseTree);

            ParseTreeNode root = parseTree.Root;
            return root != null;
        }

        public ParseTreeNode GetRoot(string sourceCode)
        {
            LanguageData language = new LanguageData(this);
            Irony.Parsing.Parser parser = new Irony.Parsing.Parser(language);
            ParseTree parseTree = parser.Parse(sourceCode);

            Validate(parseTree);

            ParseTreeNode root = parseTree.Root;
            return root;
        }

        public void Validate(ParseTree parseTree)
        {
            if (parseTree.HasErrors() && parseTree.ParserMessages.Count > 0)
            {
                var message = parseTree.ParserMessages[0];
                var error = message.Message + " at " + message.Location.ToUiString();
                throw new Exception(error);
            }            
        }

        public void DisplayTree(ParseTreeNode node, int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("  ");

            Console.WriteLine(node);

            foreach (ParseTreeNode child in node.ChildNodes)
                DisplayTree(child, level + 1);
        }
    }
}
