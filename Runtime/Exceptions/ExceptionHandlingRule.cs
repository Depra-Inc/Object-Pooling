using System;

namespace Depra.ObjectPooling.Runtime.Exceptions
{
    public abstract class ExceptionHandlingRule
    {
        internal abstract void HandleException(Exception exception);
    }
}