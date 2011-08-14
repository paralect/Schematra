using System;
using System.Collections.Generic;

namespace Paralect.Schemata.Definitions
{
    /// <summary>
    /// Schema definition
    /// </summary>
    public class RecordDefinition : TypeDefinition
    {
        /// <summary>
        /// Name of the schema that extended by this schema
        /// </summary>
        private string _extends;

        /// <summary>
        /// Tag of the schema
        /// </summary>
        private Guid _tag;

        /// <summary>
        /// List of fields
        /// </summary>
        private readonly List<FieldDefinition> _fieldDefinitions = new List<FieldDefinition>();

        /// <summary>
        /// Tag of the schema
        /// </summary>
        public Guid Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// Name of the schema that extended by this schema
        /// </summary>
        public String Extends
        {
            get { return _extends; }
            set { _extends = value; }
        }

        /// <summary>
        /// List of fields
        /// </summary>
        public List<FieldDefinition> FieldDefinitions
        {
            get { return _fieldDefinitions; }
        }
    }
}