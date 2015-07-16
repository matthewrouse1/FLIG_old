using FLIGCommon.Interfaces;
using System.Reflection;
using System;

namespace FLIGCommon.Implementations
{
    public class SettingsProvider : ISettingsProvider
    {
        public string Get(string SettingsKey)
        {
            var prop = Properties.Settings.Default.GetType().GetProperty(SettingsKey, BindingFlags.Instance | BindingFlags.Public);
            if (prop != null && prop.CanRead)
                return prop.GetValue(Properties.Settings.Default).ToString();

            return null;
        }

        public void Set(string SettingsKey, string SettingsValue)
        {
            var prop = Properties.Settings.Default.GetType().GetProperty(SettingsKey, BindingFlags.Instance | BindingFlags.Public);
            if (prop != null && prop.CanWrite)
                prop.SetValue(Properties.Settings.Default, SettingsValue);
            Properties.Settings.Default.Save();
        }
    }
}
