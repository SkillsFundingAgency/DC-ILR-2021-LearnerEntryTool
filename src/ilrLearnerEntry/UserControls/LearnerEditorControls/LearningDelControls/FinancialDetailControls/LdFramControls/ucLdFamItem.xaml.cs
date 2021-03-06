﻿
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.LdFramControls
{
	/// <summary>
	/// Interaction logic for LearnerSupportFundingItem.xaml
	/// </summary>
	public partial class ucLdFamItem : BaseUserControl, INotifyPropertyChanged
	{
		private LearningDeliveryFAM _learningDeliverFAM;
        
        #region Constructor
		public ucLdFamItem()
		{
			InitializeComponent();
			this.DataContext = this;
		}
		#endregion

		#region Public Properties
		public LearningDeliveryFAM CurrentItem
		{
			get { return _learningDeliverFAM; }
			set
			{
				if (value != null)
				{
					_learningDeliverFAM = value;
					this.DataContext = this;
					OnPropertyChanged("CurrentItem");
					OnPropertyChanged("IsTypeVisible");
					OnPropertyChanged("IsCodeReadOnly");
					OnPropertyChanged("IsCodeVisible");
					OnPropertyChanged("IsFromVisible");
					OnPropertyChanged("IsToVisible");
				}
				else
				{
					this.DataContext = null;
				}
			}
		}

		public Boolean IsTypeEnables
		{
			get
			{
				bool bRet = true;
				return bRet;
			}
		}
		public Visibility IsTypeVisible
		{
			get
			{
				Visibility v = Visibility.Visible;
				if ((_learningDeliverFAM == null) || (_learningDeliverFAM.LearnDelFAMType == null))
				{

				}
				else
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
						case "LSF":
							v = Visibility.Visible;
							break;
						case "HEM":
						case "LDM":
							v = Visibility.Collapsed;
							break;
						default:
							v = Visibility.Visible;
							break;
					}
				}
				return v;
			}
		}
		public Visibility IsCodeVisible
		{
			get
			{
				Visibility v = Visibility.Visible;
				if ((_learningDeliverFAM == null) || (_learningDeliverFAM.LearnDelFAMType == null))
				{

				}
				else
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
							v = Visibility.Visible;
							break;
						case "HEM":
						case "LDM":
						case "LSF":
							v = Visibility.Collapsed;
							break;
						default:
							v = Visibility.Visible;
							break;
					}
				}
				return v;
			}
		}
		
        public bool IsCodeReadOnly
        {
            get
            {
                bool bRet = false;
                if ((_learningDeliverFAM == null) || (_learningDeliverFAM.LearnDelFAMType == null))
                {

                }
                else
                {
                    switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
                    {
                        case "ALB":
                        case "ACT":
                            bRet = false;
                            break;
                        case "LSF":
                            bRet = true;
                            break;
                        default:
                            bRet = true;
                            break;
                    }
                }
                return bRet;
            }
        }

        public Visibility IsFromVisible
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				if (_learningDeliverFAM != null)
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
						case "HEM":
						case "LDM":
                        case "ACT":
                        case "LSF":
							v = Visibility.Visible;
							break;
						default:
							v = Visibility.Collapsed;
							break;
					}
				}
				return v;
			}
		}
		public Visibility IsToVisible
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				if (_learningDeliverFAM != null)
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
						case "HEM":
						case "LDM":
                        case "ACT":
                        case "LSF":
							v = Visibility.Visible;
							break;
						default:
							v = Visibility.Collapsed;
							break;
					}
				}
				return v;
			}
		}
		#endregion

		#region Public Methods
		#endregion

		#region PRIVATE Methods
		#endregion
	}
}
