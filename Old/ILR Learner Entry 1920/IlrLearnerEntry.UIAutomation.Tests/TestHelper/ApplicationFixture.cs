using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;

namespace IlrLearnerEntry.UIAutomation.Tests.TestHelper
{
    public class ApplicationFixture : IDisposable
    {

        const string ApplicationProcessName1 = "ILRLearnerEntry1920";

        public string ApplicationPath { get; private set; }

        public ApplicationFixture()
        {
             this.ApplicationPath = ConfigurationManager.AppSettings["Applicationpath"].ToString();

            string currentPath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            string rootpath = Uri.UnescapeDataString(Path.GetDirectoryName(currentPath));

            this.ApplicationPath = Path.Combine(rootpath, ApplicationPath);

            Application application = Application.Launch(ApplicationPath);
            this.Application = application;
            IlrLearnerEntry.UIAutomation.Tests.WindowObjects.Windows.Init(application);
            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
            KillRunningApplication();
            removeInternalIlrFile();
        }

        public Application Application { get; private set; }

        private void KillRunningApplication()
        {
            Process[] processes = Process.GetProcessesByName(ApplicationProcessName1);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }

        private void removeInternalIlrFile()
        {
            string folder = Path.GetDirectoryName(ApplicationPath);
            string[] files = Directory.GetFiles(folder, "*.ilr", SearchOption.AllDirectories);
            foreach (string file in files)
                File.Delete(file);
        }
    }
}
