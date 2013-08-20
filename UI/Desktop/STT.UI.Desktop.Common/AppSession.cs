using System;
using STT.Model.Entity;

namespace STT.UI.Desktop.Common
{
    public static class AppSession
    {
        public static event Action<UserAccount> LoggedInUserChanged;

        public static UserAccount LoggedInUser { get; private set; }

        public static void SetLoggedInUser(UserAccount userAccount)
        {
            LoggedInUser = userAccount;

            var handler = LoggedInUserChanged;
            if (handler != null)
                handler(userAccount);
        }
    }
}
