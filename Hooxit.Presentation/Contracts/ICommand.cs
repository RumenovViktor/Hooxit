using System;

namespace Hooxit.Presentation
{
    public interface ICommand
    {
        DateTime IssuedOn { get; set; }
    }
}
