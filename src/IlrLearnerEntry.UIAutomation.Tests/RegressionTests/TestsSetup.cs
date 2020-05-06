using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using TestStack.White;


namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{

    
    public class TestsSetup
    {

       // const string ApplicationPath = @"E:\Downloads\ILRLearnerTool_1718.2.3\ILRLearnerEntry1819.exe";
        const string ApplicationProcessName = "ILR Learner Entry - 1920";
        const string ApplicationProcessName1 = "ILRLearnerEntry1920";


        public  TestsSetup()
        {
            Reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();

           // KillRunningApplication();
           // Application application = Application.Launch(ApplicationPath);
           // IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Init(application);
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
