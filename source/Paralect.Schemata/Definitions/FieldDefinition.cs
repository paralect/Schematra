using System;

namespace Paralect.Schemata.Definitions
{
    public class FieldDefinition
    {
        /// <summary>
        /// Index of field (should be unique for only one level of hierarchy)
        /// </summary>
        public Int32 Index { get; set; }

        /// <summary>
        /// Name of the field
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Field qualifier (optional, required, repeated)
        /// </summary>
        public FieldQualifier Qualifier { get; set; }

        /// <summary>
        /// Schema of the field value
        /// </summary>
        public String TypeName { get; set; }

        /// <summary>
        /// Default value text
        /// </summary>
        public String DefaultValue { get; set; }

        public Boolean Nullable { get; set; }

        /// <summary>
        /// Set qualifier by string 
        /// </summary>
        public void SetQualifierByString(String qualifier)
        {
            switch (qualifier)
            {
                case "optional": Qualifier = FieldQualifier.Optional; break;
                case "required": Qualifier = FieldQualifier.Required; break;
                case "repeated": Qualifier = FieldQualifier.Repeated; break;
                case "historic": Qualifier = FieldQualifier.Historic; break;
            }
        }
    }
}