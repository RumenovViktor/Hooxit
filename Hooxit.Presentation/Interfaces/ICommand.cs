using System;

namespace Hooxit.Presentation.Interfaces
{
    public interface ICommand
    {
        DateTime IssuedOn { get; set; }
    }
}
