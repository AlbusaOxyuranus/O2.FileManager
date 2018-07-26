using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using O2.FileManager.Helpers;
using O2.FileManager.Helpers.Commands;

namespace O2.FileManager.ViewModels
{
    public class DiskViewModel : ViewModelBase
    {
        private long _availableFreeSpace;
        private DriveType _driveType;
        private ObservableCollection<DiskViewModel> _items;
        private ObservableCollection<IObjectDisk> _itemsFiles;
        private string _name;
        private DiskViewModel _selectedItem;
        private long _totalFreeSpace;
        private long _totalSize;
        private string _volumeLabel;
        private IObjectDisk _selectedObjectDisk;
        public IAsyncCommand SelectCommand { get; }
        public IAsyncCommand SelectDiskCommand { get; }
        public DiskViewModel()
        {
            Items = new ObservableCollection<DiskViewModel>();
            ItemsFiles = new ObservableCollection<IObjectDisk>();
            SelectDiskCommand = AsyncCommand.Create(SelectDisk, CanSelectDisk);
            SelectCommand = AsyncCommand.Create(Select, CanSelect);
        }

        private bool CanSelectDisk()
        {
            return true;
        }

        private async Task SelectDisk()
        {
            await Task.Delay(1);
            OnLoadedFilesAndDirectories(_selectedItem.Name);
        }

        private bool CanSelect()
        {
            return true;
        }

       
        private async Task Select()
        {
            await Task.Delay(1);
            if (SelectedObjectDisk.Is<DirectoryViewModel>())
            {
                ///SelectedObjectDisk.As<DirectoryViewModel>().ParentDirectory = 
                OnLoadedFilesAndDirectories(SelectedObjectDisk.As<DirectoryViewModel>().Name);
            }
        }

        public IObjectDisk SelectedObjectDisk
        {
            get => _selectedObjectDisk;
            set
            {
                if (Equals(value, _selectedObjectDisk)) return;
                _selectedObjectDisk = value;
                OnPropertyChanged();
            }
        }

        public DiskViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<IObjectDisk> ItemsFiles
        {
            get => _itemsFiles;
            set
            {
                if (Equals(value, _itemsFiles)) return;
                _itemsFiles = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DiskViewModel> Items
        {
            get => _items;
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                OnPropertyChanged();
            }
        }

        public long TotalSize
        {
            get => _totalSize;
            set
            {
                if (value == _totalSize) return;
                _totalSize = value;
                OnPropertyChanged();
            }
        }

        public long TotalFreeSpace
        {
            get => _totalFreeSpace;
            set
            {
                if (value == _totalFreeSpace) return;
                _totalFreeSpace = value;
                OnPropertyChanged();
            }
        }

        public long AvailableFreeSpace
        {
            get => _availableFreeSpace;
            set
            {
                if (value == _availableFreeSpace) return;
                _availableFreeSpace = value;
                OnPropertyChanged();
            }
        }

        public DriveType DriveType
        {
            get => _driveType;
            set
            {
                if (value == _driveType) return;
                _driveType = value;
                OnPropertyChanged();
            }
        }

        public string VolumeLabel
        {
            get => _volumeLabel;
            set
            {
                if (value == _volumeLabel) return;
                _volumeLabel = value;
                OnPropertyChanged();
            }
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


        private void OnLoadedFilesAndDirectories(string targetDirectory)
        {
            ItemsFiles.Clear();
            var root = Path.GetPathRoot(targetDirectory);
            ItemsFiles.Add(new DirectoryViewModel(){Name = root , ShortName = "..."});
            // Process the list of files found in the directory.
            var fileEntries = Directory.GetFiles(targetDirectory);
            foreach (var fileName in fileEntries) ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            var subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (var subdirectory in subdirectoryEntries) ProcessDirectory(subdirectory);
        }

        private void ProcessDirectory(string subdirectory)
        {
            string lastFolderName = Path.GetFileName(subdirectory);
            ItemsFiles.Add(new DirectoryViewModel() { Name = subdirectory , ShortName = lastFolderName });
        }

        private void ProcessFile(string fileName)
        {
            ItemsFiles.Add(new FileViewModel {Name = fileName, ShortName = Path.GetFileName(fileName) });
            
        }

        public static ObservableCollection<DiskViewModel> GetDisks()
        {
            var diskViewModels = new ObservableCollection<DiskViewModel>();
            var allDrives = DriveInfo.GetDrives();

            foreach (var d in allDrives)
                diskViewModels.Add(new DiskViewModel
                {
                    VolumeLabel = d.VolumeLabel,
                    Name = d.Name,
                    DriveType = d.DriveType,
                    AvailableFreeSpace = d.AvailableFreeSpace,
                    TotalFreeSpace = d.TotalFreeSpace,
                    TotalSize = d.TotalSize
                });
            return diskViewModels;
        }
    }
}