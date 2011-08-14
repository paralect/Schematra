using System;

namespace Paralect.Schemata.SDO
{
    /**
     * A sequence is a heterogeneous list of {@link Property properties} and corresponding values.
     * It represents an ordered arbitrary mixture of data values from more than one property of a {@link DataObject data object}.
     */
    public interface ISequence
    {
        /**
         * Returns the number of entries in the sequence.
         * @return the number of entries.
         */
        int Size();

        /**
         * Returns the property for the given entry index.
         * Returns <code>null</code> for mixed text entries.
         * @param index the index of the entry.
         * @return the property or <code>null</code> for the given entry index.
         */
        IProperty GetProperty(int index);

        /**
         * Returns the property value for the given entry index.
         * @param index the index of the entry.
         * @return the value for the given entry index.
         */
        Object GetValue(int index);

        /**
         * Sets the entry at a specified index to the new value.
         * @param index the index of the entry.
         * @param value the new value for the entry.
         */
        Object SetValue(int index, Object value);

        /**
         * Adds a new entry with the specified property name and value
         * to the end of the entries.
         * @param propertyName the name of the entry's property.
         * @param value the value for the entry.
         */
        Boolean Add(String propertyName, Object value);

        /**
         * Adds a new entry with the specified property index and value
         * to the end of the entries.
         * @param propertyIndex the index of the entry's property.
         * @param value the value for the entry.
         */
        Boolean Add(int propertyIndex, Object value);

        /**
         * Adds a new entry with the specified property and value
         * to the end of the entries.
         * @param property the property of the entry.
         * @param value the value for the entry.
         */
        Boolean Add(IProperty property, Object value);

        /**
         * Adds a new entry with the specified property name and value
         * at the specified entry index.
         * @param index the index at which to add the entry.
         * @param propertyName the name of the entry's property.
         * @param value the value for the entry.
         */
        void Add(int index, String propertyName, Object value);

        /**
         * Adds a new entry with the specified property index and value
         * at the specified entry index.
         * @param index the index at which to add the entry.
         * @param propertyIndex the index of the entry's property.
         * @param value the value for the entry.
         */
        void Add(int index, int propertyIndex, Object value);

        /**
         * Adds a new entry with the specified property and value
         * at the specified entry index.
         * @param index the index at which to add the entry.
         * @param property the property of the entry.
         * @param value the value for the entry.
         */
        void Add(int index, IProperty property, Object value);

        /**
         * Removes the entry at the given entry index.
         * @param index the index of the entry.
         */
        void Remove(int index);

        /**
         * Moves the entry at <code>fromIndex</code> to <code>toIndex</code>.
         * @param toIndex the index of the entry destination.
         * @param fromIndex the index of the entry to move.
         */
        void Move(int toIndex, int fromIndex);

        /**
         * Adds a new text entry to the end of the Sequence.
         * @param text value of the entry.
         */
        void AddText(String text);

        /**
         * Adds a new text entry at the given index.
         * @param index the index at which to add the entry.
         * @param text value of the entry.
         */
        void AddText(int index, String text);
    }
}

