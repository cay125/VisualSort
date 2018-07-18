using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;

namespace VisualSort
{
    public enum SortTypeEnum
    {
        SelectSort = 0,
        BubbleSort = 1,
        MergeSort  = 2,
        ShellSort  = 3,
        QuickSort  = 4,
        BucketSort = 5,
        InsertSort = 6,
        HeapSort   = 7
    }
    public class EnumToBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? false : value.Equals(parameter);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
    public class SortType : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
            }
        }

        private SortTypeEnum _sortEnum = SortTypeEnum.SelectSort;

        public SortTypeEnum SortEnum
        {
            get
            {
                return _sortEnum;
            }

            set
            {
                _sortEnum = value;
                OnPropertyChanged("SortEnum");
            }
        }
    }
}
