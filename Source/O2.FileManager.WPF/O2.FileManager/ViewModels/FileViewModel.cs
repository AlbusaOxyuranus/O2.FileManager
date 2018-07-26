using System;
using System.Threading.Tasks;
using O2.FileManager.Helpers.Commands;

namespace O2.FileManager.ViewModels
{
    public class FileViewModel:ViewModelBase,IObjectDisk
    {
        private string _name;
        private string _shortName;

        public IAsyncCommand ClickCommand { get; }

        public FileViewModel()
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
