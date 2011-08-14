using System;

namespace Paralect.Schematra
{
    public class EnumTypeBuilder : EnumType
    {
        public EnumTypeBuilder(TypeContext typeContext) : base(typeContext)
        {

        }

        public EnumTypeBuilder AddConstant(Int32 index, String name)
        {
            AddConstantInternal(index, name);
            return this;
        }

        /// <summary>
        /// Define name by name and @namespace
        /// </summary>
        public EnumTypeBuilder SetName(String name, String @namespace)
        {
            SetNameInternal(name, @namespace);
            return this;
        }

        /// <summary>
        /// Define name by full name
        /// </summary>
        public EnumTypeBuilder SetName(String fullName)
        {
            SetNameInternal(fullName);
            return this;
        }

        public EnumType Create()
        {
            return CreateInternal();
        }
    }
}