namespace Hooxit.Services
{
    public sealed class UserInfo
    {
        private static string userName;

        public static string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
    }
}