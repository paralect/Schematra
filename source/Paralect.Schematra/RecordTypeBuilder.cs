using System;

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
        public RecordTypeBuilder SetBaseType(String typeName)
        {
            _baseTypeName = typeName;
            return this;
        }

        public RecordTypeBuilder AddField(Int32 index, String name, String type)
        {
            AddFieldInternal(index, name, type);
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