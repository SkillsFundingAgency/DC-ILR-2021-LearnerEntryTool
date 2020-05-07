using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using TestStack.White;

namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{
    public class TestsSetup
    {
        public  TestsSetup()
        {
            Reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        }

        public string Reportpath { get; private set; }

        public void TakeScreenShot()
        {
            string methodName;
            StackTrace stackTrace = new StackTrace();
            methodName = stackTrace.GetFrame(1).GetMethod().Name;
            Desktop.TakeScreenshot(Path.Combine(Reportpath, methodName + ".jpeg"), ImageFormat.Jpeg);
        }
    }
}
