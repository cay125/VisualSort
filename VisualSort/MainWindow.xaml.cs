using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;

namespace VisualSort
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet dataSet;
        SortType sortType;
        public MainWindow()
        {
            InitializeComponent();
            
            sortType = new SortType();
            DataContext = sortType;
        }

        private void GenerateNums(object sender, RoutedEventArgs e)
        {
            dataSet = new DataSet(area, Settings.TotalNums, Settings.Gap);
            area.Children.Clear();
            dataSet.InitCanvas();
        }
        
        private void Run(object sender, RoutedEventArgs e)
        {
            Thread thread;
            Settings.TimeSpanMs = Convert.ToInt32(TimeInput.Text);
            switch (sortType.SortEnum)
            {
                case SortTypeEnum.SelectSort:
                    thread = new Thread(new ThreadStart(SelectSort));
                    thread.Start();
                    break;
                case SortTypeEnum.BubbleSort:
                    thread = new Thread(new ThreadStart(BubbleSort));
                    thread.Start();
                    break;
                case SortTypeEnum.MergeSort:
                    thread = new Thread(new ThreadStart(MergeSort));
                    thread.Start();
                    break;
                case SortTypeEnum.ShellSort:
                    thread = new Thread(new ThreadStart(ShellSort));
                    thread.Start();
                    break;
                case SortTypeEnum.QuickSort:
                    break;
                case SortTypeEnum.BucketSort:
                    break;
                case SortTypeEnum.InsertSort:
                    thread = new Thread(new ThreadStart(InsertSort));
                    thread.Start();
                    break;
                default:
                    break;
            }
        }
    }
}
