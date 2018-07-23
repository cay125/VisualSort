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
        Status status;
        Thread thread;
        public MainWindow()
        {
            InitializeComponent();
            
            sortType = new SortType();
            DataContext = sortType;
            status = new Status();
            Binding statusBind1 = new Binding
            {
                Source = status,
                Path = new PropertyPath("IsGenerateEn")
            };
            Binding statusBind2 = new Binding
            {
                Source = status,
                Path = new PropertyPath("IsDataValid")
            };
            Binding statusBind3 = new Binding
            {
                Source = status,
                Path = new PropertyPath("ToRunContent")
            };
            Generate.SetBinding(Button.IsEnabledProperty, statusBind1);
            ToRun.SetBinding(Button.IsEnabledProperty, statusBind2);
            ToRun.SetBinding(Button.ContentProperty, statusBind3);
        }

        private void GenerateNums(object sender, RoutedEventArgs e)
        {
            status.IsDataValid = true;
            Settings.TotalNums = Convert.ToInt32(Nums.Text);
            dataSet = new DataSet(area, Settings.TotalNums, Settings.Gap);
            area.Children.Clear();
            dataSet.InitCanvas();
        }
        
        private void Run(object sender, RoutedEventArgs e)
        {
            if (status.IsRunning == false)
            {
                Settings.TimeSpanMs = Convert.ToInt32(TimeInput.Text);
                status.IsRunning = true;
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
                        thread = new Thread(new ThreadStart(QuickSort));
                        thread.Start();
                        break;
                    case SortTypeEnum.BucketSort:
                        thread = new Thread(new ThreadStart(BucketSort));
                        thread.Start();
                        break;
                    case SortTypeEnum.InsertSort:
                        thread = new Thread(new ThreadStart(InsertSort));
                        thread.Start();
                        break;
                    case SortTypeEnum.HeapSort:
                        thread = new Thread(new ThreadStart(HeapSort));
                        thread.Start();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                thread.Abort();
                area.Children.Clear();
                if (sortType.SortEnum == SortTypeEnum.BucketSort)
                    dataSet.DeleteBucket();
                status.IsRunning = false;
                status.IsDataValid = false;
            }
        }

        private void WindowLoad(object sender, RoutedEventArgs e)
        {

        }
    }
}
