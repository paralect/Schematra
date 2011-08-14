using System;
namespace Paralect.Schemata
{
    public class PrimitiveTypeBuilder : PrimitiveType
    {
        public PrimitiveTypeBuilder(TypeContext typeContext) : base(typeContext)
        {
        }

        /// <summary>
        /// Define name by name and @namespace
        /// </summary>
        public PrimitiveTypeBuilder SetName(String name, String @namespace)
        {
            SetNameInternal(name, @namespace);
            return this;
        }

        /// <summary>
        /// Define name by full name
        /// </summary>
        public PrimitiveTypeBuilder SetName(String fullName)
        {
            SetNameInternal(fullName);
            return this;
        }

        public PrimitiveTypeBuilder AddAlias(String aliasName)
        {
            AddAliasInternal(aliasName);
            return this;
        }

        public PrimitiveType Create()
        {
            return CreateInternal();
        }
    }
}