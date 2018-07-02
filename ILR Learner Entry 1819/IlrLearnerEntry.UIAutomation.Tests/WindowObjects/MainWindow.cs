using System;


using TestStack.White.UIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.Finders;

namespace IlrLearnerEntry.UIAutomation.Tests.WindowObjects
{
    public class MainWindow : WindowObject
    {
        private WindowObject Main
        {
            get{
                return this;
            }
        }

        private TabPage HomePage
        {
            get { return _window.Get<TabPage>(SearchCriteria.ByText("Home Page")); }
        }

        private TabPage Learners
        {
            get {
                return _window.Get<TabPage>(SearchCriteria.ByText("Learners"));
            }
        }

        private TabPage LearnerDestinations
        {
            get { 
                     return _window.Get<TabPage>(SearchCriteria.ByText("Learner Destination and Progression"));
            }
        }


        private Button ImportButton
        {
            get { return Button("Import Data"); }
        }

        private Button ExportButton
        {
            get { return Button("Export Data"); }
        }

        private ListView DataGrid
        {
            get { return ListView(); }
        }

        internal MainWindow(Window window) : base(window)
        {
        }
    
        public LearnerWindow SelectLearnerTab
        {
            get {
                Learners.Select();
                return Windows.LearnerWindow;
            }

        }

        public HomeWindow SelectHomeTab
        {
            get
            {
                HomePage.Select();
                return Windows.HomeWindow;
            }

        }


        //public void AddDepartment()
        //{
        //    DepartmentsTab.Select();
        //    AddButton.Click();
        //}

        //public void AddProject()
        //{
        //    ProjectsTab.Select();
        //    AddButton.Click();
        //}

        //public bool IsDepartmentInList(string departmentName)
        //{
        //    DepartmentsTab.Select();
        //    return DataGrid.Contains(departmentName);
        //}

        //public bool IsProjectInList(string projectName, int price)
        //{
        //    ProjectsTab.Select();
        //    return DataGrid.Contains(new ProjectInList(projectName, price));
        //}

        //public void AddEmployee()
        //{
        //    EmployeesTab.Select();
        //    AddButton.Click();
        //}

        //public bool IsEmployeeInList(string firstName, string lastName, string department)
        //{
        //    EmployeesTab.Select();
        //    return DataGrid.Contains(new EmployeeInList(firstName, lastName, department));
        //}

        //public bool IsCreatedDepartmentInList()
        //{
        //    return IsDepartmentInList(DepartmentCreator.LastCreated.Name);
        //}

        //public void OpenProject(string projectName)
        //{
        //    ProjectsTab.Select();
        //    DataGrid.SelectRow(projectName);
        //    EditButton.Click();
        //}

        //public void OpenCreatedProject()
        //{
        //    OpenProject(ProjectCreator.LastCreated.Name);
        //}

        //public void OpenCreatedEmployee()
        //{
        //    OpenEmployee(EmployeeCreator.LastCreated.FirstName, EmployeeCreator.LastCreated.LastName);
        //}

        //private void OpenEmployee(string firstName, string lastName)
        //{
        //    EmployeesTab.Select();
        //    DataGrid.SelectRow(new EmployeeInList(firstName, lastName, null));
        //    EditButton.Click();
        //}

        //public void OpenDashboard()
        //{
        //    DashboardTab.Select();
        //}

        //public void OpenDepartment(string departmentName)
        //{
        //    DepartmentsTab.Select();
        //    DataGrid.SelectRow(departmentName);
        //    EditButton.Click();
        //}

        //public void OpenCreatedDepartment()
        //{
        //    OpenDepartment(DepartmentCreator.LastCreated.Name);
        //}
    }
}
