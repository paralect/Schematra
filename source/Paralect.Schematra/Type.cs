using System;
using System.Collections.Generic;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra
{
    public abstract class Type
    {
        /// <summary>
        /// Type context this type belongs to
        /// </summary>
        protected readonly TypeContext _typeContext;

        /// <summary>
        /// Short name of type (without namespace prefix)
        /// </summary>
        protected String _name;

        /// <summary>
        /// Namespace of this type
        /// </summary>
        protected String _namespace;

        /// <summary>
        /// Full name of type with namespace
        /// </summary>
        protected String _fullName;

        /// <summary>
        /// List of aliasses
        /// </summary>
        protected List<String> _aliases = new List<String>();

        /// <summary>
        /// Short name of type (without namespace prefix)
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Namespace of this type
        /// </summary>
        public string Namespace
        {
            get { return _namespace; }
        }

        /// <summary>
        /// Full name of type with namespace
        /// </summary>
        public String FullName
        {
            get { return _fullName; }
        }

        /// <summary>
        /// List of aliasses
        /// </summary>
        public ICollection<string> Aliases
        {
            get { return _aliases.AsReadOnly(); }
        }

        /// <summary>
        /// Initialization
        /// </summary>
        protected Type(TypeContext typeContext)
        {
            _typeContext = typeContext;
        }

        public abstract void Build();

        /// <summary>
        /// Define name by name and @namespace
        /// </summary>
        protected void SetNameInternal(String name, String @namespace)
        {
            _name = name;
            _namespace = @namespace;
            _fullName = Utils.ConcatNamespaces(@namespace, name);
        }

        /// <summary>
        /// Define name by full name
        /// </summary>
        protected void SetNameInternal(String fullName)
        {
            _fullName = fullName;

            // Extract name
            var index = _fullName.LastIndexOf('.');
            _name = index != -1 ? fullName.Substring(index + 1) : _fullName;
            _namespace = index != -1 ? fullName.Substring(0, index) : String.Empty;
        }

        /// <summary>
        /// Add alias
        /// </summary>
        protected void AddAliasInternal(String alias)
        {
            if (_aliases.Contains(alias))
                throw new DuplicateTypeAliasException("Alias {0} already defined for type {1}", alias, _fullName);

            _aliases.Add(alias);
        }

        /// <summary>
        /// Copy this type infor to another type instance
        /// </summary>
        /// <param name="to"></param>
        protected void CopyInternal(Type to)
        {
            to._name = _name;
            to._fullName = _fullName;
            to._namespace = _namespace;

            foreach (var aliase in _aliases)
            {
                to.AddAliasInternal(aliase);
            }
        }
    }
}