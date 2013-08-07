using System;
using System.Security;
using STT.Model.Helper;

namespace STT.Model.Entity
{
    public class UserAccount : EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsPowerUser { get; set; }

        public UserAccount()
        {
            var now = DateTime.Now;
            CreatedOn = now;
            LastLogin = now;
        }
        public UserAccount(string username, string password, bool isActive, bool isPowerUser)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            Username = username;

            // Password encoding
            PasswordSalt = PasswordHelper.GenerateSalt();
            Password = PasswordHelper.EncodePassword(password, PasswordSalt);
            
            var now = DateTime.Now;
            CreatedOn = now;
            LastLogin = now;

            IsActive = isActive;
            IsPowerUser = isPowerUser;
        }

        public bool IsPasswordValid(string password)
        {
            var encodedPassword = PasswordHelper.EncodePassword(password, PasswordSalt);
            return Password.Equals(encodedPassword);
        }
        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (!IsPasswordValid(oldPassword))
                throw new SecurityException("Old password is invalid.");

            // Encode the new password
            PasswordSalt = PasswordHelper.GenerateSalt();
            Password = PasswordHelper.EncodePassword(newPassword, PasswordSalt);
        }
    }
}
