using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CodingDojo2.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        //Class that converts a given string into a color brush for a better user experience in the GUI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temp = (string) value;
            if (temp.Equals("Enabled"))
            {
                return new SolidColorBrush(Colors.LightGreen);
            }
           else
            {
                return new SolidColorBrush(Colors.Red);

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
