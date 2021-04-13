using System;
using System.Collections.Generic;
using System.Text;

namespace testC01.FileServices
{
    public interface IUtilHandle
    {
        //sắp xếp theo thẻ 52
        List<string> Sort(List<string> allLines);
        //chuyển đổi value thẻ 52 ra miligiay
        double ConvertToSeconds(string timeString);
    }
}
