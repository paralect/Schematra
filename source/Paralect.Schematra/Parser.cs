using System;
using System.Collections.Generic;
using Paralect.Schematra.Definitions;

namespace Paralect.Schematra
{
    public class Parser
    {
        /// <summary>
        /// Compilation definition (contains all symbols)
        /// </summary>
        private readonly CompilationDefinition _compilation;

        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<String, Type> _typeByFullName = new Dictionary<String, Type>();

        /// <summary>
        /// TypeContext
        /// </summary>
        private readonly TypeContext _context;


        /// <summary>
        /// Initialization
        /// </summary>
        public Parser(CompilationDefinition compilation)
        {
            _compilation = compilation;
            _context = new TypeContext();
        }

        /*
        /// <summary>
        /// Parse definitions and return SchemaContext on success.
        /// Throws if error.
        /// </summary>
        public TypeContext Parse()
        {
            PreRegister();

            foreach (var unitDefinition in _compilation.Units)
            {
                foreach (var typeDefinition in unitDefinition.TypeDefinitions)
                {
                    if (typeDefinition is RecordDefinition)
                    {
                        ParseRecordDefinition(typeDefinition as RecordDefinition, unitDefinition);
                    }
                    else if (typeDefinition is EnumDefinition)
                    {
                        ParseEnumDefinition(typeDefinition as EnumDefinition, unitDefinition);
                    }
                }
            }

            return _context;
        }

        private void ParseEnumDefinition(EnumDefinition enumDefinition, UnitDefinition unitDefinition)
        {
            throw new NotImplementedException();
        }

        private void ParseRecordDefinition(RecordDefinition recordDefinition, UnitDefinition unitDefinition)
        {
            var fullName = Utils.ConcatNamespaces(recordDefinition.Namespace, recordDefinition.Name);

            var recordTypeBuilder = (RecordTypeBuilder) _context.GetByFullName(fullName);

            // Set tag
            recordTypeBuilder.DefineTag(recordDefinition.Tag);

            // Set base type if available
            if (recordDefinition.Extends != null)
            {
                recordTypeBuilder.DefineBaseRecordType((RecordType)GetType(unitDefinition, recordDefinition.Extends));
            }

            foreach (var fieldDefinition in recordDefinition.FieldDefinitions)
            {
                var fieldInfoBuilder = new FieldInfoBuilder();
                fieldInfoBuilder.DefineIndex(fieldDefinition.Index);
                fieldInfoBuilder.DefineName(fieldDefinition.Name);
                fieldInfoBuilder.DefineQualifier(fieldDefinition.Qualifier);
                fieldInfoBuilder.DefineType(GetType(unitDefinition, fieldDefinition.TypeName));

                recordTypeBuilder.DefineField(fieldInfoBuilder);
            }
        }

        private void PreRegister()
        {
            foreach (var unitDefinition in _compilation.Units)
            {
                foreach (var typeDefinition in unitDefinition.TypeDefinitions)
                {
                    var fullName = Utils.ConcatNamespaces(typeDefinition.Namespace, typeDefinition.Name);

                    if (typeDefinition is RecordDefinition)
                    {
                        var recordDefinition = (RecordDefinition) typeDefinition;

                        var recordType = new RecordTypeBuilder()
                            .DefineName(fullName)
                            .DefineTag(recordDefinition.Tag);

                        _context.AddType(recordType);
                    }
                }
            }            
        }

        private Type GetType(UnitDefinition unit, String nameOrFullName)
        {
            var index = nameOrFullName.LastIndexOf('.');

            // if it is a full name (i.e. contains dot)
            if (index != -1)
            {
                return _context.GetByFullName(nameOrFullName);
            }

            var fullName = "";
            foreach (var @using in unit.Usings)
            {
                var currentfullName = Utils.ConcatNamespaces(@using, nameOrFullName);

                var type = _context.GetByFullName(fullName);
                if (type != null)
                {
                    fullName = currentfullName;
                    break;
                }
            }

            if (fullName == "")
            {
                var type = _context.GetByFullName(nameOrFullName);

                if (type != null)
                    fullName = nameOrFullName;                
            }

            if (fullName == "")
                throw new SchemaException("The type or namespace name '{0}' could not be found (are you missing a using directive or an file reference?)", nameOrFullName);

            return _context.GetByFullName(fullName);
        }*/
    }
}

