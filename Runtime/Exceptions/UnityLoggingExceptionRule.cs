using System;
using UnityEngine;

namespace Depra.ObjectPooling.Runtime.Exceptions
{
    public class UnityLoggingExceptionRule : ExceptionHandlingRule
    {
        internal override void HandleException(Exception exception) => Debug.LogWarning(exception);
    }
}