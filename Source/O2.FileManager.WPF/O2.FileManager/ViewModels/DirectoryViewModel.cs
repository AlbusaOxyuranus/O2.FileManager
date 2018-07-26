using System;
using System.Threading.Tasks;

using O2.FileManager.Helpers.Commands;

namespace O2.FileManager.ViewModels
{
    public class DirectoryViewModel : ViewModelBase, IObjectDisk
    {
        
        private DirectoryViewModel _parentDirectory;
        private string _name;
        private string _shortName;

        public DirectoryViewModel()
        {
            ClickCommand = AsyncCommand.Create(ClickMethod, canClickMethod);
        }

        private bool canClickMethod()
        {
            return true;
        }

        private Task ClickMethod()
        {
            throw new NotImplementedException();
        }

        public DirectoryViewModel ParentDirectory
        {
            get => _parentDirectory;
            set
            {
                if (Equals(value, _parentDirectory)) return;
                _parentDirectory = value;
                OnPropertyChanged();
            }
        }

        public IAsyncCommand ClickCommand { get; }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ShortName
        {
            get => _shortName;
            set
            {
                if (value == _shortName) return;
                _shortName = value;
                OnPropertyChanged();
            }
        }
    }
}