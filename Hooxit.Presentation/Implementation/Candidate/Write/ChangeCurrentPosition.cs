using Hooxit.Presentation.Interfaces;
using System;

namespace Hooxit.Presentation.Implemenation.Candidate.Write
{
    public class ChangeCurrentPosition : ICommand
    {
        public string Position { get; set; }

        public DateTime IssuedOn { get; set; }

        public string IssuedBy { get; set; }
    }
}
