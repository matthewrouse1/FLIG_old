namespace FLIGCommon.Interfaces
{
    public interface IUserDetails
    {
        string GetUsername();
        System.Security.SecureString GetPassword();
        string GetEmailAddress();
        string GetRepoPath();
        string GetRepoURL();

        void SetUsername(string Value);
        void SetPassword(string Value);
        void SetEmailAddress(string Value);
        void SetRepoPath(string Value);
        void SetRepoURL(string Value);

        bool IsUserSetup();
    }
}
