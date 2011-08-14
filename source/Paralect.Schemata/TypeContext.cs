using System;
using System.Collections.Generic;
using Paralect.Schemata.Exceptions;

namespace Paralect.Schemata
{
    public class TypeContext
    {
        /// <summary>
        /// All types available in this context
        /// </summary>
        private readonly List<Type> _types = new List<Type>(32);

        /// <summary>
        /// Types hashed by by full name
        /// </summary>
        private readonly Dictionary<String, Type> _typesByFullName = new Dictionary<String, Type>(64);
        
        /// <summary>
        /// Schemas, hashed by by tag name
        /// </summary>
        private readonly Dictionary<Guid, RecordType> _typesByTag = new Dictionary<Guid, RecordType>(32);

        /// <summary>
        /// Initialization
        /// </summary>
        public TypeContext()
        {
            // An 8-bit signed integer
            AddType(new PrimitiveTypeBuilder(this).SetName("Byte").AddAlias("byte").AddAlias("int8").AddAlias("i8").Create());

            // A 16-bit signed integer
            AddType(new PrimitiveTypeBuilder(this).SetName("Int16").AddAlias("int16").AddAlias("i16").Create());

            // A 32-bit signed integer
            AddType(new PrimitiveTypeBuilder(this).SetName("Int32").AddAlias("int32").AddAlias("i32").Create());

            // A 64-bit signed integer
            AddType(new PrimitiveTypeBuilder(this).SetName("Int64").AddAlias("int64").AddAlias("i64").Create());

            // Single precision (32-bit) IEEE 754 floating-point number
            AddType(new PrimitiveTypeBuilder(this).SetName("Float").AddAlias("float").Create());

            // Double precision (64-bit) IEEE 754 floating-point number
            AddType(new PrimitiveTypeBuilder(this).SetName("Double").AddAlias("double").Create());

            // Unicode character sequence
            AddType(new PrimitiveTypeBuilder(this).SetName("String").AddAlias("string").Create());

            // A boolean value
            AddType(new PrimitiveTypeBuilder(this).SetName("Boolean").AddAlias("boolean").AddAlias("Bool").AddAlias("bool").Create());

            // Do we need this if we have "byte" and we can specify byte[]? 
            AddType(new PrimitiveTypeBuilder(this).SetName("Binary").AddAlias("binary").AddAlias("Bytes").AddAlias("bytes").Create());
        }

        /// <summary>
        /// Build type context. Insure that all references are valid and there is no circular depenency.
        /// Type context that wasn't built shouldn't be used.
        /// </summary>
        public void Build()
        {
            // Build each type
            _types.ForEach(t => t.Build());

            // Insure that there is no circular dependency
            CheckForCircularDependency();
        }

        /// <summary>
        /// Check that there is no circular dependency between all record types
        /// </summary>
        private void CheckForCircularDependency()
        {
            foreach (var type in _types)
            {
                // we are interesting only in Records
                if (!(type is RecordType))
                    continue;

                var recordType = (RecordType) type;

                // Traversing tree of base types checking that this type doesn't directly or indirectly references to itself.
                var baseType = recordType.BaseType;
                while (baseType != null)
                {
                    if (baseType == recordType)
                        throw new CircularDependencyException("Circular dependency between types {0} and {1}", recordType.FullName, baseType.FullName);

                    baseType = ((RecordType)baseType).BaseType;
                }
            }            
        }

        /// <summary>
        /// Define Record type
        /// </summary>
        public TypeContext DefineRecord(Action<RecordTypeBuilder> builder)
        {
            var recordTypeBuilder = new RecordTypeBuilder(this);
            builder(recordTypeBuilder);

            AddType(recordTypeBuilder.Create());
            return this;
        }

        /// <summary>
        /// Define Enum type
        /// </summary>
        public TypeContext DefineEnum(Action<EnumTypeBuilder> builder)
        {
            var enumTypeBuilder = new EnumTypeBuilder(this);
            builder(enumTypeBuilder);

            AddType(enumTypeBuilder.Create());
            return this;            
        }

        /// <summary>
        /// Add schema to context
        /// </summary>
        public void AddType(Type type)
        {
            // Check that schema FullName is unique
            if (_typesByFullName.ContainsKey(type.FullName))
                throw new DuplicateTypeNameException("Type {0} already registered", type.FullName);

            // Check that aliases are unique
            foreach (var aliasName in type.Aliases)
            {
                var aliasFullName = Utils.ConcatNamespaces(type.Namespace, aliasName);

                if (_typesByFullName.ContainsKey(aliasFullName))
                    throw new DuplicateTypeNameException("Alias {0} for type {1} cannot be added because type {2} already registered", aliasName, type.FullName, aliasFullName);
            }

            if (type is RecordType)
            {
                var recordType = (RecordType) type;

                // Check that schema tag was specified
                if (recordType.Tag == Guid.Empty)
                    throw new TypeNotTaggedException("Type {0} doesn't tagged. Please specify tag (GUID).", recordType.FullName);

                // Check that schema Tag is unique.
                RecordType existedRecordType;
                if (_typesByTag.TryGetValue(recordType.Tag, out existedRecordType))
                    throw new DuplicateTypeTagException("Type {0} with tag {1} already registered. Cannot register {2} with the same tag.",
                        existedRecordType.FullName,
                        existedRecordType.Tag,
                        recordType.FullName);
            }


            // Add schema to context
            _types.Add(type);
            _typesByFullName.Add(type.FullName, type);

            // Register aliasses
            foreach (var aliasName in type.Aliases)
            {
                var aliasFullName = Utils.ConcatNamespaces(type.Namespace, aliasName);
                _typesByFullName.Add(aliasFullName, type);
            }


            if (type is RecordType)
            {
                var recordType = (RecordType)type;
                _typesByTag.Add(recordType.Tag, recordType);
            }
            
        }

        /// <summary>
        /// Remove schema from the context
        /// </summary>
        public void RemoveSchema(RecordType recordType)
        {
            _types.Remove(recordType);
            _typesByFullName.Remove(recordType.FullName);
            _typesByTag.Remove(recordType.Tag);
        }

        public RecordType GetRecordType(String fullName)
        {
            return GetByFullName(fullName) as RecordType;
        }

        public Type GetByFullName(String fullName)
        {
            if (fullName == null)
                throw new ArgumentNullException("fullName");

            Type type;
            _typesByFullName.TryGetValue(fullName, out type);

            return type;
        }
    }
}