using System;
using System.Collections.Generic;

namespace Paralect.Schematra.SDO
{
    /**
     * A representation of a Property in the {@link Type type} of a {@link DataObject data object}.
     */
    public interface IProperty
    {
        /**
         * Returns the name of the Property.
         * @return the Property name.
         */
        String GetName();

        /**
         * Returns the type of the Property.
         * @return the Property type.
         */
        IType GetType();

        /**
         * Returns whether the Property is many-valued.
         * @return <code>true</code> if the Property is many-valued.
         */
        Boolean IsMany();

        /**
         * Returns whether the Property is containment, i.e., whether it represents by-value composition.
         * @return <code>true</code> if the Property is containment.
         */
        Boolean IsContainment();

        /**
         * Returns the containing type of this Property.
         * @return the Property's containing type.
         * @see Type#getProperties()
         */
        Type GetContainingType();

        /**
         * Returns the default value this Property will have in a {@link DataObject data object} where the Property hasn't been set.
         * @return the default value.
         */
        Object GetDefault();

        /**
         * Returns true if values for this Property cannot be modified using the SDO APIs.
         * When true, DataObject.set(Property property, Object value) throws an exception.
         * Values may change due to other factors, such as services operating on DataObjects.
         * @return true if values for this Property cannot be modified.
         */
        Boolean IsReadOnly();

        /**
         * Returns the opposite Property if the Property is bi-directional or null otherwise.
         * @return the opposite Property if the Property is bi-directional or null
         */
        IProperty GetOpposite();

        /**
         * Returns a list of alias names for this Property.
         * @return a list of alias names for this Property.
         */
        List<String> GetAliasNames();

        /**
         * Returns whether or not instances of this property can be set to null. The effect of calling set(null) on a non-nullable
         * property is not specified by SDO.
         * @return true if this property is nullable.
         */
        Boolean IsNullable();

        /**
         * Returns whether or not this is an open content Property. 
         * 
         * Open Content Property is just properties that wasn't defined in the IType.
         * 
         * From (3.1.9 of Java-SDO-Spec-v2.1.0-FINAL.pdf)
         *    DataObjects can have two kinds of Properties:
         *     1. Those specified by their Type (see Type)
         *     2. Those not specified by their Type. These additional properties are called open content.
         *       
         * @return true if this property is an open content Property.
         */
        Boolean IsOpenContent();

        /**
         * Returns a read-only List of instance Properties available on this Property.
         * <p>
         * This list includes, at a minimum, any open content properties (extensions) added to
         * the object before {@link commonj.sdo.helper.TypeHelper#define(DataObject) defining
         * the Property's Type}. Implementations may, but are not required to in the 2.1 version
         * of SDO, provide additional instance properties.
         * @return the List of instance Properties on this Property.
         */
        List<IProperty> GetInstanceProperties();

        /**
         * Returns the value of the specified instance property of this Property.
         * @param property one of the properties returned by {@link #getInstanceProperties()}.
         * @return the value of the specified property.
         * @see DataObject#get(Property)
         */
        Object Get(IProperty property);

    }
}
