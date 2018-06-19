using IlrLearnerEntry.UIAutomation.Tests.WindowObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestStack.White;
using Xunit;


namespace IlrLearnerEntry.UIAutomation.Tests.Tests
{

    
    public class Tests
    {

        const string ApplicationPath = @"E:\Downloads\ILRLearnerTool_1718.2.3\ILRLearnerEntry1819.exe";
        const string ApplicationProcessName = "ILR Learner Entry - 1819";
        const string ApplicationProcessName1 = "ILRLearnerEntry1819";


        public  Tests()
        {
           // KillRunningApplication();
           // Application application = Application.Launch(ApplicationPath);
           // IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Init(application);
        }

        private void KillRunningApplication()
        {
            Process[] processes = Process.GetProcessesByName(ApplicationProcessName1);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}
