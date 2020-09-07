using Test2WpfApplication.ViewModels;
using System.IO;
using System.Windows;
using Test2WpfApplication.Utilities;
using System;

namespace Test2WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        private ExpolorerViewModel _logic;
        public ExpolorerViewModel Logic
        {
            get
            {
                if (_logic == null)
                    _logic = new ExpolorerViewModel();

                return _logic;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = Logic;
        }

        #region Methods

        /// <summary>
        /// Perform delete action on the current selected treeviewitem
        /// </summary>
        private void DoDelete()
        {
            var dataItem = (DataItemViewModel)FolderView.SelectedItem;

            if (dataItem != null)
            {
                DataItemViewModel itemToDelete = null;
                foreach (DataItemViewModel drive in Logic.Drives)
                {
                    foreach (DataItemViewModel item in drive.Children)
                        if (item != null && item.FullPath == dataItem.FullPath)
                        {
                            if (Delete(dataItem.FullPath))
                                itemToDelete = item;
                            break;
                        }

                    if (itemToDelete != null)
                    {
                        drive.Children.Remove(itemToDelete);
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// Perform delete for the given item
        /// </summary>
        /// <param name="path">full item path</param>
        /// <returns></returns>
        private bool Delete(string path)
        {
            try
            {
                var doDelete = MessageBox.Show(string.Format("Really want to delete {0}", path), "DELETE", MessageBoxButton.YesNo);
                if (doDelete == MessageBoxResult.Yes)
                {
                    var attributes = File.GetAttributes(path) & FileAttributes.Directory;
                    if (attributes == FileAttributes.Directory)
                        Directory.Delete(path);
                    else
                        File.Delete(path);

                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager.HandleException(ex);
            }
           
            return false;
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoDelete();
        }

        #endregion Methods
    }
}
