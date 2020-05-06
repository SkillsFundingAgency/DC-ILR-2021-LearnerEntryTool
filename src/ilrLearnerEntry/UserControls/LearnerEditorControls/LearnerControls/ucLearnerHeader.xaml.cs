using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ILR;
using ilrLearnerEntry.UserControls.Validations;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
	/// <summary>
	/// Interaction logic for ucLearnerHeader.xaml
	/// </summary>
	public partial class ucLearnerHeader : System.Windows.Controls.UserControl, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Private Variables
        private const String CLASSNAME = "Learner";
        private ILR.Schema XmlSchema = new ILR.Schema();
        #endregion

        #region Constructor
        public ucLearnerHeader()
		{
			InitializeComponent();
		}
        #endregion

        #region Public Properties
        public DataTable GenderList { set; get; }

        private Learner _learner;
        public Learner CurrentItem
		{
			get { return _learner; }
			set
			{
                _prevukprn = string.Empty;
                _uln = string.Empty;
                _pre_mergerUkprn = string.Empty;
                if (value != null)
				{
					_learner = value;
                    _prevukprn = value.PrevUKPRN.ToString();
                    _pre_mergerUkprn = value.PMUKPRN.ToString();
                    _uln = value.ULN.ToString();
                    this.DataContext = this;
					OnPropertyChanged("GenderList");
					OnPropertyChanged("DOB");
                    OnPropertyChanged("PrevUKPRN");
                    OnPropertyChanged("PMUKPRN");
                    OnPropertyChanged("ULN");
                    OnPropertyChanged("CurrentItem");
                }
				else
				{
					this.DataContext = null;
				}
			}

		}

		public DateTime? DOB
		{
			get { return _learner.DateOfBirth; }
			set
			{
				if (Convert.ToDateTime(_learner.DateOfBirth).Ticks != Convert.ToDateTime(value).Ticks)
				{
					_learner.DateOfBirth = value;
					OnPropertyChanged("CurrentItem");
				}
			}
		}

        private string _prevukprn = string.Empty;
        public string PrevUKPRN
		{
            get { return _prevukprn; }
            set
            {
                _prevukprn = value;
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.PrevUKPRN = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result)
                    { CurrentItem.PrevUKPRN = number; }
                }
            }
        }

        private string _pre_mergerUkprn = string.Empty;
        public string PMUKPRN
        {
            get { return _pre_mergerUkprn; }
            set
            {
                _pre_mergerUkprn = value;
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.PMUKPRN = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result)
                    { CurrentItem.PMUKPRN = number; }
                }
            }
        }

        private string _uln = string.Empty;
        public string ULN
        {
            get { return _uln; }
            set
            {
                _uln = value;
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.ULN = null;
                }
                else
                {
                    long number;
                    bool result = Int64.TryParse(System.Convert.ToString(value), out number);
                    if (result)
                    {
                        CurrentItem.ULN = number;
                    }
                    else
                        CurrentItem.ULN = null;
                }
            }
        }
		#endregion

		#region Public Methods
		#endregion

		#region PRIVATE Methods
		private void dtDOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				this.DOB = Convert.ToDateTime(e.AddedItems[0]);
			}
		}

        // If the text is not a valid date, show a message.
        private void dtDOB_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            DateTime newDate;

            if (!DateTime.TryParse(e.Text, out newDate))
            {
                string message = String.Format("The date, {0} is not a valid date, please enter a valid date.", e.Text);
                System.Windows.MessageBox.Show(message, "Date of birth", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// INotifyPropertyChanged requires a property called PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Fires the event for the property when it changes.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
#if DEBUG
			VerifyPropertyName(propertyName);
#endif
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

		}

		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				var msg = "Invalid property name: " + propertyName;

				if (this.ThrowOnInvalidPropertyName)
				{
					throw new Exception(msg);
				}
				else
				{
					Debug.Fail(msg);
				}
			}
		}

		protected bool ThrowOnInvalidPropertyName { get; set; }

        #endregion

        #region IDataErrorInfo Members
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
        public string this[string columnName]
        {
            get
            {
                string sReturn = null;
                switch (columnName)
                {
                   
                    case "ULN":
                        if (ULN == null || ULN.ToString().Length == 0)
                            return "ULN - required\r\n";
                        if (!string.IsNullOrEmpty(ULN))
                        {
                            sReturn += CheckPropertyLength(ULN, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt64ValidValue(ULN, columnName);
                        }

                        break;
                    case "PrevUKPRN":
                        if (!string.IsNullOrEmpty(PrevUKPRN))
                        {
                            sReturn += CheckPropertyLength(PrevUKPRN, CLASSNAME, columnName);
                            sReturn += NumericValidations.CheckInt64ValidValue(PrevUKPRN, columnName);
                        }
                        break;
                    default:
                        break;
                }
                return sReturn;
            }
        }

        public int GetItemSize(string ItemName)
        {
            return XmlSchema.GetMaxLength(ItemName);
        }
        public string CheckPropertyLength(object itemValue, string ClassName, string ItemName)
        {
            String ItemFullName = String.Format("{0}.{1}", ClassName, ItemName);
            int ItemSize = GetItemSize(ItemFullName);
            if (itemValue != null && itemValue.ToString().Length > ItemSize)
            {
                return String.Format("exceeds maximum length ({0} characters). Current length : {1}\r\n", ItemSize, itemValue.ToString().Length);
            }
            return null;
        }
        #endregion
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			OnPropertyChanged("CurrentItem");
			CurrentItem.RefreshData();
		}

	}
}
