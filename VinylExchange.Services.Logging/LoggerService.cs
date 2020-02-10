using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

            //StackFrame frame = new StackFrame();

            //string controllerName = frame.GetMethod().DeclaringType.Name;

            //string actionName = frame.GetMethod().Name;
                        

            logger.LogError($"{DateTime.Now} -- Unhandled Exception at {callerFilePath}--{callerMemberName}  --> {ex.ToString()}");
        }

    }
}
