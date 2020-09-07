using Test2WpfApplication.FileSystem;
using Test2WpfApplication.FileSystem.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Test2WpfApplication.ViewModels
{

    /// <summary>
    /// DataItem ViewModel
    /// </summary>
    public class DataItemViewModel : BaseViewModel
    {
        #region Members and Properties

        /// <summary>
        /// Type of element
        /// </summary>
		public DataType Type { get; set; }

        /// <summary>
        /// Full path of the element
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Name of the element
        /// </summary>
        public string Name { get { return Type == DataType.Drive ? FullPath : FileSystemHelper.GetFileOrFolderName(FullPath); } }

        /// <summary>
        /// List of children
        /// </summary>
        public ObservableCollection<DataItemViewModel> Children { get; set; }

        /// <summary>
        /// Expand command for drives/folders
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        /// <summary>
        /// delete command for folders/files
        /// </summary>
        public ICommand DeleteCommand { get; set; }


        /// <summary>
        /// Makes possible drives/folders expanding
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return Children != null ? (Children.Count(f => f != null) > 0) : false;
            }
            set
            {
                if (value == true)
                {
                    Expand();

                    if (Type == DataType.FolderClosed)
                    {
                        Type = DataType.FolderOpened;
                    }
                }
                else
                {
                    InitChildren();

                    if (Type != DataType.Drive)
                    {
                        Type = DataType.FolderClosed;
                    }
                }
            }
        }

        #endregion Members and Properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="type"></param>
        public DataItemViewModel(string fullPath, DataType type)
        {
            Type = type;
            FullPath = fullPath;
            ExpandCommand = new RelayCommand(Expand);
            DeleteCommand = new RelayCommand(Delete);

            InitChildren();
        }

        /// <summary>
        /// Initialize Children
        /// </summary>
        private void InitChildren()
        {
            if (Children == null)
            Children = new ObservableCollection<DataItemViewModel>();
            else
                Children.Clear();

            if (Type != DataType.File)
                Children.Add(null); // dummy node
        }

        /// <summary>
        /// Actions when a drive/folder is expanded
        /// </summary>
        private void Expand()
        {
            if (Type == DataType.File)
                return;

            Children.Clear();

            var children = FileSystemHelper.GetFolderContent(Type == DataType.Drive ? FullPath.Substring(FullPath.Count() - 4, 3) : FullPath);

            foreach (var child in children)
                Children.Add(new DataItemViewModel(child.FullPath, child.Type));
        }

        /// <summary>
        /// Actions when a drive/folder delete
        /// </summary>
        private void Delete()
        {
            //TreeItems = itemprovider.GetItems();

            //foreach (var item in TreeItems)
            //{
            //    if (item.IsSelected)
            //    {
            //        TreeItems.Remove(item);
            //    }
            //}
        }
    }
}
