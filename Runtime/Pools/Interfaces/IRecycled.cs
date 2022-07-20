namespace Depra.ObjectPooling.Runtime.Pools.Interfaces
{
    /// <summary>
    /// Interface for hooking into a object recycle phase.
    /// </summary>
    public interface IRecycled
    {
        /// <summary>
        /// Invoked when the object is reused.
        /// </summary>
        void OnRecycle();
    }
}