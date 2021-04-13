using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace testC01.FileServices.Implements
{
    public class FileHandle : IFileHandle 
    {
        private readonly ILogError _logError;
        private readonly ILogger<FileHandle> _logger;

        public FileHandle()
        {
        }

        public FileHandle(ILogger<FileHandle> logger, ILogError logError)
        {
            _logError = logError;
            _logger = logger;
        }
        public List<string> GetAllLineOfFile(string filePath, string[] filter)
        {
            List<string> allLine = new List<string>();
            StreamReader reader = new StreamReader(filePath);
            string line;
            while((line = reader.ReadLine())!= null)
            {
                try
                {
                    if (line.Contains(filter[0]) && line.Contains(filter[1]))
                    {
                        allLine.Add(line);
                    }
                }
                catch (ArgumentNullException ane)
                {
                    _logError.LogErrorException(ane, nameof(filter), (filter == null) ? "NULL" : "");
                }
            }
            return allLine;
        }

        public List<string> GetAllLineOfFIleInputFoder(string foderPath, string[] filter)
        {
            List<string> allLines = new List<string>();
            List<string> listPath = new List<string>();
            try
            {
                listPath = Directory.GetFiles(foderPath).ToList();   
            }
            catch(Exception e)
            {
                _logError.LogErrorException(e, nameof(foderPath), foderPath == null ? "NULL" : foderPath);  
                
            }
            foreach (var path in listPath)
            {
                var lines = GetAllLineOfFile(path, filter);
                allLines.AddRange(lines);
            }
            return allLines;
        }

        //public void WriteTimeToFile(DateTime startTime, DateTime stopTime, string path)
        //{
        //    double Milliseconds = stopTime.Subtract(startTime).TotalMilliseconds;
        //        using (StreamWriter sw = File.AppendText(path))
        //        {
        //            sw.WriteLine("Begin=" + startTime);
        //            sw.WriteLine("End=" + stopTime);
        //            sw.WriteLine("ElapsedMilliseconds=" + Milliseconds);
        //        }     
        //}

        public void WriteToNewFile(string filePath, List<string> allLines)
        {
            for (int i = 0; i < allLines.Count; i++)
            {
                string line="";
                try
                {
                    line = Regex.Replace(allLines[i], @"34=\d+", "34=" + (i + 1));
                }
                catch(ArgumentNullException ane )
                {
                    _logError.LogErrorException(ane, nameof(allLines), (allLines == null) ? "NULL" : "");
                }
                try
                {
                    File.AppendAllText(filePath, line + Environment.NewLine);
                    Console.WriteLine(line);
                }
                catch (ArgumentNullException ane)
                {
                    _logError.LogErrorException(ane, nameof(filePath), (filePath == null || filePath == "") ? "NULL" : "");
                }
                catch (DirectoryNotFoundException dne)
                {
                    _logError.LogErrorException(dne, nameof(filePath), filePath);
                }

            }
        }
    }
}
