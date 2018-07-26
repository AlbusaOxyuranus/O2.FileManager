namespace O2.FileManager.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private DiskViewModel _leftDiskViewModel;
        private DiskViewModel _rigthDiskViewModel;

        public MainViewModel()
        {
            _leftDiskViewModel = new DiskViewModel();
            _rigthDiskViewModel=new DiskViewModel();
        }
        public DiskViewModel LeftDiskViewModel
        {
            get => _leftDiskViewModel;
            set
            {
                if (Equals(value, _leftDiskViewModel)) return;
                _leftDiskViewModel = value;
                OnPropertyChanged();
            }
        }

        public DiskViewModel RigthDiskViewModel
        {
            get => _rigthDiskViewModel;
            set
            {
                if (Equals(value, _rigthDiskViewModel)) return;
                _rigthDiskViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
