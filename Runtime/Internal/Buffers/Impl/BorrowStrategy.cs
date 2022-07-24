namespace Depra.ObjectPooling.Runtime.Internal.Buffers.Impl
{
    public enum BorrowStrategy
    {
        /// <summary>
        /// Last in first out (stack).
        /// </summary>
        LIFO,

        /// <summary>
        /// First in first out (queue).
        /// </summary>
        FIFO,

        /// <summary>
        /// Random out.
        /// </summary>
        Random
    }
}