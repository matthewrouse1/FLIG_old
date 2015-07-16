using FLIGCommon.Interfaces;
using Autofac;
using System.Security;
using System.Security.Cryptography;
using System;

namespace FLIGCommon.Implementations
{
    public class UserDetails : IUserDetails
    {
        private ISettingsProvider settings;
        private readonly IContainer Container;

        public UserDetails(IContainer container)
        {
            this.Container = container;
            this.settings = Container.Resolve<ISettingsProvider>();
        }

        public bool IsUserSetup()
        {
            if (string.IsNullOrEmpty(this.settings.Get("Username")) ||
                string.IsNullOrEmpty(this.settings.Get("Password")) ||
                string.IsNullOrEmpty(this.settings.Get("EmailAddress")) ||
                string.IsNullOrEmpty(this.settings.Get("RepoPath")) ||
                string.IsNullOrEmpty(this.settings.Get("RepoURL")))
                return false;
            return true;
        }

        #region Username
        public string GetUsername()
        {
            return settings.Get("Username");
        }

        public void SetUsername(string Value)
        {
            settings.Set("Username", Value);
        }
        #endregion

        #region Password

        // How the storage works.
        // We need to store the encrypted key and the entropy used to encrypt it.
        //var data = "Example";
        //byte[] entropy = new byte[16];
        //var procData = ProtectedData.Protect(GetBytes(data), entropy, DataProtectionScope.CurrentUser);
        //var procString = GetString(procData);
        //var unprocData = ProtectedData.Unprotect(GetBytes(procString), entropy, DataProtectionScope.CurrentUser);
        //var unprocString = GetString(unprocData);

        // Get String back from SecureString
        //IntPtr unmanagedString = IntPtr.Zero;
        //try
        //{
        //    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
        //    Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
        //}
        //finally
        //{
        //    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        //}

        private byte[] NewEntropy()
        {
            var Entropy = new byte[16];
            settings.Set("Entropy", Convert.ToBase64String(Entropy));
            return Entropy;
        }

        private byte[] CheckEntropy()
        {
            var entropy = new byte[16];
            if (string.IsNullOrEmpty(settings.Get("Entropy")))
            {
                entropy = this.NewEntropy();
            }
            else
            {
                entropy = Convert.FromBase64String(settings.Get("Entropy"));
            }

            return entropy;
        }

        public SecureString GetPassword()
        {
            var entropy = this.CheckEntropy();
            var secure = new SecureString();
            foreach (var c in Convert.ToBase64String(ProtectedData.Unprotect(Convert.FromBase64String(settings.Get("Password")), entropy, DataProtectionScope.CurrentUser)))
            {
                secure.AppendChar(c);
            }
            return secure;
        }

        public void SetPassword(string Value)
        {
            var entropy = this.CheckEntropy();
            settings.Set("Password", Convert.ToBase64String(ProtectedData.Protect(Convert.FromBase64String(Value), entropy, DataProtectionScope.CurrentUser)));
        }
        #endregion

        #region EmailAddress
        public string GetEmailAddress()
        {
            return settings.Get("EmailAddress");
        }

        public void SetEmailAddress(string Value)
        {
            settings.Set("EmailAddress", Value);
        }
        #endregion

        #region RepoPath
        public string GetRepoPath()
        {
            return settings.Get("RepoPath");
        }

        public void SetRepoPath(string Value)
        {
            settings.Set("RepoPath", Value);
        }
        #endregion

        #region RepoURL
        public string GetRepoURL()
        {
            return settings.Get("RepoURL");
        }

        public void SetRepoURL(string Value)
        {
            settings.Set("RepoURL", Value);
        }
        #endregion
    }
}
