using System;

namespace Paralect.Schemata
{
    public class Utils
    {
        /// <summary>
        /// Concat two namespaces
        /// </summary>
        public static String ConcatNamespaces(String namespace1, String namespace2)
        {
            if (String.IsNullOrEmpty(namespace1) && !String.IsNullOrEmpty(namespace2))
                return namespace2;

            if (!String.IsNullOrEmpty(namespace1) && String.IsNullOrEmpty(namespace2))
                return namespace1;

            if (!String.IsNullOrEmpty(namespace1) && !String.IsNullOrEmpty(namespace2))
                return String.Format("{0}.{1}", namespace1, namespace2);

            return String.Empty;
        }         
    }
}