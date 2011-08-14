using System;
using Paralect.Schemata.Exceptions;

namespace Paralect.Schemata
{
    /// <summary>
    /// Represent field metadata 
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// Type context
        /// </summary>
        private readonly TypeContext _typeContext;

        /// <summary>
        /// Index of field (should be unique for only one level of hierarchy)
        /// </summary>
        protected Int32 _index;

        /// <summary>
        /// Name of the field
        /// </summary>
        protected String _name;

        /// <summary>
        /// Cached value of field type
        /// </summary>
        protected Type _type;

        /// <summary>
        /// Field type full name
        /// </summary>
        protected String _typeFullName;

        /// <summary>
        /// Protected initialization
        /// </summary>
        public FieldInfo(TypeContext typeContext, Int32 index, String name, String typeFullName)
        {
            _typeContext = typeContext;
            _index = index;
            _name = name;
            _typeFullName = typeFullName;
        }

        /// <summary>
        /// Index of field (should be unique for only one level of hierarchy)
        /// </summary>
        public Int32 Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Name of the field
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        public string TypeFullName
        {
            get { return _typeFullName; }
        }

        /// <summary>
        /// Type of the field
        /// </summary>
        public Type Type
        {
            get { return _type; }
        }

        public void Build()
        {
            var type = _typeContext.GetByFullName(_typeFullName);

            if (type == null)
                throw new SchemataException("Type for field {0} is invalid", _name);

            _type = type;
        }
    }
}