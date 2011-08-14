using System;

namespace Paralect.Schemata.SDO.Helpers
{
    /**
     * A Factory for creating DataObjects.  
     * The created DataObjects are not connected to any other DataObjects.
     */
    public interface IDataFactory
    {
        /**
         * Create a DataObject of the Type specified by typeName with the given package uri.
         * @param uri The uri of the Type.
         * @param typeName The name of the Type.
         * @return the created DataObject.
         * @throws IllegalArgumentException if the uri and typeName does
         *   not correspond to a Type this factory can instantiate.
         */
        IDataObject create(String uri, String typeName);

        /**
         * Create a DataObject supporting the given interface.
         * InterfaceClass is the interface for the DataObject's Type.
         * The DataObject created is an instance of the interfaceClass.
         * @param interfaceClass is the interface for the DataObject's Type.
         * @return the created DataObject.
         * @throws IllegalArgumentException if the instanceClass does
         *   not correspond to a Type this factory can instantiate.
         */
        IDataObject create(Type interfaceType);

        /**
         * Create a DataObject of the Type specified.
         * @param type The Type.
         * @return the created DataObject.
         * @throws IllegalArgumentException if the Type
         *   cannot be instantiaed by this factory.
         */
        IDataObject create(IType type);

        /**
         * The default DataFactory.
         */
//        DataFactory INSTANCE = HelperProvider.getDataFactory();

    }

}

