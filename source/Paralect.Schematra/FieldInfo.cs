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

        public Object DefaultValue
        {
            get { return _defaultValue;  }
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
                if (type.FullName == "Int16")
                    _defaultValue = default(Int16);

                if (type.FullName == "Int32")
                    _defaultValue = default(Int32);

                if (type.FullName == "Int64")
                    _defaultValue = default(Int64);

                if (type.FullName == "Byte")
                    _defaultValue = default(Byte);

                if (type.FullName == "Float")
                    _defaultValue = default(Single);

                if (type.FullName == "Double")
                    _defaultValue = default(Double);

                if (type.FullName == "Boolean")
                    _defaultValue = default(Boolean); // false

                if (type.FullName == "String")
                    _defaultValue = String.Empty;

                if (type.FullName == "Guid")
                    _defaultValue = Guid.Empty;

                // set default value to:
                // for numeric - 0
                // for string - 0
                // for array - []
                // for boolean - false
            }

            if (type is PrimitiveType && _defaultValue != null)
            {
                if (type.FullName == "Int16")
                    _defaultValue = Convert.ToInt16(_defaultValue);

                if (type.FullName == "Int32")
                    _defaultValue = Convert.ToInt32(_defaultValue);

                if (type.FullName == "Int64")
                    _defaultValue = Convert.ToInt64(_defaultValue);

                if (type.FullName == "Byte")
                    _defaultValue = Convert.ToByte(_defaultValue);

                if (type.FullName == "Float")
                    _defaultValue = Convert.ToSingle(_defaultValue);

                if (type.FullName == "Double")
                    _defaultValue = Convert.ToDouble(_defaultValue);

                if (type.FullName == "String")
                    _defaultValue = Convert.ToString(_defaultValue);

                if (type.FullName == "Boolean")
                {
                    var text = Convert.ToString(_defaultValue);

                    if (String.CompareOrdinal(text, "true") == 0 ||
                        String.CompareOrdinal(text, "True") == 0)
                        _defaultValue = true;

                    _defaultValue = false;
                }

                if (type.FullName == "Guid")
                    _defaultValue = Guid.Parse(Convert.ToString(_defaultValue));
            }

            _type = type;
        }
    }
}