namespace VinylExchange.Services.Logging
{
    #region

    using System;
    using System.Runtime.CompilerServices;

    #endregion

    public interface ILoggerService
    {
        void LogException(
            Exception ex,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null);
    }
}