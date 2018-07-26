using System.Windows.Input;
using O2.FileManager.Helpers.Commands;

namespace O2.FileManager.ViewModels
{
    public interface IObjectDisk
    {
        IAsyncCommand ClickCommand { get; }
        string Name { get; set; }
        string ShortName { get; set; }
    }
}