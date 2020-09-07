using Test2WpfApplication.FileSystem.Data;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Test2WpfApplication
{
    /// <summary>
    /// Image converter, in oderd to show Icons for elements 
    /// </summary>
    [ValueConversion(typeof(DataItem), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = "Images/File.png";

            switch ((DataType)value)
            {
                case DataType.Drive:
                    image = "Images/Drive.png";
                    break;
                case DataType.FolderClosed:
                    image = "Images/FolderClosed.png";
                    break;
                case DataType.FolderOpened:
                    image = "Images/FolderOpened.png";
                    break;
                case DataType.Empty:
                    return null;
                    break;
            }

            return new BitmapImage(new Uri(string.Format("pack://application:,,,/{0}", image)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
