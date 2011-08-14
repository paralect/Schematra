using System;
using System.Collections.Generic;
using Paralect.Schematra.Exceptions;

namespace Paralect.Schematra
{
    public class EnumType : Type
    {
        protected List<EnumConstantInfo> _constants = new List<EnumConstantInfo>();
        protected Dictionary<Int32, EnumConstantInfo> _constantsByIndex = new Dictionary<int, EnumConstantInfo>();
        protected Dictionary<String, EnumConstantInfo> _constantsByName = new Dictionary<String, EnumConstantInfo>();

        protected EnumType(TypeContext typeContext) : base(typeContext)
        {
        }

        public override void Build()
        {
            // do nothing
        }

        protected EnumType CreateInternal()
        {
            var enumType = new EnumType(_typeContext);
            CopyInternal(enumType);

            foreach (var constantInfo in _constants)
            {
                enumType.AddConstantInternal(constantInfo.Index, constantInfo.Name);
            }

            return enumType;
        }

        protected void AddConstantInternal(Int32 index, String name)
        {
            if (_constantsByName.ContainsKey(name))
                throw new DuplicateConstantNameException("Duplicate constant {0} found for enum {1}", name, FullName);

            if (_constantsByIndex.ContainsKey(index))
                throw new DuplicateFieldIndexException("Duplicate constant index {0} found for enum {1}", index, FullName);

            var constantInfo = new EnumConstantInfo(index, name);

            _constants.Add(constantInfo);
            _constantsByName[constantInfo.Name] = constantInfo;
            _constantsByIndex[constantInfo.Index] = constantInfo;
        }
    }
}