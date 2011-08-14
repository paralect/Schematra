using System;
using System.Collections.Generic;

namespace Paralect.Schematra.SDO
{
    /**
     * A setting encapsulates a {@link Property property} and a corresponding single value of the property's {@link Property#getType type}.
     */
    public interface IChangeSummarySetting
    {
        /**
         * Returns the property of the setting.
         * @return the setting property.
         */
        IProperty GetProperty();

        /**
         * Returns the value of the setting.
         * @return the setting value.
         */
        Object GetValue();

        /**
         * Returns whether or not the property is set.
         * @return <code>true</code> if the property is set.
         */
        Boolean IsSet();
    }


    /**
     * A change summary is used to record changes to DataObjects,
     * allowing applications to efficiently and incrementally update back-end storage when required.
     */
    public interface IChangeSummary
    {
        /**
         * Indicates whether change logging is on (<code>true</code>) or off (<code>false</code>).
         * @return <code>true</code> if change logging is on.
         * @see #beginLogging
         * @see #endLogging
         */
        Boolean IsLogging();

        /**
         * Returns the {@link DataGraph data graph} associated with this change summary or null.
         * @return the data graph.
         * @see DataGraph#getChangeSummary
         */
        IDataGraph GetDataGraph();

        /**
         * Returns a list consisting of all the {@link DataObject data objects} that have been changed while {@link #isLogging logging}.
         * <p>
         * The {@link #isCreated new} and {@link #isModified modified} objects in the List are references to objects
         * associated with this ChangeSummary. 
         * The {@link #isDeleted deleted} objects in the List are references to objects 
         * at the time that event logging was enabled; 
         * <p> Each changed object must have exactly one of the following methods return true:
         *   {@link #isCreated isCreated}, 
         *   {@link #isDeleted isDeleted}, or
         *   {@link #isModified isModified}.
         * @return a list of changed data objects.
         * @see #isCreated(DataObject)
         * @see #isDeleted(DataObject)
         * @see #isModified(DataObject)
         */
        List<IDataObject> GetChangedDataObjects();

        /**
         * Returns whether or not the specified data object was created while {@link #isLogging logging}.
         * Any object that was added to the scope
         * but was not in the scope when logging began, 
         * will be considered created.
         * @param dataObject the data object in question.
         * @return <code>true</code> if the specified data object was created.
         * @see #getChangedDataObjects
         */
        Boolean IsCreated(IDataObject dataObject);

        /**
         * Returns whether or not the specified data object was deleted while {@link #isLogging logging}.
         * Any object that is not in scope but was in scope when logging began 
         * will be considered deleted.
         * @param dataObject the data object in question.
         * @return <code>true</code> if the specified data object was deleted.
         * @see #getChangedDataObjects
         */
        Boolean isDeleted(IDataObject dataObject);

        /**
         * Returns a list of {@link ChangeSummary.Setting settings} 
         * that represent the property values of the given <code>dataObject</code>
         * at the point when logging {@link #beginLogging() began}.
         * <p>In the case of a {@link #isDeleted(DataObject) deleted} object, 
         * the List will include settings for all the Properties.
         * <p> An old value setting indicates the value at the
         * point logging begins.  A setting is only produced for 
         * {@link #isModified modified} objects if 
         * either the old value differs from the current value or
         * if the isSet differs from the current value. 
         * <p> No settings are produced for {@link #isCreated created} objects.
         * @param dataObject the object in question.
         * @return a list of settings.
         * @see #getChangedDataObjects
         */
        List<IChangeSummarySetting> GetOldValues(IDataObject dataObject);

        /**
         * Clears the List of {@link #getChangedDataObjects changes} and turns change logging on.
         * No operation occurs if logging is already on.
         * @see #endLogging
         * @see #isLogging
         */
        void BeginLogging();

        /**
         * An implementation that requires logging may throw an UnsupportedOperationException.
         * Turns change logging off.  No operation occurs if logging is already off.
         * @see #beginLogging
         * @see #isLogging
         */
        void EndLogging();


        /**
         * Returns whether or not the specified data object was updated while {@link #isLogging logging}.
         * An object that was contained in the scope when logging began
         * and remains in the scope when logging ends will be considered potentially modified.
         * <p> An object considered modified must have at least one old value setting.
         * @param dataObject the data object in question.
         * @return <code>true</code> if the specified data object was modified.
         * @see #getChangedDataObjects
         */
        Boolean IsModified(IDataObject dataObject);

        /**
         * Returns the ChangeSummary root DataObject - the object from which 
         * changes are tracked.  
         * When a DataGraph is used, this is the same as getDataGraph().getRootObject().
         * @return the ChangeSummary root DataObject
         */
        IDataObject GetRootObject();

        /**
         * Returns a {@link ChangeSummary.Setting setting} for the specified property
         * representing the property value of the given <code>dataObject</code>
         * at the point when logging {@link #beginLogging() began}.
         * <p>Returns null if the property was not modified and 
         * has not been {@link #isDeleted(DataObject) deleted}. 
         * @param dataObject the object in question.
         * @param property the property of the object.
         * @return the Setting for the specified property.
         * @see #getChangedDataObjects
         */
        IChangeSummarySetting GetOldValue(IDataObject dataObject, IProperty property);

        /**
         * Returns the value of the {@link DataObject#getContainer container} data object
         * at the point when logging {@link #beginLogging() began}.
         * @param dataObject the object in question.
         * @return the old container data object.
         */
        IDataObject GetOldContainer(IDataObject dataObject);

        /**
         * Returns the value of the {@link DataObject#getContainmentProperty containment property} data object property
         * at the point when logging {@link #beginLogging() began}.
         * @param dataObject the object in question.
         * @return the old containment property.
         */
        IProperty GetOldContainmentProperty(IDataObject dataObject);

        /**
         * Returns the value of the {@link DataObject#getSequence sequence} for the data object
         * at the point when logging {@link #beginLogging() began}.
         * @param dataObject the object in question.
         * @return the old containment property.
         */
        ISequence GetOldSequence(IDataObject dataObject);

        /**
         * This method is intended for use by service implementations only.
         * Undoes all changes in the log to restore the tree of 
         * DataObjects to its original state when logging began.
         * isLogging() is unchanged.  The log is cleared.
         * @see #beginLogging
         * @see #endLogging
         * @see #isLogging
         */
        void UndoChanges();

    }
    
}

