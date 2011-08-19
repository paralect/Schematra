using System;
using System.Collections.Generic;

namespace Paralect.Schematra
{
    public class RecordTypeBuilder : RecordType
    {
        public RecordTypeBuilder(TypeContext typeContext) : base(typeContext)
        {
            
        }


        /// <summary>
        /// Define name by name and @namespace
        /// </summary>
        public RecordTypeBuilder SetName(String name, String @namespace)
        {
            SetNameInternal(name, @namespace);
            return this;
        }        

        /// <summary>
        /// Define name by full name
        /// </summary>
        public RecordTypeBuilder SetName(String fullName)
        {
            SetNameInternal(fullName);
            return this;
        }

        /// <summary>
        /// Define tag
        /// </summary>
        public RecordTypeBuilder SetTag(Guid tag)
        {
            _tag = tag;
            return this;
        }

        /// <summary>
        /// Define base record type
        /// </summary>
        public RecordTypeBuilder SetBaseType(String baseType)
        {
            _baseTypeResolver = new TypeResolver(baseType);
            return this;
        }
        /// <summary>
        /// Define base record type
        /// </summary>
        public RecordTypeBuilder SetBaseType(TypeResolver baseTypeResolver)
        {
            _baseTypeResolver = baseTypeResolver;
            return this;
        }

        public RecordTypeBuilder AddField(Int32 index, String name, TypeResolver typeResolver, FieldQualifier qualifier, Object defaultValue)
        {
            AddFieldInternal(index, name, typeResolver, qualifier, defaultValue);
            return this;
        }

        public RecordTypeBuilder AddField(Int32 index, String name, String typeName, FieldQualifier qualifier, Object defaultValue)
        {
            AddFieldInternal(index, name, new TypeResolver(typeName), qualifier, defaultValue);
            return this;
        }

        public RecordTypeBuilder SetUsings(List<String> usings)
        {
            SetUsingsInternal(usings);
            return this;
        }

        /// <summary>
        /// Create instance of RecordType. In case it was bult incorrect - throws exception.
        /// </summary>
        public RecordType Create()
        {
            // Defined in parent class as a clone-like function.
            return CreateRecordTypeInternal();
        }
    }
}