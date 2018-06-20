using System;
using System.Linq;

using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.Utility;

namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public static class Windows
    {
        private static Application _application;

        #region ConfigItems
        public const string MainWindowTitle = "ILR Learner Entry - 1819";
        #endregion


        public static MainWindow Main
        {
            get { return new MainWindow(GetWindow(MainWindowTitle)); }
        }

        public static LearnerWindow LearnerWindow
        {
            get { return new LearnerWindow(GetWindow(MainWindowTitle)); }
        }

        public static HomeWindow HomeWindow
        {
            get { return new HomeWindow(GetWindow(MainWindowTitle)); }
        }


        public static void Init(Application application)
        {
            _application = application;
        }

        private static Window GetWindow(string title)
        {
            return Retry.For(
                () => _application.GetWindows().First(x => x.Title.Contains(title)),
                TimeSpan.FromSeconds(5));
        }
    }
}
