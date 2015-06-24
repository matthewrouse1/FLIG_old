namespace FLIGCommon.Interfaces
{
    public interface ISettingsProvider
    {
        string Get(string SettingsKey);
        void Set(string SettingsKey, string SettingsValue);
    }
}
