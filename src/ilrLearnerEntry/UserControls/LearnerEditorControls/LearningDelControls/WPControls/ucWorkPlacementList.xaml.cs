﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ILR;


namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.WorkPlacementControls
{
	public partial class ucWorkPlacementList : BaseUserControl, INotifyPropertyChanged
	{

		private LearningDelivery _learnerDelivery;

		#region Constructor
		public ucWorkPlacementList()
		{
			InitializeComponent();
		}
		#endregion

		#region Public Properties
		public LearningDelivery CurrentItem
		{
			get { return _learnerDelivery; }
			set
			{
				if (value != null)
				{
					_learnerDelivery = value;
					this.DataContext = this;
					WorkPlacementItemsCV = CollectionViewSource.GetDefaultView(_learnerDelivery.LearningDeliveryWorkPlacementList as List<LearningDeliveryWorkPlacement>);
					OnPropertyChanged("CurrentItem");
					if (_learnerDelivery.LearningDeliveryWorkPlacementList.Count > 0)
					{
						(WorkPlacementItemsCV.CurrentItem as ILR.LearningDeliveryWorkPlacement).IsSelected = true;
					}

					WorkPlacementItemsCV.MoveCurrentToFirst();
					WorkPlacementItemsCV.Refresh();
					OnPropertyChanged("CurrentItem");
				}
				else
				{
					this.DataContext = null;
				}
				OnPropertyChanged("CurrentItem");
				OnPropertyChanged("WorkPlacementItemsCV");
				ShouldShowListView();
			}
		}
		public ICollectionView WorkPlacementItemsCV
		{
			get;
			private set;
		}
		#endregion

		#region PRIVATE Methods
		private void ShouldShowListView()
		{
			if ((_learnerDelivery == null) || (_learnerDelivery.LearningDeliveryWorkPlacementList.Count == 0))
			{
				WorkPlacementItemControl.Visibility = System.Windows.Visibility.Collapsed;
			}
			else if (_learnerDelivery.LearningDeliveryWorkPlacementList.Count == 1)
			{
				WorkPlacementItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				WorkPlacementItemControl.Visibility = System.Windows.Visibility.Visible;
			}
			lv.Visibility = System.Windows.Visibility.Visible;
		}

		private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				WorkPlacementItemControl.CurrentItem = e.AddedItems[0] as LearningDeliveryWorkPlacement;
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			LearningDeliveryWorkPlacement tmp = _learnerDelivery.CreateLearningDeliveryWorkPlacement();
			WorkPlacementItemsCV.MoveCurrentTo(tmp);
			WorkPlacementItemsCV.Refresh();
            _learnerDelivery.RefreshData();
            OnPropertyChanged("WorkPlacementItemsCV");
			ShouldShowListView();
		}
		private void Remove_Click(object sender, RoutedEventArgs e)
		{

			if (WorkPlacementItemsCV.CurrentItem != null)
			{
				LearningDeliveryWorkPlacement lr2Remove = WorkPlacementItemsCV.CurrentItem as LearningDeliveryWorkPlacement;
				if (lr2Remove != null)
				{
					_learnerDelivery.Delete(lr2Remove);
					if ((_learnerDelivery != null) && (_learnerDelivery.LearningDeliveryWorkPlacementList != null) && (_learnerDelivery.LearningDeliveryWorkPlacementList.Count>0))
					{

						if (!WorkPlacementItemsCV.MoveCurrentToPrevious())
						{
							WorkPlacementItemsCV.MoveCurrentToFirst();
						}
						if ((WorkPlacementItemsCV.CurrentItem != null) && (WorkPlacementItemsCV.CurrentItem != lr2Remove))
						{
							LearningDeliveryWorkPlacement lr = WorkPlacementItemsCV.CurrentItem as LearningDeliveryWorkPlacement;
							lr.IsSelected = true;
						}
						else
						{
							WorkPlacementItemsCV.MoveCurrentToNext();
							if (WorkPlacementItemsCV.CurrentItem != null)
							{
								LearningDeliveryWorkPlacement lr = WorkPlacementItemsCV.CurrentItem as LearningDeliveryWorkPlacement;
								lr.IsSelected = true;
							}
						}
					}
					WorkPlacementItemsCV.Refresh();
					OnPropertyChanged("WorkPlacementItemsCV");
					ShouldShowListView();
				}
			}
		}

		#endregion
	}
}
