using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace testC01.FileServices.Implements
{
    public class UtilHandle : IUtilHandle
    {
        
        private const string regex = @"52=\d{8}\s+\d{2}:\d{2}:[\d\.]+";
        private const string regex34 = @"34=[\d]+";
        private readonly ILogger<UtilHandle> _logger;
        private readonly ILogError _logError;

        public UtilHandle(ILogger<UtilHandle> logger,ILogError logError)
        {
            _logger = logger;
            _logError = logError;
        }

        public UtilHandle()
        {
        }

        // chuyển đổi giá trị thẻ 52 sang mili giay
        public double ConvertToSeconds(string timeString)
        {
                timeString = Regex.Replace(timeString, @"[\s+:]", "");
                int year = int.Parse(timeString.Substring(0, 4));
                int month = int.Parse(timeString.Substring(4, 2));
                int day = int.Parse(timeString.Substring(6, 2));
                int hour = int.Parse(timeString.Substring(8, 2));
                int minute = int.Parse(timeString.Substring(10, 2));
                double second = double.Parse(timeString.Substring(12));
                // ngày hiện tại trừ ngày nhỏ nhất
                double totalSeconds = new DateTime(year, month, day).Subtract(DateTime.MinValue).TotalSeconds + hour * 60 * 60 + minute * 60 + second;
                
           
            
            return totalSeconds;
        }
        
        //so sánh giá trị thẻ 52
        private int compareLine(string x, string y)
        {
            string tag52_1="";
            string tag52_2="";
            try
            {
                tag52_1 = Regex.Match(x, regex).ToString().Substring(3);
                tag52_2 = Regex.Match(y, regex).ToString().Substring(3);
            }
            catch (ArgumentNullException ane)
            {
                _logError.LogErrorException(ane, nameof(regex), (regex == null || regex == "") ? "NULL" : "");
                Environment.Exit(0);
            }
            
            double totalSeconds1 = ConvertToSeconds(tag52_1);
            double totalSeconds2 = ConvertToSeconds(tag52_2);
            if (totalSeconds1 > totalSeconds2)
            {
                return 1;
            }
            if (totalSeconds1 < totalSeconds2)
            {
                return -1;
            }
            return 0;
        } 
        private int compareLine34(string x, string y)
        {
            int compare52 = compareLine(x, y);
            int tag34_1 = 0;
            int tag34_2 = 0;
            try 
            {
                 tag34_1 = int.Parse(Regex.Match(x, regex34).ToString().Substring(3));
                 tag34_2 = int.Parse(Regex.Match(y, regex34).ToString().Substring(3));
            }
            catch (ArgumentNullException ane)
            {
                _logError.LogErrorException(ane, nameof(regex34), (regex34 == null || regex34 == "") ? "NULL" : "");
                Environment.Exit(0);
            }
            if(compare52 == 0)
            {
                if (tag34_1 > tag34_2)
                {
                    return 1;
                }
                if (tag34_2 > tag34_1)
                {
                    return -1;
                }
                return 0;
            }
            else
            {
                return compare52;
            }
            
        }
        //sắp xếp theo giá trị thẻ 52 
        public List<string> Sort(List<string> allLines)
        {

            allLines.Sort(compareLine34);
            return allLines;
        }
    }
}
