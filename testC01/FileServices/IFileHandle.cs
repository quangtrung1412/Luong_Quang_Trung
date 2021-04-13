using System;
using System.Collections.Generic;
using System.Text;

namespace testC01.FileServices
{
    public interface IFileHandle
    {
        //lấy tất cả các dòng theo filter
        List<string> GetAllLineOfFile(string filePath, string[] filter);
        //lay tất cả dòng của các File add vào 1 list
        List<string> GetAllLineOfFIleInputFoder(string foderPath, string[] filter);
        //ghi File mới
        void WriteToNewFile(string filePath, List<string> allLines);
        //in timerun ra file log
        //void WriteTimeToFile(DateTime startTime, DateTime stopTime, string path);
    }
}
