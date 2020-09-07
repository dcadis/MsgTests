using Test2WpfApplication.FileSystem;
using Test2WpfApplication.FileSystem.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace Test2WpfApplication.ViewModels
{
    /// <summary>
    /// Drives structure ViewModel
    /// </summary>
    public class ExpolorerViewModel : BaseViewModel
    {
        #region Members and Properties

        /// <summary>
        /// Drives on the local filesystem
        /// </summary>
        public ObservableCollection<DataItemViewModel> Drives { get; set; }

        #endregion Members and Properties

        /// <summary>
        /// Constructor
        /// </summary>
        public ExpolorerViewModel()
        { 
            var children = FileSystemHelper.GetLogicalDrives();

            Drives = new ObservableCollection<DataItemViewModel>(children.Select(drive => new DataItemViewModel(drive.FullPath, DataType.Drive)));
        }
    }
}
