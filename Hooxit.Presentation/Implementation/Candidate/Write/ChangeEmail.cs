using Hooxit.Presentation.Interfaces;
using System;

namespace Hooxit.Presentation.Implemenation.Candidate.Write
{
    public class ChangeEmail : ICommand
    {
        public ChangeEmail() { }

        public ChangeEmail(string email, DateTime issuedOn, string issuedBy)
        {
            this.Email = email;
            this.IssuedOn = issuedOn;
            this.IssuedBy = issuedBy;
        }

        public string Email { get; set; }

        public DateTime IssuedOn { get; set; }

        public string IssuedBy { get; set; }
    }
}
