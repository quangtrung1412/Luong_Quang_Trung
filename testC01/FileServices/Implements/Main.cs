using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testC01.FileServices.Implements
{
    public class Main:IMain
    {
        private readonly IFileHandle _fileHandle;
        private readonly IUtilHandle _utilHandle;
        private readonly IConfiguration _config;
        //truyen ham main
        private readonly ILogger<Main> _logger;
        private DateTime startTime;
        private DateTime stopTime;
        public Main(IFileHandle fileHandle , IUtilHandle utilHandle, IConfiguration config , ILogger<Main> logger)
        {
            _fileHandle = fileHandle;
            _utilHandle = utilHandle;
            _config = config;
            _logger = logger;
        }
        public void Run()
        {
            startTime = DateTime.Now;
            //rename filename Nlog
            var target = (FileTarget)LogManager.Configuration.FindTargetByName("logFile");
            target.FileName = _config["FolderOutput"] + "\\" + _config["FileLog"];
            LogManager.ReconfigExistingLoggers();
            //rename filename Nlog logerror
            var targetError = (FileTarget)LogManager.Configuration.FindTargetByName("logFileError");
            targetError.FileName = _config["FolderOutput"] + "\\" + "Error-${shortdate}.txt";
            LogManager.ReconfigExistingLoggers();
            _logger.LogInformation("Begin = " + startTime.ToString());
            string[] filter = _config.GetSection("Filter").Get<string[]>();
            var allLines = _fileHandle.GetAllLineOfFIleInputFoder(_config["Folder"], filter);
            allLines = _utilHandle.Sort(allLines);
            _fileHandle.WriteToNewFile(_config["FolderOutput"]+"\\"+_config["FileOutput"] ,allLines);       
            stopTime = DateTime.Now;
            _logger.LogInformation("End = " + stopTime.ToString());
            double totalMilisecond = stopTime.Subtract(startTime).TotalMilliseconds;
            _logger.LogInformation("ElapsedMilliseconds="+totalMilisecond);
            //_fileHandle.WriteTimeToFile(startTime, stopTime, _config["FileLog"]);
        }
    }
}
