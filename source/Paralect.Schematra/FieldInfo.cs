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

        protected FieldQualifier _qualifier;

        /// <summary>
        /// Protected initialization
        /// </summary>
        public FieldInfo(TypeContext typeContext, Int32 index, String name, TypeResolver typeResolver, FieldQualifier qualifier)
        {
            _typeContext = typeContext;
            _index = index;
            _name = name;
            _qualifier = qualifier;
            _typeResolver = typeResolver;
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

        public void Build()
        {
            var type = _typeResolver.Resolve(_typeContext);

            if (type == null)
                throw new TypeNotFoundException("Type for field {0} is invalid", _name);

            _type = type;
        }
    }
}