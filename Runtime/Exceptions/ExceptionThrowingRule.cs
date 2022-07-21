using System;

namespace Depra.ObjectPooling.Runtime.Exceptions
{
    public class ExceptionThrowingRule : ExceptionHandlingRule
    {
        internal override void HandleException(Exception exception) => throw exception;
    }
}