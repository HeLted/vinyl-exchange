using System;
using System.Runtime.CompilerServices;

namespace VinylExchange.Services.Logging
{
    public interface ILoggerService
    {
        void LogException(Exception ex,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null);
    }
}
