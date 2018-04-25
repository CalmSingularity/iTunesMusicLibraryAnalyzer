using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows;

namespace iTunesLibraryAnalyzer
{
	class Analyzer
	{
		private string xmlLibraryFilePathName, key;
		public Dictionary<string, int> Results { get; private set; }

		public Analyzer(string xmlLibraryFilePathName, string key)
		{
			this.xmlLibraryFilePathName = xmlLibraryFilePathName;
			this.key = key;
			Results = new Dictionary<string, int>();
		}

		public void Analyze()
		{
			try
			{
				XmlTextReader reader = new XmlTextReader(xmlLibraryFilePathName);
				reader.WhitespaceHandling = WhitespaceHandling.None;
				while (reader.Read())
				{
					int count;
					if (reader.NodeType == XmlNodeType.Text && reader.Value == key)
					{
						reader.Read();
						while (reader.NodeType != XmlNodeType.Text)
						{
							reader.Read();
						}
						if (Results.TryGetValue(reader.Value, out count))
						{
							Results[reader.Value] = count + 1;
						}
						else
						{
							Results[reader.Value] = 1;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

	}
}
