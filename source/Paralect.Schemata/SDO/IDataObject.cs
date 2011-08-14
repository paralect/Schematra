using System;
using System.Collections.Generic;

namespace Paralect.Schemata.SDO
{
    /**
     * A data object is a representation of some structured data. 
     * It is the fundamental component in the SDO (Service Data Objects) package.
     * Data objects support reflection, path-based access, convenience creation and deletion methods, 
     * and the ability to be part of a {@link DataGraph data graph}.
     * <p>
     * Each data object holds its data as a series of {@link Property Properties}. 
     * Properties can be accessed by name, property index, or using the property meta object itself. 
     * A data object can also contain references to other data objects, through reference-type Properties.
     * <p>
     * A data object has a series of convenience accessors for its Properties. 
     * These methods either use a path (String), 
     * a property index, 
     * or the {@link Property property's meta object} itself, to identify the property.
     * Some examples of the path-based accessors are as follows:
     *<pre>
     * DataObject company = ...;
     * company.get("name");                   is the same as company.get(company.getType().getProperty("name"))
     * company.set("name", "acme");
     * company.get("department.0/name")       is the same as ((DataObject)((List)company.get("department")).get(0)).get("name")
     *                                        .n  indexes from 0 ... implies the name property of the first department
     * company.get("department[1]/name")      [] indexes from 1 ... implies the name property of the first department
     * company.get("department[number=123]")  returns the first department where number=123
     * company.get("..")                      returns the containing data object
     * company.get("/")                       returns the root containing data object
     *</pre> 
     * <p> There are general accessors for Properties, i.e., {@link #get(Property) get} and {@link #set(Property, Object) set}, 
     * as well as specific accessors for the primitive types and commonly used data types like 
     * String, Date, List, BigInteger, and BigDecimal.
     */
    public interface IDataObject
    {
        /**
        * Returns the value of a property of either this object or an object reachable from it, as identified by the
        * specified path.
        * @param path the path to a valid object and property.
        * @return the value of the specified property.
        * @see #get(Property)
        */
        Object Get(String path);

        /**
        * Sets a property of either this object or an object reachable from it, as identified by the specified path,
        * to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void Set(String path, Object value);

        /**
        * Returns whether a property of either this object or an object reachable from it, as identified by the specified path,
        * is considered to be set.
        * @param path the path to a valid object and property.
        * @see #isSet(Property)
        */
        Boolean IsSet(String path);

        /**
        * Unsets a property of either this object or an object reachable from it, as identified by the specified path.
        * @param path the path to a valid object and property.
        * @see #unset(Property)
        */
        void Unset(String path);

        /**
        * Returns the value of a <code>boolean</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>boolean</code> value of the specified property.
        * @see #get(String)
        */
        Boolean GetBoolean(String path);

        /**
        * Returns the value of a <code>byte</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>byte</code> value of the specified property.
        * @see #get(String)
        */
        Byte GetByte(String path);

        /**
        * Returns the value of a <code>char</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>char</code> value of the specified property.
        * @see #get(String)
        */
        Char GetChar(String path);

        /**
        * Returns the value of a <code>double</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>double</code> value of the specified property.
        * @see #get(String)
        */
        Double GetDouble(String path);

        /**
        * Returns the value of a <code>float</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>float</code> value of the specified property.
        * @see #get(String)
        */
        Single GetSingle(String path);

        /**
        * Returns the value of a <code>int</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>int</code> value of the specified property.
        * @see #get(String)
        */
        Int32 getInt32(String path);

        /**
        * Returns the value of a <code>long</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>long</code> value of the specified property.
        * @see #get(String)
        */
        Int64 GetInt64(String path);

        /**
        * Returns the value of a <code>short</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>short</code> value of the specified property.
        * @see #get(String)
        */
        Int16 GetInt16(String path);

        /**
        * Returns the value of a <code>byte[]</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>byte[]</code> value of the specified property.
        * @see #get(String)
        */
        Byte[] GetBytes(String path);

        /**
        * Returns the value of a <code>BigDecimal</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>BigDecimal</code> value of the specified property.
        * @see #get(String)
        */
        Decimal GetDecimal(String path);

        /**
        * Returns the value of a <code>DataObject</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>DataObject</code> value of the specified property.
        * @see #get(String)
        */
        IDataObject GetDataObject(String path);

        /**
        * Returns the value of a <code>Date</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>Date</code> value of the specified property.
        * @see #get(String)
        */
        DateTime GetDateTime(String path);

        /**
        * Returns the value of a <code>String</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>String</code> value of the specified property.
        * @see #get(String)
        */
        String GetString(String path);

        /**
        * Returns the value of a <code>List</code> property identified by the specified path.
        * @param path the path to a valid object and property.
        * @return the <code>List</code> value of the specified property.
        * @see #get(String)
        */
        List<Object> GetList(String path);

        /**
        * @see #getSequence()
        * Returns the value of a <code>Sequence</code> property identified by the specified path.
        * An implementation may throw an UnsupportedOperationException.
        * @param path the path to a valid object and property.
        * @return the <code>Sequence</code> value of the specified property.
        * @see #get(String)
        * @deprecated in 2.1.0.
        */
        ISequence GetSequence(String path);

        /**
        * Sets the value of a <code>boolean</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetBoolean(String path, Boolean value);

        /**
        * Sets the value of a <code>byte</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetByte(String path, Byte value);

        /**
        * Sets the value of a <code>char</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetChar(String path, Char value);

        /**
        * Sets the value of a <code>double</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetDouble(String path, Double value);

        /**
        * Sets the value of a <code>float</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetSingle(String path, Single value);

        /**
        * Sets the value of a <code>int</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetInt32(String path, Int32 value);

        /**
        * Sets the value of a <code>long</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetInt64(String path, Int64 value);

        /**
        * Sets the value of a <code>short</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetInt16(String path, Int16 value);

        /**
        * Sets the value of a <code>byte[]</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetBytes(String path, Byte[] value);

        /**
        * Sets the value of a <code>BigDecimal</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetDecimal(String path, Decimal value);

        /**
        * Sets the value of a <code>DataObject</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetDataObject(String path, IDataObject value);

        /**
        * Sets the value of a <code>Date</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetDate(String path, DateTime value);

        /**
        * Sets the value of a <code>String</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        */
        void SetString(String path, String value);

        /**
        * Sets the value of a <code>List</code> property identified by the specified path, to the specified value.
        * @param path the path to a valid object and property.
        * @param value the new value for the property.
        * @see #set(String, Object)
        * @see #setList(Property, List)
        */
        void SetList(String path, List<Object> value);

        /**
        * Returns the value of the property at the specified index in {@link Type#getProperties property list} 
        * of this object's {@link Type type}.
        * @param propertyIndex the index of the property.
        * @return the value of the specified property.
        * @see #get(Property)
        */
        Object Get(int propertyIndex);

        /**
        * Sets the property at the specified index in {@link Type#getProperties property list} of this object's
        * {@link Type type}, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void Set(int propertyIndex, Object value);

        /**
        * Returns whether the the property at the specified index in {@link Type#getProperties property list} of this object's
        * {@link Type type}, is considered to be set.
        * @param propertyIndex the index of the property.
        * @return whether the specified property is set.
        * @see #isSet(Property)
        */
        Boolean IsSet(int propertyIndex);

        /**
        * Unsets the property at the specified index in {@link Type#getProperties property list} of this object's {@link Type type}.
        * @param propertyIndex the index of the property.
        * @see #unset(Property)
        */
        void Unset(int propertyIndex);

        /**
        * Returns the value of a <code>boolean</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>boolean</code> value of the specified property.
        * @see #get(int)
        */
        Boolean GetBoolean(int propertyIndex);

        /**
        * Returns the value of a <code>byte</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>byte</code> value of the specified property.
        * @see #get(int)
        */
        Byte GetByte(int propertyIndex);

        /**
        * Returns the value of a <code>char</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>char</code> value of the specified property.
        * @see #get(int)
        */
        Char GetChar(int propertyIndex);

        /**
        * Returns the value of a <code>double</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>double</code> value of the specified property.
        * @see #get(int)
        */
        Double GetDouble(int propertyIndex);

        /**
        * Returns the value of a <code>float</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>float</code> value of the specified property.
        * @see #get(int)
        */
        Single GetSingle(int propertyIndex);

        /**
        * Returns the value of a <code>int</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>int</code> value of the specified property.
        * @see #get(int)
        */
        Int32 GetInt32(int propertyIndex);

        /**
        * Returns the value of a <code>long</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>long</code> value of the specified property.
        * @see #get(int)
        */
        Int64 GetInt64(int propertyIndex);

        /**
        * Returns the value of a <code>short</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>short</code> value of the specified property.
        * @see #get(int)
        */
        Int16 GetInt16(int propertyIndex);

        /**
        * Returns the value of a <code>byte[]</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>byte[]</code> value of the specified property.
        * @see #get(int)
        */
        Byte[] GetBytes(int propertyIndex);

        /**
        * Returns the value of a <code>BigDecimal</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>BigDecimal</code> value of the specified property.
        * @see #get(int)
        */
        Decimal GetDecimal(int propertyIndex);

        /**
        * Returns the value of a <code>DataObject</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>DataObject</code> value of the specified property.
        * @see #get(int)
        */
        IDataObject GetDataObject(int propertyIndex);

        /**
        * Returns the value of a <code>Date</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>Date</code> value of the specified property.
        * @see #get(int)
        */
        DateTime GetDate(int propertyIndex);

        /**
        * Returns the value of a <code>String</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>String</code> value of the specified property.
        * @see #get(int)
        */
        String GetString(int propertyIndex);

        /**
        * Returns the value of a <code>List</code> property identified by the specified property index.
        * @param propertyIndex the index of the property.
        * @return the <code>List</code> value of the specified property.
        * @see #get(int)
        */
        List<Object> GetList(int propertyIndex);

        /**
        * @see #getSequence()
        * Returns the value of a <code>Sequence</code> property identified by the specified property index.
        * An implementation may throw an UnsupportedOperationException.
        * @param propertyIndex the index of the property.
        * @return the <code>Sequence</code> value of the specified property.
        * @see #get(int)
        * @deprecated in 2.1.0.
        */
        ISequence GetSequence(int propertyIndex);

        /**
        * Sets the value of a <code>boolean</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetBoolean(int propertyIndex, Boolean value);

        /**
        * Sets the value of a <code>byte</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetByte(int propertyIndex, byte value);

        /**
        * Sets the value of a <code>char</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetChar(int propertyIndex, char value);

        /**
        * Sets the value of a <code>double</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetDouble(int propertyIndex, double value);

        /**
        * Sets the value of a <code>float</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetSingle(int propertyIndex, Single value);

        /**
        * Sets the value of a <code>int</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetInt32(int propertyIndex, Int32 value);

        /**
        * Sets the value of a <code>long</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetInt64(int propertyIndex, Int64 value);

        /**
        * Sets the value of a <code>short</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetInt16(int propertyIndex, Int16 value);

        /**
        * Sets the value of a <code>byte[]</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetBytes(int propertyIndex, byte[] value);

        /**
        * Sets the value of a <code>BigDecimal</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetDecimal(int propertyIndex, Decimal value);

        /**
        * Sets the value of a <code>DataObject</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetDataObject(int propertyIndex, IDataObject value);

        /**
        * Sets the value of a <code>Date</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetDateTime(int propertyIndex, DateTime value);

        /**
        * Sets the value of a <code>String</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        */
        void SetString(int propertyIndex, String value);

        /**
        * Sets the value of a <code>List</code> property identified by the specified property index, to the specified value.
        * @param propertyIndex the index of the property.
        * @param value the new value for the property.
        * @see #set(int, Object)
        * @see #setList(Property, List)
        */
        void SetList(int propertyIndex, List<Object> value);

        /**
        * Returns the value of the given property of this object.
        * <p>
        * If the property is {@link Property#isMany many-valued},
        * the result will be a {@link java.util.List}
        * and each object in the List will be {@link Type#isInstance an instance of} 
        * the property's {@link Property#getType type}.
        * Otherwise the result will directly be an instance of the property's type.
        * @param property the property of the value to fetch.
        * @return the value of the given property of the object.
        * @see #set(Property, Object)
        * @see #unset(Property)
        * @see #isSet(Property)
        */
        Object Get(IProperty property);

        /**
        * Sets the value of the given property of the object to the new value.
        * <p>
        * If the property is {@link Property#isMany many-valued},
        * the new value must be a {@link java.util.List}
        * and each object in that list must be {@link Type#isInstance an instance of} 
        * the property's {@link Property#getType type};
        * the existing contents are cleared and the contents of the new value are added.
        * Otherwise the new value directly must be an instance of the property's type
        * and it becomes the new value of the property of the object.
        * @param property the property of the value to set.
        * @param value the new value for the property.
        * @see #unset(Property)
        * @see #isSet(Property)
        * @see #get(Property)
        */
        void Set(IProperty property, Object value);

        /**
        * Returns whether the property of the object is considered to be set.
        * <p>
        * isSet() for many-valued Properties returns true if the List is not empty and 
        * false if the List is empty. For single-valued Properties it returns true if the Property
        * has been set() and not unset(), and false otherwise.
        * Any call to set() without a call to unset() will cause isSet() to return true, regardless of
        * the value being set. For example, after calling set(property, property.getDefault()) on a
        * previously unset property, isSet(property) will return true, even though the value of
        * get(property) will be unchanged.
        * @param property the property in question.
        * @return whether the property of the object is set.
        * @see #set(Property, Object)
        * @see #unset(Property)
        * @see #get(Property)
        */
        Boolean IsSet(IProperty property);

        /**
        * Unsets the property of the object.
        * <p>
        * If the property is {@link Property#isMany many-valued},
        * the value must be an {@link java.util.List}
        * and that list is cleared.
        * Otherwise, 
        * the value of the property of the object 
        * is set to the property's {@link Property#getDefault default value}.
        * The property will no longer be considered {@link #isSet set}.
        * @param property the property in question.
        * @see #isSet(Property)
        * @see #set(Property, Object)
        * @see #get(Property)
        */
        void Unset(IProperty property);

        /**
        * Returns the value of the specified <code>boolean</code> property.
        * @param property the property to get.
        * @return the <code>boolean</code> value of the specified property.
        * @see #get(Property)
        */
        Boolean GetBoolean(IProperty property);

        /**
        * Returns the value of the specified <code>byte</code> property.
        * @param property the property to get.
        * @return the <code>byte</code> value of the specified property.
        * @see #get(Property)
        */
        byte GetByte(IProperty property);

        /**
        * Returns the value of the specified <code>char</code> property.
        * @param property the property to get.
        * @return the <code>char</code> value of the specified property.
        * @see #get(Property)
        */
        char GetChar(IProperty property);

        /**
        * Returns the value of the specified <code>double</code> property.
        * @param property the property to get.
        * @return the <code>double</code> value of the specified property.
        * @see #get(Property)
        */
        double GetDouble(IProperty property);

        /**
        * Returns the value of the specified <code>float</code> property.
        * @param property the property to get.
        * @return the <code>float</code> value of the specified property.
        * @see #get(Property)
        */
        Single GetSingle(IProperty property);

        /**
        * Returns the value of the specified <code>int</code> property.
        * @param property the property to get.
        * @return the <code>int</code> value of the specified property.
        * @see #get(Property)
        */
        Int32 GetInt32(IProperty property);

        /**
        * Returns the value of the specified <code>long</code> property.
        * @param property the property to get.
        * @return the <code>long</code> value of the specified property.
        * @see #get(Property)
        */
        Int64 GetInt64(IProperty property);

        /**
        * Returns the value of the specified <code>short</code> property.
        * @param property the property to get.
        * @return the <code>short</code> value of the specified property.
        * @see #get(Property)
        */
        Int16 GetInt16(IProperty property);

        /**
        * Returns the value of the specified <code>byte[]</code> property.
        * @param property the property to get.
        * @return the <code>byte[]</code> value of the specified property.
        * @see #get(Property)
        */
        byte[] GetBytes(IProperty property);

        /**
        * Returns the value of the specified <code>BigDecimal</code> property.
        * @param property the property to get.
        * @return the <code>BigDecimal</code> value of the specified property.
        * @see #get(Property)
        */
        Decimal GetDecimal(IProperty property);

        /**
        * Returns the value of the specified <code>DataObject</code> property.
        * @param property the property to get.
        * @return the <code>DataObject</code> value of the specified property.
        * @see #get(Property)
        */
        IDataObject GetDataObject(IProperty property);

        /**
        * Returns the value of the specified <code>Date</code> property.
        * @param property the property to get.
        * @return the <code>Date</code> value of the specified property.
        * @see #get(Property)
        */
        DateTime GetDateTime(IProperty property);

        /**
        * Returns the value of the specified <code>String</code> property.
        * @param property the property to get.
        * @return the <code>String</code> value of the specified property.
        * @see #get(Property)
        */
        String GetString(IProperty property);

        /**
        * Returns the value of the specified <code>List</code> property.
        * The List returned contains the current values.
        * Updates through the List interface operate on the current values of the DataObject.
        * Each access returns the same List object.
        * @param property the property to get.
        * @return the <code>List</code> value of the specified property.
        * @see #get(Property)
        */
        List<Object> GetList(IProperty property);

        /**
        * @see #getSequence()
        * Returns the value of the specified <code>Sequence</code> property.
        * An implementation may throw an UnsupportedOperationException.
        * @param property the property to get.
        * @return the <code>Sequence</code> value of the specified property.
        * @see #get(Property)
        * @deprecated in 2.1.0.
        */
        ISequence GetSequence(IProperty property);

        /**
        * Sets the value of the specified <code>boolean</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetBoolean(IProperty property, Boolean value);

        /**
        * Sets the value of the specified <code>byte</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetByte(IProperty property, byte value);

        /**
        * Sets the value of the specified <code>char</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetChar(IProperty property, char value);

        /**
        * Sets the value of the specified <code>double</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetDouble(IProperty property, double value);

        /**
        * Sets the value of the specified <code>float</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetSingle(IProperty property, Single value);

        /**
        * Sets the value of the specified <code>int</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetInt32(IProperty property, int value);

        /**
        * Sets the value of the specified <code>long</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetInt64(IProperty property, Int64 value);

        /**
        * Sets the value of the specified <code>short</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetInt16(IProperty property, Int16 value);

        /**
        * Sets the value of the specified <code>byte[]</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetBytes(IProperty property, byte[] value);

        /**
        * Sets the value of the specified <code>BigDecimal</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetDecimal(IProperty property, Decimal value);

        /**
        * Sets the value of the specified <code>DataObject</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetDataObject(IProperty property, IDataObject value);

        /**
        * Sets the value of the specified <code>Date</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetDateTime(IProperty property, DateTime value);

        /**
        * Sets the value of the specified <code>String</code> property, to the specified value.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetString(IProperty property, String value);

        /**
        * Sets the value of the specified <code>List</code> property, to the specified value.
        * <p> The new value must be a {@link java.util.List}
        * and each object in that list must be {@link Type#isInstance an instance of} 
        * the property's {@link Property#getType type};
        * the existing contents are cleared and the contents of the new value are added.
        * @param property the property to set.
        * @param value the new value for the property.
        * @see #set(Property, Object)
        */
        void SetList(IProperty property, List<Object> value);

        /**
        * Returns a new {@link DataObject data object} contained by this object using the specified property,
        * which must be a {@link Property#isContainment containment property}.
        * The type of the created object is the {@link Property#getType declared type} of the specified property.
        * @param propertyName the name of the specified containment property.
        * @return the created data object.
        * @see #createDataObject(String, String, String)
        */
        IDataObject CreateDataObject(String propertyName);

        /**
        * Returns a new {@link DataObject data object} contained by this object using the specified property,
        * which must be a {@link Property#isContainment containment property}.
        * The type of the created object is the {@link Property#getType declared type} of the specified property.
        * @param propertyIndex the index of the specified containment property.
        * @return the created data object.
        * @see #createDataObject(int, String, String)
        */
        IDataObject CreateDataObject(int propertyIndex);

        /**
        * Returns a new {@link DataObject data object} contained by this object using the specified property,
        * which must be a {@link Property#isContainment containment property}.
        * The type of the created object is the {@link Property#getType declared type} of the specified property.
        * @param property the specified containment property.
        * @return the created data object.
        * @see #createDataObject(Property, Type)
        */
        IDataObject CreateDataObject(IProperty property);

        /**
        * Returns a new {@link DataObject data object} contained by this object using the specified property,
        * which must be a {@link Property#isContainment containment property}.
        * The type of the created object is specified by the packageURI and typeName arguments.
        * The specified type must be a compatible target for the property identified by propertyName.
        * @param propertyName the name of the specified containment property.
        * @param namespaceURI the namespace URI of the package containing the type of object to be created.
        * @param typeName the name of a type in the specified package.
        * @return the created data object.
        * @see #createDataObject(String)
        * @see DataGraph#getType
        */
        IDataObject CreateDataObject(String propertyName, String namespaceURI, String typeName);

        /**
        * Returns a new {@link DataObject data object} contained by this object using the specified property,
        * which must be a {@link Property#isContainment containment property}.
        * The type of the created object is specified by the packageURI and typeName arguments.
        * The specified type must be a compatible target for the property identified by propertyIndex.
        * @param propertyIndex the index of the specified containment property.
        * @param namespaceURI the namespace URI of the package containing the type of object to be created.
        * @param typeName the name of a type in the specified package.
        * @return the created data object.
        * @see #createDataObject(int)
        * @see DataGraph#getType
        */
        IDataObject CreateDataObject(int propertyIndex, String namespaceURI, String typeName);

        /**
        * Returns a new {@link DataObject data object} contained by this object using the specified property,
        * which must be of {@link Property#isContainment containment type}.
        * The type of the created object is specified by the type argument,
        * which must be a compatible target for the speicifed property.
        * @param property a containment property of this object.
        * @param type the type of object to be created.
        * @return the created data object.
        * @see #createDataObject(int)
        */
        IDataObject CreateDataObject(IProperty property, Type type);

        /**
        * Remove this object from its container and then unset all its non-{@link Property#isReadOnly readOnly} Properties.
        * If this object is contained by a {@link Property#isReadOnly readOnly} {@link Property#isContainment containment property}, its non-{@link Property#isReadOnly readOnly} Properties will be unset but the object will not be removed from its container.
        * All DataObjects recursively contained by {@link Property#isContainment containment Properties} will also be deleted.
        */
        void Delete();

        /**
        * Returns the containing {@link DataObject data object}
        * or <code>null</code> if there is no container.
        * @return the containing data object or <code>null</code>.
        */
        IDataObject GetContainer();

        /**
        * Return the Property of the {@link DataObject data object} containing this data object
        * or <code>null</code> if there is no container.
        * @return the property containing this data object.
        */
        IProperty GetContainmentProperty();

        /**
        * Returns the {@link DataGraph data graph} for this object or <code>null</code> if there isn't one.
        * @return the containing data graph or <code>null</code>.
        */
        IDataGraph getDataGraph();

        /**
        * Returns the data object's type.
        * <p>
        * The type defines the Properties available for reflective access.
        * @return the type.
        */
        IType GetType();

        /**
        * Returns the <code>Sequence</code> for this DataObject.
        * When getType().isSequencedType() == true,
        * the Sequence of a DataObject corresponds to the
        * XML elements representing the values of its Properties.
        * Updates through DataObject and the Lists or Sequences returned
        * from DataObject operate on the same data.
        * When getType().isSequencedType() == false, null is returned.  
        * @return the <code>Sequence</code> or null.
        */
        ISequence GetSequence();
  
        /**
        * Returns a read-only List of the Properties currently used in this DataObject.
        * This list will contain all of the Properties in getType().getProperties()
        * and any Properties where isSet(property) is true.
        * For example, Properties resulting from the use of
        * open or mixed XML content are present if allowed by the Type.
        * the List does not contain duplicates. 
        * The order of the Properties in the List begins with getType().getProperties()
        * and the order of the remaining Properties is determined by the implementation.
        * The same list will be returned unless the DataObject is updated so that 
        * the contents of the List change.
        * @return the List of Properties currently used in this DataObject.
        */
        List<IProperty> GetInstanceProperties();

        /**
        * Returns the named Property from the current instance properties,
        * or null if not found.  The instance properties are getInstanceProperties().  
        * @param propertyName the name of the Property
        * @return the named Property from the DataObject's current instance properties, or null.
        */
        IProperty GetInstanceProperty(String propertyName);

        /**
        * @deprecated replaced by {@link #getInstanceProperty(String)} in 2.1.0
        */
        IProperty GetProperty(String propertyName);

        /**
        * Returns the root {@link DataObject data object}.
        * @return the root data object.
        */
        IDataObject GetRootObject();

        /**
        * Returns the ChangeSummary with scope covering this dataObject, or null
        * if there is no ChangeSummary. 
        * @return the ChangeSummary with scope covering this dataObject, or null.
        */
        IChangeSummary getChangeSummary();

        /**
        * Removes this DataObject from its container, if any.
        * Same as 
        *  getContainer().getList(getContainmentProperty()).remove(this) or
        *  getContainer().unset(getContainmentProperty())
        * depending on getContainmentProperty().isMany() respectively.
        */
        void Detach();
    }



}

