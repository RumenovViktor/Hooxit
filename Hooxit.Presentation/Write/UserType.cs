using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Write
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
