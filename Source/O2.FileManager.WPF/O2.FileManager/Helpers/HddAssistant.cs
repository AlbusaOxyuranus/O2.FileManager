using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using O2.FileManager.ViewModels;

namespace O2.FileManager.Helpers
{
    public static class HddAssistant
    {
        public static IEnumerable<DiskViewModel> GetDisks()
        {
            IEnumerable<DiskViewModel> diskViewModels = new List<DiskViewModel>();
            var allDrives = DriveInfo.GetDrives();

            foreach (var d in allDrives)
            {
                //d.Name;
                //d.DriveType;
                //d.VolumeLabel;
                //d.DriveFormat;
                //d.AvailableFreeSpace;
                //d.TotalFreeSpace;
                //d.TotalSize
            }

            return diskViewModels;
        }
    }
}