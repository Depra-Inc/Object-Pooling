namespace Depra.ObjectPooling.Runtime.Processors.Abstract
{
    public abstract class InstanceProcessor<TSource>
    {
        /// <summary>
        /// Create new Instance of Type T using source object.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <returns>New instance.</returns> 
        public abstract TSource CreateInstance(object key);

        /// <summary>
        /// Destroy Instance of Type T.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <param name="instance">Instance to destroy.</param>
        public abstract void DestroyInstance(object key, TSource instance);

        /// <summary>
        /// Called when instance is enabled.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <param name="instance">Instance to enable.</param>
        public virtual void OnEnableInstance(object key, TSource instance)
        {
        }

        /// <summary>
        /// Called when instance is disabled.
        /// </summary> 
        /// <param name="key">Unique identifier for Pool.</param>
        /// <param name="instance">Instance to disable.</param>
        public virtual void OnDisableInstance(object key, TSource instance)
        {
        }
    }
}