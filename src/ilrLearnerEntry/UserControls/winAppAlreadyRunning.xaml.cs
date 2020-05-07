using System.Windows;

namespace ilrLearnerEntry.UserControls
{
	/// <summary>
	/// Interaction logic for winAppAlreadyRunning.xaml
	/// </summary>
	public partial class winAppAlreadyRunning : Window
	{
		public winAppAlreadyRunning()
		{
			InitializeComponent();
			this.txtApplicationName.Text = App.ApplicationName;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
