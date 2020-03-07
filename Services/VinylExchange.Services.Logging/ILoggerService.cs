namespace VinylExchange.Services.Logging
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ILoggerService
    {
        void LogException(
            Exception ex,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null);
    }
}