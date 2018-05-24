using Hooxit.Presentation.Write;
using System.Threading.Tasks;

namespace Hooxit.Services.Contracts
{
    public interface ISkillsService
    {
        bool ChangeSkills(ChangeSkills skills);
    }
}
