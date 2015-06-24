﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace GitInterface
{
    public class Git
    {
        public PullOptions pullOptions { get; private set; }
        public PushOptions pushOptions { get; private set; }
        public FetchOptions fetchOptions { get; private set; }
        public CloneOptions cloneOptions { get; private set; }
        public StatusOptions statusOptions { get; private set; }

        public SecureUsernamePasswordCredentials credentials { get; private set; }
        public LibGit2Sharp.Repository repository { get; private set; }
        public Signature signature { get; private set; }

        private void setupOptions(string Username, SecureString SecurePassword, string RepoPath, string RepoUrl)
        {
            credentials = new SecureUsernamePasswordCredentials() { Username = Username, Password = SecurePassword };
            var credentialsProvider = new CredentialsHandler((_url, _user, _cred) => new SecureUsernamePasswordCredentials
            {
                Username = Username,
                Password = SecurePassword
            });
            fetchOptions = new FetchOptions() { CredentialsProvider = credentialsProvider };
            pullOptions = new PullOptions() { FetchOptions = fetchOptions };
            pushOptions = new PushOptions() { CredentialsProvider = credentialsProvider };
            cloneOptions = new CloneOptions() { CredentialsProvider = credentialsProvider };
            statusOptions = new StatusOptions() { ExcludeSubmodules = true, IncludeUnaltered = false };
        }

        private void setSignature(string Username, string EmailAddress)
        {
            this.signature = new Signature(Username, EmailAddress, DateTimeOffset.Now);
        }

        public string CloneRepo(string Url, string CloneToDirectory)
        {
            return LibGit2Sharp.Repository.Clone(Url, CloneToDirectory, this.cloneOptions);
        }

        public Git(string Username, string EmailAddress, SecureString Password, string RepoPath = @"C:\repos\formsLibrary", string RepoUrl = "http://github.com/AdvancedLegal/FormsLibrary")
        {
            // Password should be in the form of:
            // var password = new System.Security.SecureString();
            // "Welcome4".ToCharArray().ToList().ForEach(c => password.AppendChar(c));

            this.setupOptions(Username, Password, RepoPath, RepoUrl);

            this.setSignature(Username, EmailAddress);

            if (!Directory.Exists(RepoPath))
            {
                this.CloneRepo(RepoUrl, RepoPath);
            }

            if (!RepoPath.EndsWith("\\.git\\"))
                RepoPath += "\\.git\\";

            this.repository = new Repository(RepoPath);

        }

        public MergeResult Pull()
        {
            return this.repository.Network.Pull(this.signature, this.pullOptions);
        }

        public void Push(Branch Branch = null)
        {
            Branch = Branch == null ? this.repository.Branches["master"] : Branch;
            this.repository.Network.Push(Branch, this.pushOptions);
        }

        public RepositoryStatus Status()
        {
            return this.repository.RetrieveStatus(statusOptions);
        }
    }
}
