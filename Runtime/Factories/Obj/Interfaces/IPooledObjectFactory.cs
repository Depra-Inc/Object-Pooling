namespace Depra.ObjectPooling.Runtime.Factories.Obj.Interfaces
{
    public interface IPooledObjectFactory<TSource>
    {
        /// <summary>
        /// Create new Instance of Type T using source object.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <returns>New instance.</returns> 
        TSource CreateObject(object key);

        /// <summary>
        /// Destroy Instance of Type T.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <param name="instance">Instance to destroy.</param>
        void DestroyObject(object key, TSource instance);

        /// <summary>
        /// Called when instance is enabled.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <param name="instance">Instance to enable.</param>
        void OnEnableObject(object key, TSource instance);

        /// <summary>
        /// Called when instance is disabled.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <param name="instance">Instance to disable.</param>
        void OnDisableObject(object key, TSource instance);
    }
}