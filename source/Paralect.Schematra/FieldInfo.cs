using System;
using System.Collections.Generic;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra
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
        protected TypeResolver _typeResolver;

        /// <summary>
        /// Field qualifier (optional or required)
        /// </summary>
        protected FieldQualifier _qualifier;

        /// <summary>
        /// Default value of property
        /// </summary>
        protected Object _defaultValue;

        /// <summary>
        /// Protected initialization
        /// </summary>
        public FieldInfo(TypeContext typeContext, Int32 index, String name, TypeResolver typeResolver, FieldQualifier qualifier, Object defaultValue)
        {
            _typeContext = typeContext;
            _index = index;
            _name = name;
            _qualifier = qualifier;
            _typeResolver = typeResolver;
            _defaultValue = defaultValue;
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

        /// <summary>
        /// Field qualifier (optional or required)
        /// </summary>
        public FieldQualifier Qualifier
        {
            get { return _qualifier; }
        }

        /// <summary>
        /// Type of the field
        /// </summary>
        public Type Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Field type full name
        /// </summary>
        public TypeResolver TypeResolver
        {
            get { return _typeResolver; }
        }

        /// <summary>
        /// Build FieldInfo
        /// </summary>
        public void Build()
        {
            var type = _typeResolver.Resolve(_typeContext);

            if (type == null)
                throw new TypeNotFoundException("Type for field {0} is invalid", _name);

            if (type is PrimitiveType && _defaultValue == null)
            {
                // set default value to:
                // for numeric - 0
                // for string - 0
                // for array - []
                // for boolean - false
            }

            _type = type;
        }
    }
}