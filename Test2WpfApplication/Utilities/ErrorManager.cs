using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test2WpfApplication.Utilities
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
            MessageBox.Show("Auch bei uns passiert es fehler: " + ex.Message, "Oups", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
