using System.Threading.Tasks;
using System.Windows.Input;

namespace Hooxit.Services.Contracts
{
    public interface IUserPersonalInfoHandler<T>
    {
        Task<T> Handle(T command);
    }
}
