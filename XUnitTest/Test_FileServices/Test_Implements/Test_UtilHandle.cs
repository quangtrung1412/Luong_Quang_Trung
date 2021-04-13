using System;
using System.Collections.Generic;
using System.Text;
using testC01.FileServices.Implements;
using Xunit;

namespace XUnitTest.Test_FileServices.Test_Implements
{
    public class Test_UtilHandle
    {
        public const string DateTimeString = "52=20190517 09:00:00.065";
        UtilHandle UH = new UtilHandle();
        
        public void Test_ConvertToSeconds()
        {
            double expected = 63693680400.065;
            double result = UH.ConvertToSeconds(DateTimeString);
            Assert.Equal(expected, result);
        }
        
    }
}
