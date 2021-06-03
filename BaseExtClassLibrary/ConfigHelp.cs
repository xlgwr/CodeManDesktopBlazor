using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
	public class ConfigHelp
	{


		public static string GetAppConfig(string key)
		{
			return ConfigurationManager.AppSettings[key].ToNotNullString();
		}
		public static void SaveAppConfig(string key, string value)
		{
			//Create the object
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			//make changes
			config.AppSettings.Settings[key].Value = value;

			//save to apply changes
			config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
