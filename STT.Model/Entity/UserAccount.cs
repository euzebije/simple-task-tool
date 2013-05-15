using System;

namespace STT.Model.Entity
{
    public class UserAccount : EntityBase
    {
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsPowerUser { get; set; }

        public UserAccount(){}
        public UserAccount(string username, bool isActive, bool isPowerUser)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");

            Username = username;
            
            var now = DateTime.Now;
            CreatedOn = now;
            LastLogin = now;

            IsActive = isActive;
            IsPowerUser = isPowerUser;
        }
    }
}
