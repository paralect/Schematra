using System.Collections.Generic;

namespace Paralect.Schematra.Definitions
{
    /// <summary>
    /// Defines compilation unit that can be compiled
    /// </summary>
    public class CompilationDefinition
    {
        /// <summary>
        /// List of units to compile
        /// </summary>
        private List<UnitDefinition> units = new List<UnitDefinition>();

        /// <summary>
        /// List of units to compile
        /// </summary>
        public List<UnitDefinition> Units
        {
            get { return units; }
        }

        /// <summary>
        /// Merge with another compilation definition
        /// </summary>
        public void Merge(CompilationDefinition compilationDefinition)
        {
            units.AddRange(compilationDefinition.Units);
        }
    }
}