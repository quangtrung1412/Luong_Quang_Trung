using System;
using System.Collections.Generic;
using System.Text;
using testC01.FileServices.Implements;
using Xunit;

namespace XUnitTest.Test_FileServices.Test_Implements
{
    public class Test_FileHandle
    {
        private readonly string foderPath= "D:\\thuctap\\12+13__33__20210322085224";
        private readonly string[] filter = { "55=VN000000AGR5", "35=X" };
        private readonly string filePath= "D:\\thuctap\\12+13__33__20210322085224\\STO_RTSDIST12_DIST_DATA.dat";
        FileHandle FH = new FileHandle();
        [Fact]
        public void Test_GetAllLineOfFile()

        {  
            List<string> allLine = FH.GetAllLineOfFile(filePath,filter);
            Assert.Contains(allLine, item => item.Contains("8=FIX.4.4☺9=671☺35=X☺49=VNMGW☺56=99999☺34=35☺52=20190517 09:00:00.066☺30001=STO☺20004=G7☺336=99☺55=VN000000AGR5☺75=20190221☺60=090000028☺30521=0☺30522=0☺30523=0☺30524=0☺268=10☺83=1☺279=0☺269=1☺290=1☺270=0.0☺271=0☺346=0☺30271=0☺83=2☺279=0☺269=0☺290=1☺270=0.0☺271=0☺346=0☺30271=0☺83=3☺279=0☺269=1☺290=2☺270=0.0☺271=0☺346=0☺30271=0☺83=4☺279=0☺269=0☺290=2☺270=0.0☺271=0☺346=0☺30271=0☺83=5☺279=0☺269=1☺290=3☺270=0.0☺271=0☺346=0☺30271=0☺83=6☺279=0☺269=0☺290=3☺270=0.0☺271=0☺346=0☺30271=☺83=7☺279=0☺269=1☺290=4☺270=0.0☺271=0☺346=0☺30271=0☺83=8☺279=0☺269=0☺290=4☺270=0.0☺271=0☺346=0☺30271=0☺83=9☺279=0☺269=1☺290=5☺270=0.0☺271=0☺346=0☺30271=0☺83=10☺279=0☺269=0☺290=5☺270=0.0☺271=0☺346=0☺30271=0☺10=155☺"));
        }
        [Fact]
        public void Test_GetAllLineOfFIleInputFoder()
        {
            List<string> allLine = FH.GetAllLineOfFIleInputFoder(foderPath, filter);
            Assert.Equal(allLine.Count, 12);
        }
    }
}
