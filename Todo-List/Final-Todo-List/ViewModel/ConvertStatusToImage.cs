using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Final_Todo_List.ViewModel
{
    class ConvertStatusToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string item)
            {
                string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

                string imagePath = item switch
                {
                    "Pending" => $"pack://application:,,,/{assemblyName};component/Images/hourglass.png",
                    "Waiting" => $"pack://application:,,,/{assemblyName};component/Images/pause.png",
                    "Complete" => $"pack://application:,,,/{assemblyName};component/Images/done.png",
                    "Canceled" => $"pack://application:,,,/{assemblyName};component/Images/cancel.png"
                };

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }       
            

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
