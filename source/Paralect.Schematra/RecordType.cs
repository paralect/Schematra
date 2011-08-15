using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra
{
    public class RecordType : Type
    {
        /// <summary>
        /// Record tag
        /// </summary>
        protected Guid _tag;

        /// <summary>
        /// Base type resolver
        /// </summary>
        protected TypeResolver _baseTypeResolver;

        /// <summary>
        /// Cached record base type
        /// </summary>
        protected Type _baseType;

        /// <summary>
        /// Record fields
        /// </summary>
        protected List<FieldInfo> _fields = new List<FieldInfo>();

        /// <summary>
        /// Records fields by name
        /// </summary>
        protected Dictionary<String, FieldInfo> _fieldsByName = new Dictionary<String, FieldInfo>();

        /// <summary>
        /// Records fields by index
        /// </summary>
        protected Dictionary<Int32, FieldInfo> _fieldsByIndex = new Dictionary<Int32, FieldInfo>();



        /// <summary>
        /// Schema tag
        /// </summary>
        public Guid Tag
        {
            get { return _tag; }
        }
        
        /// <summary>
        /// Schema base type
        /// </summary>
        public Type BaseType
        {
            get { return _baseType; }
        }

        protected RecordType(TypeContext typeContext) : base(typeContext)
        {

        }

        /// <summary>
        /// Cloning 
        /// </summary>
        protected RecordType CreateRecordTypeInternal()
        {
            var recordType = new RecordType(_typeContext);
            CopyInternal(recordType);
            recordType._baseTypeResolver = _baseTypeResolver;
            recordType._tag = _tag;

            foreach (var fieldInfo in _fields)
            {
                recordType.AddFieldInternal(fieldInfo.Index, fieldInfo.Name, fieldInfo.TypeResolver, fieldInfo.Qualifier);
            }

            return recordType;
        }

        public override void Build()
        {
            // if record base type was specified
            if (_baseTypeResolver != null)
            {
                var recordBaseType = _baseTypeResolver.Resolve(_typeContext);

                // Extension allowed only for rectords
                if (!(recordBaseType is RecordType))
                    throw new TypeMismatchException("Record can extend only another record (not enum for instance). But this is not true for record {0}.", _fullName);

                _baseType = recordBaseType;
            }

            // build all fields 
            foreach (var fieldInfo in _fields)
            {
                fieldInfo.Build();
            }
        }

        public ReadOnlyCollection<FieldInfo> GetFields()
        {
            return _fields.AsReadOnly();
        }

        public FieldInfo GetField(String name)
        {
            return _fieldsByName[name];
        }

        public FieldInfo GetField(Int32 index)
        {
            return _fieldsByIndex[index];
        }

        protected void AddFieldInternal(Int32 index, String name, TypeResolver typeResolver, FieldQualifier qualifier)
        {
            if (_fieldsByName.ContainsKey(name))
                throw new DuplicateFieldNameException("Duplicate field {0} found for record {1}", name, FullName);

            if (_fieldsByIndex.ContainsKey(index))
                throw new DuplicateFieldIndexException("Duplicate field index {0} found for record {1}", index, FullName);

            var fieldInfo = new FieldInfo(_typeContext, index, name, typeResolver, qualifier);

            _fields.Add(fieldInfo);
            _fieldsByName[fieldInfo.Name] = fieldInfo;
            _fieldsByIndex[fieldInfo.Index] = fieldInfo;
        }
    }
}