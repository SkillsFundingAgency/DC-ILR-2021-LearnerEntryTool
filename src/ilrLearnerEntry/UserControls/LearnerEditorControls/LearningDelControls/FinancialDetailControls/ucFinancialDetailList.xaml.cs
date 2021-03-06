﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.FinancialDetailControls
{
    /// <summary>
    /// Interaction logic for ucTrailblazerList.xaml
    /// </summary>
    public partial class ucFinancialDetailList : BaseUserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private ILR.LearningDelivery _learningDelivery;
        private List<ILR.ApprenticeshipFinancialRecord> _apprenticeshipFinancialRecordList = new List<ILR.ApprenticeshipFinancialRecord>(0);
        #endregion

        public ucFinancialDetailList()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #region Public Properties

        public LearningDelivery CurrentItem
        {
            get { return _learningDelivery; }
            set
            {
                _learningDelivery = value;
                if ((_learningDelivery == null) || (_learningDelivery.ApprenticeshipFinancialRecordList == null))
                {
                    _apprenticeshipFinancialRecordList = null;
                }
                else
                {
                    _apprenticeshipFinancialRecordList = GetTrailblazerList(_learningDelivery.ApprenticeshipFinancialRecordList);
                    ApprenticeshipFinancialItemsCV = CollectionViewSource.GetDefaultView(_apprenticeshipFinancialRecordList as List<ILR.ApprenticeshipFinancialRecord>);
                    ApprenticeshipFinancialItemsCV.MoveCurrentToFirst();

                    if (_apprenticeshipFinancialRecordList.Count > 0)
                    {
                        (ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord).IsSelected = true;
                    }
                    //ApprenticeshipFinancialItemsCV.Refresh();
                }
                OnPropertyChanged("CurrentItem");
                OnPropertyChanged("ApprenticeshipFinancialItemsCV");
                ShouldShowListView();
            }
        }
        public ICollectionView ApprenticeshipFinancialItemsCV
        {
            get;
            private set;
        }

        public DataTable FinTypeList { set; get; }
        public DataTable FinCodetList_TNP { set; get; }
        public DataTable FinCodetList_PMR { set; get; }
        #endregion

        #region PRIVATE Methods
        private List<ILR.ApprenticeshipFinancialRecord> GetTrailblazerList(List<ILR.ApprenticeshipFinancialRecord> inputlist)
        {
            return inputlist.ToList();
        }
        private void ShouldShowListView()
        {
            if ((_learningDelivery == null) || (_learningDelivery.ApprenticeshipFinancialRecordList.Count == 0))
            {
                LDFinancialDetailControl.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (_learningDelivery.ApprenticeshipFinancialRecordList.Count > 0)
            {
                LDFinancialDetailControl.Visibility = System.Windows.Visibility.Visible;

                // lv.SelectedItem = lv.Items.GetItemAt(rows.Count - 1);
                //lv.ScrollIntoView(lv.SelectedItem);
                // ListViewItem item = lv.ItemContainerGenerator.ContainerFromItem(lv.SelectedItem) as ListViewItem;
                //   if(item !=null)
                //   item.Focus();

                //if (lv.SelectedItem != null)
                //{
                //    int itemIndex = lv.SelectedIndex;
                //    VirtualizingStackPanel vsp =
                //    (VirtualizingStackPanel)typeof(ItemsControl).InvokeMember("_itemsHost",
                //    BindingFlags.Instance | BindingFlags.GetField | BindingFlags.NonPublic, null,
                //    lv, null);
                //    if (vsp != null)
                //    {
                //        double scrollHeight = vsp.ScrollOwner.ScrollableHeight;

                //        // itemIndex_ is index of the item which we want to show in the middle of the view
                //        double offset = scrollHeight * itemIndex / lv.Items.Count;

                //        vsp.SetVerticalOffset(offset);
                //    }
                //}
            }
            else
            {
                LDFinancialDetailControl.Visibility = System.Windows.Visibility.Visible;
            }
            lv.Visibility = System.Windows.Visibility.Visible;
        }

        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                LDFinancialDetailControl.CurrentItem = e.AddedItems[0] as ApprenticeshipFinancialRecord;

                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(delegate
                {
                    ListView view = sender as ListView;
                    view.ScrollIntoView(view.SelectedItem);
                }));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ApprenticeshipFinancialRecord tmp = _learningDelivery.CreateApprenticeshipFinancialRecord();
            _apprenticeshipFinancialRecordList = _learningDelivery.ApprenticeshipFinancialRecordList;
            ApprenticeshipFinancialItemsCV = CollectionViewSource.GetDefaultView(_learningDelivery.ApprenticeshipFinancialRecordList as List<ILR.ApprenticeshipFinancialRecord>);
            tmp.IsSelected = true;
            ApprenticeshipFinancialItemsCV.MoveCurrentTo(tmp);
            ApprenticeshipFinancialItemsCV.Refresh();
            OnPropertyChanged("ApprenticeshipFinancialItemsCV");
            ShouldShowListView();
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var removeButton = sender as Button;

            if (removeButton != null)
            {
                var parameter = removeButton.CommandParameter;

                if (parameter != null)
                {

                    ILR.ApprenticeshipFinancialRecord les2Remove = parameter as ILR.ApprenticeshipFinancialRecord;
                    les2Remove.IsSelected = true;

                    if (les2Remove != null)
                    {
                        ApprenticeshipFinancialItemsCV.MoveCurrentTo(les2Remove);
                        _learningDelivery.Delete(les2Remove);
                        _apprenticeshipFinancialRecordList.Remove(les2Remove);

                        if (!ApprenticeshipFinancialItemsCV.MoveCurrentToPrevious())
                        {
                            ApprenticeshipFinancialItemsCV.MoveCurrentToFirst();
                            ApprenticeshipFinancialItemsCV.Refresh();
                            OnPropertyChanged("ApprenticeshipFinancialItemsCV");
                        }

                        if (ApprenticeshipFinancialItemsCV.CurrentItem != null && ApprenticeshipFinancialItemsCV.CurrentItem != les2Remove)
                        {
                            ILR.ApprenticeshipFinancialRecord f = ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord;
                            f.IsSelected = true;

                        }
                        else
                        {
                            ApprenticeshipFinancialItemsCV.MoveCurrentToNext();
                            if (ApprenticeshipFinancialItemsCV.CurrentItem != null)
                            {
                                ILR.ApprenticeshipFinancialRecord f = ApprenticeshipFinancialItemsCV.CurrentItem as ILR.ApprenticeshipFinancialRecord;
                                f.IsSelected = true;
                            }
                        }
                    }
                    ApprenticeshipFinancialItemsCV.Refresh();
                
                OnPropertyChanged("ApprenticeshipFinancialItemsCV");
                ShouldShowListView();
                }
            }
        }

        #endregion
    }
}
