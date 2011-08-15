using System;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra
{
    public class TypeResolver
    {
        /// <summary>
        /// Name of the type (can be namespace qualified name (i.e. full name) or short name)
        /// </summary>
        private String _name;

        /// <summary>
        /// List of usings that should be respected
        /// </summary>
        private String[] _usings;

        /// <summary>
        /// Initialization
        /// </summary>
        public TypeResolver(String name, String[] usings = null)
        {
            _name = name;
            _usings = usings ?? new String[]{};
        }

        public Type Resolve(TypeContext typeContext)
        {
            var type = typeContext.GetByFullName(_name);

            // throw if type wasn't found
            if (type == null)
                throw new TypeNotFoundException("There is no type {0}", _name);

            return type;
        }
    }
}