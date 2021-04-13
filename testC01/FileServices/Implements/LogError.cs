using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace testC01.FileServices.Implements
{
    public class LogError : ILogError
    {
        private readonly ILogger<LogError> _logger;

        public LogError(ILogger<LogError> logger)
        {
            _logger = logger;
        }
        public void LogErrorException<T>(Exception e, string param,T value)
        {
            _logger.LogError(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + "[Error]");
            _logger.LogError("LastContent ={0} :"+param,value);
            _logger.LogError("Message = " + e.Message);
            _logger.LogError("StackTrace =" + e.StackTrace);
            _logger.LogError("TargetSite =" + e.TargetSite);

        }
    }
}
