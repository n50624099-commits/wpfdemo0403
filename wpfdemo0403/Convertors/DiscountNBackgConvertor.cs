using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace wpfdemo0403.Convertors
{
    public class DiscountNBackgConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out int discount))
            {
                if (discount > 15)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StockToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out int stock))
            {
                if (stock == 0)
                {
                    return new SolidColorBrush(Colors.LightGray);
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}