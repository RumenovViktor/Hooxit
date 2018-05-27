namespace Hooxit.Presentation.UserManagement
{
    public class UserType
    {
        public UserTypes ChosenUserType { get; set; }
    }

    public enum UserTypes
    {
        Company = 1,
        Candidate = 2
    }
}
