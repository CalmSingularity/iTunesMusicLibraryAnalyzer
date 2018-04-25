using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace iTunesLibraryAnalyzer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			tbLibraryFilePathName.Text = @"C:\Users\" + Environment.UserName + @"\Music\iTunes\iTunes Music Library.xml";
		}

		private void btnAnalyze_Click(object sender, RoutedEventArgs e)
		{
			Analyzer ana = new Analyzer(tbLibraryFilePathName.Text, cbKeyToAnalyze.Text);
			ana.Analyze();

			tbResults.Text = "Count\tValue\n";
			foreach (KeyValuePair<string, int> item in ana.Results.OrderByDescending(key => key.Value))
			{
				tbResults.AppendText(item.Value.ToString() + "\t" + item.Key + "\n");
			}
		}

		private void btnBrowse_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
			dialog.InitialDirectory = tbLibraryFilePathName.Text.Substring(0, tbLibraryFilePathName.Text.LastIndexOf('\\'));
			if (dialog.ShowDialog() == true)
				tbLibraryFilePathName.Text = dialog.FileName;
		}

		private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
		}
	}
}
