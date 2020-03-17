namespace VinylExchange.Services.Logging
{
    #region

    using System;
    using System.Runtime.CompilerServices;

    using Microsoft.Extensions.Logging;

    #endregion

    public class LoggerService : ILoggerService
    {
        private readonly ILogger logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            this.logger = logger;
        }

        public void LogException(
            Exception ex,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null)
        {
            this.logger.LogError(
                $"{DateTime.Now} -- Unhandled Exception at {callerFilePath}--{callerMemberName}  --> {ex}");
        }
    }
}