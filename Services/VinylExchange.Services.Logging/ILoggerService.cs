using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace VinylExchange.Services.Logging
{
    public interface ILoggerService
    {
        void LogException(Exception ex,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null);
    }
}
