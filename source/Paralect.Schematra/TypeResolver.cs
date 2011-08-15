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
            var index = _name.LastIndexOf('.');

            // if name contains dots, then this is a namespace qualified name
            if (index != -1)
            {
                Type type = typeContext.GetByFullName(_name);

                // throw if type wasn't found
                if (type == null)
                    throw new TypeNotFoundException("There is no type {0}", _name);
            }

            // check that this name not exists in global namespase:
            Type resolvedType = typeContext.GetByFullName(_name);
            if (resolvedType != null)
                return resolvedType;

            // try to find type walking through all usings
            foreach (var @using in _usings)
            {
                var typeName = Utils.ConcatNamespaces(@using, _name);
                Type type = typeContext.GetByFullName(typeName);
                if (type != null)
                    return type;
            }

            throw new TypeNotFoundException("There is no type {0}. The following namespaces were checked: {1}", _name, String.Join(", ", _usings));
        }
    }
}