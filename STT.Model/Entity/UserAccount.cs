using System;

namespace STT.Model.Entity
{
    public class UserAccount : EntityBase
    {
        public override Guid Key { get; set; }

        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsPowerUser { get; set; }

        public UserAccount()
        {
        }
        public UserAccount(string username)
        {
            Username = username;
        }
    }
}
