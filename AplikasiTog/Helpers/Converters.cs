using Syncfusion.UI.Xaml.Grid.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Apel
{
    public class EccelOptionsConverter : IMultiValueConverter
    {
        private static bool _isCustomize=false;
        public bool isCustomized
        {
            get
            {
                return _isCustomize;
            }
            set
            {
                _isCustomize = value;

            }
        }

        private static bool _isCustomizeRow=false;
        public bool IsCustomizeRow
        {
            get
            {
                return _isCustomizeRow;
            }
            set
            {
                _isCustomizeRow = value;

            }
        }
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var options = new ExcelExportingOptions();
            if (!(bool)values[0])
                options.AllowOutlining = false;
            if ((bool)values[1])
                isCustomized = true;
            else
                isCustomized = false;
            
            if ((bool)values[2])
                IsCustomizeRow = true;
            else
                IsCustomizeRow = false;
            
            return options;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
