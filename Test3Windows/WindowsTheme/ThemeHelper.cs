using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace Test3Windows.WindowsTheme
{
    /// <summary>
    /// Theme ThemeHelper 
    /// </summary>
    public class ThemeHelper
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);

        /// <summary>
        /// Path location for default Windows Themes
        /// </summary>
        public static readonly string ThemesPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Resources\Themes\";

        /// <summary>
        /// 
        /// </summary>
        private const string CurrentThemeRegistryKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes";

        /// <summary>
        /// Gets Current Windows Theme
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTheme()
        {
            string theme = null;
            try
            {
                var themePath = (string)Registry.GetValue(CurrentThemeRegistryKey, "CurrentTheme", string.Empty);
                theme = themePath.Split(Path.DirectorySeparatorChar).Last();
            }
            catch (Exception ex)
            {
                ErrorManager.HandleException(ex);
            }

            return theme;
        }

        /// <summary>
        /// Changes Windows Theme
        /// </summary>
        /// <param name="themeName"></param>
        public static void SetTheme(string themeName)
        {
            string themePath = ThemesPath + @"\" + themeName;

            try
            {
                Process.Start(themePath);
            }
            catch (Exception ex)
            {
                ErrorManager.HandleException(ex);
            }
        }
    }
}
