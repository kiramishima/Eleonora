using System.Threading.Tasks;

namespace Eleonora.Core.Interfaces
{
    public interface ICommand
    {
        string Command(string command);
        Task Exec();
    }
}
