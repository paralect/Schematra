using System;
using System.Collections.Generic;

namespace Paralect.Schematra.Definitions
{
    /// <summary>
    /// Represents file (logical unit)
    /// </summary>
    public class UnitDefinition
    {
        /// <summary>
        /// List of inner defs
        /// </summary>
        private readonly List<TypeDefinition> _typeDefinitions = new List<TypeDefinition>();

        /// <summary>
        /// Usings defined for this unit
        /// </summary>
        private readonly List<String> _usings = new List<string>();

        /// <summary>
        /// List of inner defs
        /// </summary>
        public List<TypeDefinition> TypeDefinitions
        {
            get { return _typeDefinitions; }
        }

        public List<string> Usings
        {
            get { return _usings; }
        }
    }
}