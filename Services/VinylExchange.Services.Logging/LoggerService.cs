using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace VinylExchange.Services.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            this.logger = logger;
        }

        public  void LogException(Exception ex,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null)
        {                                 

            logger.LogError($"{DateTime.Now} -- Unhandled Exception at {callerFilePath}--{callerMemberName}  --> {ex.ToString()}");
        }

    }
}
