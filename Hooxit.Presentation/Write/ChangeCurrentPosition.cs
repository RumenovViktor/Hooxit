using System;

namespace Hooxit.Presentation.Write
{
    public class ChangeCurrentPosition : ICommand
    {
        public string Position { get; set; }

        public DateTime IssuedOn { get; set; }

        public string IssuedBy { get; set; }
    }
}
