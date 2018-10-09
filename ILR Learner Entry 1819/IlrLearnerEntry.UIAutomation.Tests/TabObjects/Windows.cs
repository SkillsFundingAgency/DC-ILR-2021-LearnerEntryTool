using IlrLearnerEntry.UIAutomation.Tests.TabObjects.LearnersObjects;
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

        public static Learners_LearnerListObjects LearnerWindow
        {
            get { return new Learners_LearnerListObjects(GetWindow(MainWindowTitle)); }
        }

        public static LearningDeliveryObjects LearningDeliveryTab
        {
            get
            {
                return new LearningDeliveryObjects(GetWindow(MainWindowTitle));
            }
        }
        public static HomeObjects HomeWindow
        {
            get { return new HomeObjects(GetWindow(MainWindowTitle)); }
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
