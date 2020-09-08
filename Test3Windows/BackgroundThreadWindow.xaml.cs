using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test3Windows
{
    /// <summary>
    /// Interaction logic for BackgroundThreadWindow.xaml
    /// </summary>
    public partial class BackgroundThreadWindow : Window
    {
        public BackgroundThreadWindow()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private async void Init()
        {
            txtText.Text = "started";

            string text = await GetTextAsync();
            
            txtText.Dispatcher.Invoke(() =>
            {
                txtText.Text = text;
            });
        }

        /// <summary>
        /// Asynchronous Method to retrieve a text run on a thread
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetTextAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "threaded text";
            });
        }
    }
}
