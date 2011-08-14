using System;

namespace Paralect.Schematra.Definitions
{
    /// <summary>
    /// Can be schema, enum, namespace, using etc.
    /// </summary>
    public class TypeDefinition
    {
        /// <summary>
        /// Name of the schema
        /// </summary>
        protected string _name;

        /// <summary>
        /// Namespace
        /// </summary>
        protected string _namespace;

        /// <summary>
        /// Name of the schema
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Namespace
        /// </summary>
        public string Namespace
        {
            get { return _namespace; }
            set { _namespace = value; }
        }
    }
}