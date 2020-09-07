using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using Microsoft.Win32;

namespace Test3Windows.WindowsTheme
{
    /// <summary>
    /// DataItem ViewModel
    /// </summary>
    public class WindowsThemeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public ObservableCollection<string> Themes { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public WindowsThemeViewModel()
        {
            Themes = GetWindowsThemes();
        }

        /// <summary>
        /// Gets Themes
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GetWindowsThemes()
        {
            var themes = new ObservableCollection<string>();

            var attributes = File.GetAttributes(ThemeHelper.ThemesPath) & FileAttributes.Directory;
            if (attributes == FileAttributes.Directory)
            {
                foreach (var file in Directory.GetFiles(ThemeHelper.ThemesPath))
                    themes.Add(Path.GetFileName(file));
            }

            return themes;
        }
    }
}
