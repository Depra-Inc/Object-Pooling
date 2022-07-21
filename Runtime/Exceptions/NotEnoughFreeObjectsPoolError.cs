using System;

namespace Depra.ObjectPooling.Runtime.Exceptions
{
    public class NotEnoughFreeObjectsPoolError : Exception
    {
        public NotEnoughFreeObjectsPoolError(object poolKey, Type objectType) : base(
            $"Error trying to free a object {objectType} from pool {poolKey}")
        {
        }
    }
}