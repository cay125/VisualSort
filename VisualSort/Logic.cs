using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows;

namespace VisualSort
{
    public partial class MainWindow
    {
        private void SwapAnimation(int i, int j)
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
                    if (dataSet.DataValue[j] > dataSet.DataValue[j + 1])
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
                this.Dispatcher.Invoke(new Action(() => { dataSet.MarkBars(i, i, 2); }));
                Thread.Sleep(Settings.TimeSpanMs);
                for (int j = i + 1; j > 0; j--)
                {
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(j - 1); }));
                    this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateB.MoveBar(j); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    if (dataSet.DataValue[j - 1] <= dataSet.DataValue[j])
                        break;
                    SwapAnimation(j - 1, j);
                    this.Dispatcher.Invoke(new Action(() => { dataSet.MarkBars(j - 1, j, 2); }));
                }
            }
            this.Dispatcher.Invoke(new Action(() => { dataSet.UnMark(0, Settings.TotalNums - 1); dataSet.IndicateA.Visibility = Visibility.Hidden; dataSet.IndicateB.Visibility = Visibility.Hidden; }));
        }
        void MergeArray(int first, int mid, int last, SortBar[] temp)
        {
            int i = first, j = mid + 1;
            int k = 0;
            this.Dispatcher.Invoke(new Action(() => { dataSet.MarkBars(first, last, 2); }));
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
            if (first < last)
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
        public void ShellSort()
        {
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Visible; dataSet.IndicateB.Visibility = Visibility.Visible; }));
            for (int ShellGap = Settings.TotalNums / 2; ShellGap > 0; ShellGap /= 2) 
            {
                for (int i = 0; i < Settings.TotalNums - ShellGap; i++)   
                {
                    for (int k = 0; k < Settings.TotalNums; k++)
                        if (((k - i) % ShellGap) == 0)
                            this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[k].UnHightLight(); }));
                        else
                            this.Dispatcher.Invoke(new Action(() => { dataSet.DataValue[k].DisOpacity(); }));
                    Thread.Sleep(Settings.TimeSpanMs);
                    for (int j = i + ShellGap; j >= ShellGap; j -= ShellGap)
                    {
                        this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(j - ShellGap); dataSet.IndicateB.MoveBar(j); }));
                        Thread.Sleep(Settings.TimeSpanMs);
                        if (dataSet.DataValue[j - ShellGap] <= dataSet.DataValue[j])
                            break;
                        SwapAnimation(j - ShellGap, j);
                    }
                }
            }
            this.Dispatcher.Invoke(new Action(() => { dataSet.UnMark(0, Settings.TotalNums - 1); dataSet.IndicateA.Visibility = Visibility.Hidden; dataSet.IndicateB.Visibility = Visibility.Hidden; }));
        }
        public void QuickSortRecursion(int _left,int _right)
        {
            int left = _left;
            int right = _right;
            SortBar temp;
            if (left < right)
            {
                temp = dataSet.DataValue[left];
                this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(left); dataSet.IndicateB.MoveBar(right); }));
                Thread.Sleep(Settings.TimeSpanMs);
                while (left != right)
                {
                    while (right > left)
                    {
                        if (dataSet.DataValue[right] < temp) 
                        {
                            SwapAnimation(left, right);
                            break;
                        }
                        this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateB.MoveBar(--right); }));
                        Thread.Sleep(Settings.TimeSpanMs);
                    }

                    while (left < right) 
                    {
                        if (dataSet.DataValue[left] > temp)
                        {
                            SwapAnimation(left, right);
                            break;
                        }
                        this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.MoveBar(++left); }));
                        Thread.Sleep(Settings.TimeSpanMs);
                    }
                }
                QuickSortRecursion(_left, left - 1);  
                QuickSortRecursion(right + 1, _right);  
            }
        }
        public void QuickSort()
        {
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Visible; dataSet.IndicateB.Visibility = Visibility.Visible; }));
            QuickSortRecursion(0, Settings.TotalNums - 1);
            this.Dispatcher.Invoke(new Action(() => { dataSet.IndicateA.Visibility = Visibility.Hidden; dataSet.IndicateB.Visibility = Visibility.Hidden; }));
        }
        public void BucketSort()
        {
            int i = 0;
            List<List<SortBar>> radix = new List<List<SortBar>>();
            for (i = 0; i < 10; i++)
                radix.Add(new List<SortBar>());
            for (int j = 10; j <= 1000; j *= 10)
            {
                for (i = 0; i < Settings.TotalNums; i++)
                {
                    int temp = (dataSet.DataValue[i] % j) / (j / 10);
                    radix[temp].Add(dataSet.DataValue[i]);
                }
                i = 0;
                this.Dispatcher.Invoke(new Action(() => { dataSet.CreatBucket(radix); }));
                Thread.Sleep(Settings.TimeSpanMs);
                for (int k = 0; k < 10; k++)
                {
                    for (int n = 0; n < radix[k].Count; n++)
                    {
                        dataSet.DataValue[i] = radix[k][n];
                        this.Dispatcher.Invoke(new Action(() => { radix[k][n].MoveBarToTemp(i++); }));
                        Thread.Sleep(Settings.TimeSpanMs);
                    }
                }
                for (int k = 0; k < 10; k++)
                    for (int n = 0; n < radix[k].Count; n++)
                        this.Dispatcher.Invoke(new Action(() => { radix[k][n].MoveBarDown(); }));
                Thread.Sleep(Settings.TimeSpanMs);
                this.Dispatcher.Invoke(new Action(() => { dataSet.DeleteBucket(radix); }));
                Thread.Sleep(Settings.TimeSpanMs);
                for (i = 0; i < 10; i++)
                    radix[i].Clear();
            }
        }
    }
}