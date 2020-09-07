using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test3Windows.WindowsTheme
{
    /// <summary>
    /// Manager for exceptions in the application
    /// </summary>
    public class ErrorManager
    {
        /// <summary>
        /// Handle exceptions
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="silent"></param>
        public static void HandleException(Exception ex, bool silent = false)
        {
            var message = "Auch bei uns passiert es fehler: " + Environment.NewLine + ex.Message;
            Console.WriteLine(message);
            MessageBox.Show(message, "Oups", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
