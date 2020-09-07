using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;

namespace Test3Windows.WindowsTheme
{
    /// <summary>
    /// Interaction logic for WindowsThemeWindow.xaml
    /// </summary>
    public partial class WindowsThemeWindow : Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WindowsThemeWindow()
        {
            InitializeComponent();
            DataContext = new WindowsThemeViewModel();
        }

        /// <summary>
        /// Windows Theme selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTheme.SelectedValue != null)
            {
                var strTheme = (string)cbTheme.SelectedValue;
                ThemeHelper.SetTheme(strTheme);
            }
        }

        /// <summary>
        /// After loading Themes, select the current one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTheme_Loaded(object sender, RoutedEventArgs e)
        {
            cbTheme.SelectedValue = ThemeHelper.GetCurrentTheme();
        }
    }
}
