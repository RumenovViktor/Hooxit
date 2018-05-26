using System.Threading.Tasks;

namespace Hooxit.Services.Candidates.Interfaces
{
    public interface IUserPersonalInfoHandler<T>
    {
        Task<T> Handle(T command);
    }
}
