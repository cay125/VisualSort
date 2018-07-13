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

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            dataSet = new DataSet(area, Settings.TotalNums, Settings.Gap);
            area.Children.Clear();
            dataSet.InitCanvas();
        }
        private void SwapAnimation(int i,int j)
        {
            this.Dispatcher.Invoke(new Action(() => { Canvas.SetZIndex(dataSet.DataValue[i], 100); Canvas.SetZIndex(dataSet.DataValue[j], 100); dataSet.DataValue[i].HightLight(0); dataSet.DataValue[j].HightLight(1); }));
            Thread.Sleep(Settings.TimeSpanMs);
            this.Dispatcher.Invoke(new Action(() => { dataSet.SwapBar(i, j); }));
            Thread.Sleep(Settings.TimeSpanMs * 2);
            this.Dispatcher.Invoke(new Action(() => { Canvas.SetZIndex(dataSet.DataValue[i], 0); Canvas.SetZIndex(dataSet.DataValue[j], 0); dataSet.DataValue[i].UnHightLight(); dataSet.DataValue[j].UnHightLight(); }));
        }
        void SelectSort()
        {
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Visible; dataSet.IndicateB.Visibility = Visibility.Visible; }));

            for (int i = 0; i < Settings.TotalNums - 1; i++) 
            {
                this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(i); }));
                Thread.Sleep(Settings.TimeSpanMs);
                for (int j = i + 1; j < Settings.TotalNums; j++)
                {
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateB.MoveBar(j); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    if (dataSet.DataValue[i] > dataSet.DataValue[j])
                        SwapAnimation(i, j);
                }
            }
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Hidden; dataSet.IndicateB.Visibility = Visibility.Hidden; }));
        }
        void BubbleSort()
        {
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Visible; dataSet.IndicateB.Visibility = Visibility.Visible; }));

            for (int i = 0; i < Settings.TotalNums - 1; i++) 
            {
                for (int j = 0; j < Settings.TotalNums - 1 - i; j++)  
                {
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(j); }));
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateB.MoveBar(j + 1); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    if (dataSet.DataValue[j] > dataSet.DataValue[j+1])
                        SwapAnimation(j, j + 1);
                }
            }
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Hidden; dataSet.IndicateB.Visibility = Visibility.Hidden; }));
        }
        void InsertSort()
        {
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Visible; dataSet.IndicateB.Visibility = Visibility.Visible; }));
            for (int i = 0; i < Settings.TotalNums - 1; i++)  
            {
                this.Dispatcher.Invoke(new Action(() => { dataSet.MarkBars(i, i); }));
                Thread.Sleep(Settings.TimeSpanMs);
                for (int j = i + 1; j > 0 ; j--) 
                {
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(j - 1); }));
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateB.MoveBar(j); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    if (dataSet.DataValue[j-1] <= dataSet.DataValue[j])
                        break;
                    SwapAnimation(j - 1, j);
                    this.Dispatcher.Invoke(new Action(() => { dataSet.MarkBars(j - 1, j); }));
                }
            }
            this.Dispatcher.Invoke(new Action(() => { dataSet.UnMark(0, Settings.TotalNums - 1); dataSet.IndicateA.Visibility = Visibility.Hidden; dataSet.IndicateB.Visibility = Visibility.Hidden; }));
        }
        void MergeArray(int first, int mid, int last, SortBar[] temp)
        {
            int i = first, j = mid + 1;
            int k = 0;
            this.Dispatcher.Invoke(new Action(() => { dataSet.MarkBars(first, last); }));
            Thread.Sleep(Settings.TimeSpanMs);
            while (i <= mid && j <= last)
            {
                if (dataSet.DataValue[i] <= dataSet.DataValue[j])
                {
                    this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[i].MoveBarToTemp(first + k); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    temp[k++] = dataSet.DataValue[i++];                   
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[j].MoveBarToTemp(first + k); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    temp[k++] = dataSet.DataValue[j++];
                }
            }

            while (i <= mid)
            {
                this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[i].MoveBarToTemp(first + k); }));
                Thread.Sleep(Settings.TimeSpanMs);
                temp[k++] = dataSet.DataValue[i++];
            }

            while (j <= last)
            {
                this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[j].MoveBarToTemp(first + k); }));
                Thread.Sleep(Settings.TimeSpanMs);
                temp[k++] = dataSet.DataValue[j++];
            }
            for (i = 0; i < k; i++)
            {
                dataSet.DataValue[first + i] = temp[i];
                this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[first + i].MoveBarDown(); }));
            }
            Thread.Sleep(Settings.TimeSpanMs);
            this.Dispatcher.Invoke(new Action(() => { dataSet.UnMark(first, last); }));
            Thread.Sleep(Settings.TimeSpanMs);
        }
        void MergeRecursion(int first, int last, SortBar[] temp)
        {
            if(first<last)
            {
                int mid = (first + last) / 2;
                MergeRecursion(first, mid, temp);
                MergeRecursion(mid + 1, last, temp);
                MergeArray(first, mid, last, temp);
            }

        }
        void MergeSort()
        {
            MergeRecursion(0, Settings.TotalNums - 1, new SortBar[Settings.TotalNums]);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread;
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
