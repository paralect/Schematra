using System;
using System.Collections.Generic;

namespace Paralect.Schemata.SDO.Helpers
{
    /**
     * Look up a Type given the uri and typeName or interfaceClass.
     * SDO Types are available through the
     *   getType("commonj.sdo", typeName) method.
     * Defines Types from DataObjects.
     */
    public interface TypeHelper
    {
        /**
         * Return the Type specified by typeName with the given uri,
         *   or null if not found.
         * @param uri The uri of the Type - type.getURI();
         * @param typeName The name of the Type - type.getName();
         * @return the Type specified by typeName with the given uri,
         *   or null if not found.
         */
        IType GetType(String uri, String typeName);

        /**
         * Return the Type for this interfaceClass or null if not found.
         * @param interfaceClass is the interface for the DataObject's Type -  
         *   type.getInstanceClass();
         * @return the Type for this interfaceClass or null if not found.
         */
        IType GetType(Type interfaceType);

        /**
         * Get the open content (global) Property with the specified uri and name, or null
         * if not found.
         * @param uri the namespace URI of the open content Property.
         * @param propertyName the name of the open content Property.
         * @return the global Property.
         */
        IProperty GetOpenContentProperty(String uri, String propertyName);

        /**
         * Define the DataObject as a Type.
         * The Type is available through TypeHelper and DataGraph getType() methods.
         * @param type the DataObject representing the Type.
         * @return the defined Type.
         * @throws IllegalArgumentException if the Type could not be defined.
         */
        IType Define(IDataObject type);

        /**
         * Define the list of DataObjects as Types.
         * The Types are available through TypeHelper and DataGraph getType() methods.
         * @param types a List of DataObjects representing the Types.
         * @return the defined Types.
         * @throws IllegalArgumentException if the Types could not be defined.
         */
        List<IType> Define(List<IDataObject> types);

        /**
         * Define the DataObject as a Property for setting open content.
         * The containing Type of the open content property is not specified by SDO.
         * If the specified uri is not null the defined property is accessible through
         * TypeHelper.getOpenContentProperty(uri, propertyName).
         * If a null uri is specified, the location and management of the open content property
         * is not specified by SDO.
         * @param uri the namespace URI of the open content Property or null.
         * @return the defined open content Property.
         * @throws IllegalArgumentException if the Property could not be defined.
         */
        IProperty DefineOpenContentProperty(String uri, IDataObject property);

        /**
         * The default TypeHelper.
         */
//        TODO: uncomment!
//        TypeHelper INSTANCE = HelperProvider.getTypeHelper();
    }
    
}

