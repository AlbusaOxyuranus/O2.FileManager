using System.Threading.Tasks;
using System.Windows.Input;

namespace O2.FileManager.Helpers.Commands
{
    public interface IAsyncCommand : ICommand

    {
        Task ExecuteAsync(object parameter);
    }
}