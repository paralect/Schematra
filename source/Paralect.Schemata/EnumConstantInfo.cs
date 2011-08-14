using System;

namespace Paralect.Schemata
{
    public class EnumConstantInfo
    {
        /// <summary>
        /// Name of constant
        /// </summary>
        private String _name;

        /// <summary>
        /// Integer index of this constant
        /// </summary>
        private Int32 _index;

        /// <summary>
        /// Name of constant
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Integer index of this constant
        /// </summary>
        public int Index
        {
            get { return _index; }
        }

        public EnumConstantInfo(int index, string name)
        {
            _name = name;
            _index = index;
        }
    }
}