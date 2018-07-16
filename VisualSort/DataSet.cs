using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace VisualSort
{
    class DataSet
    {
        public List<SortBar> DataValue;
        public IndicateBar IndicateA, IndicateB;
        double canvas_height, canvas_width;
        Canvas canvas;
        public DataSet(Canvas canvas, int num,double gap=0)
        {
            canvas_height = canvas.ActualHeight;
            canvas_width = canvas.ActualWidth;
            DataValue = new List<SortBar>();
            Random r = new Random();
            this.canvas = canvas;
            IndicateA = new IndicateBar(canvas_height, (canvas_width - (num - 1) * gap) / num, new SolidColorBrush(Colors.Orange), "指示器A", gap);
            IndicateB = new IndicateBar(canvas_height, (canvas_width - (num - 1) * gap) / num, new SolidColorBrush(Colors.Green), "指示器B", gap);
            IndicateA.Visibility = System.Windows.Visibility.Hidden;
            IndicateB.Visibility = System.Windows.Visibility.Hidden;
            for (int i = 0; i < num; i++)
                DataValue.Add(new SortBar(r.Next(50, 300), (canvas_width - (num - 1) * gap) / num, i, gap));
            ModifyRelaHeight();
        }
        public void InitCanvas()
        {
            for (int i = 0; i < DataValue.Count; i++)
            {
                canvas.Children.Add(DataValue[i]);
                Canvas.SetBottom(DataValue[i], 0);
                Canvas.SetLeft(DataValue[i], DataValue[i].LeftPosition);
                Canvas.SetZIndex(DataValue[i], 0);
                DataValue[i].InitSortBar();
            }
            canvas.Children.Add(IndicateA);
            Canvas.SetZIndex(IndicateA, 100);
            canvas.Children.Add(IndicateB);
            Canvas.SetZIndex(IndicateB, 100);
        }
        public void MarkBars(int first, int last, int colors)
        {
            for (int i = first; i <= last; i++)
                DataValue[i].HightLight(colors);
        }
        public void UnMark(int first,int last)
        {
            for (int i = first; i <= last; i++)
                DataValue[i].UnHightLight();
        }
        public void ModifyRelaHeight(double scale=0.9)
        {
            double min_v = canvas_height, max_v = 0;
            int min_index = 0, max_index = 0;
            for (int i = 0; i < DataValue.Count; i++)
            {
                if (min_v > DataValue[i].Height)
                {
                    min_v = DataValue[i].Height;
                    min_index = i;
                }
                if (max_v < DataValue[i].Height)
                {
                    max_v = DataValue[i].Height;
                    max_index = i;
                }
            }
            for(int i=0;i<DataValue.Count;i++)
            {
                DataValue[i].Height = DataValue[i].Height / max_v * scale * canvas_height;
            }
        }
        public void SwapBar(int index1,int index2)
        {
            SortBar temp = DataValue[index1];
            DataValue[index1].MoveBar(index2);
            DataValue[index2].MoveBar(index1);
            DataValue[index1] = DataValue[index2];
            DataValue[index2] = temp;            
        }
        public void CreatBucket(List<List<SortBar>> buckets)
        {
            List<Button> source = new List<Button>();
            int add = 0;
            for (int i = 0; i < 10; i++)
            {
                if (buckets[i].Count > 0)
                {
                    int index = source.Count;
                    source.Add(new Button { Width = DataValue[buckets[i].Count + add - 1].LeftPosition + DataValue[0].Width - DataValue[add].LeftPosition, Background = new SolidColorBrush(Colors.LightGray), Content = "桶" + i.ToString(), HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center, VerticalContentAlignment = System.Windows.VerticalAlignment.Top, BorderThickness = new System.Windows.Thickness(0), FontSize = 15, Foreground = new SolidColorBrush(Colors.DarkSlateGray) });
                    canvas.Children.Add(source[index]);
                    canvas.RegisterName("Bucket" + i.ToString(), source[index]);
                    Canvas.SetBottom(source[index], 0);
                    Canvas.SetLeft(source[index], DataValue[add].LeftPosition);
                    Canvas.SetZIndex(source[index], -1);
                    DoubleAnimation s = new DoubleAnimation { From = 0, To = canvas_height, Duration = TimeSpan.FromSeconds(Settings.InitTimeSpan), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };
                    source[index].BeginAnimation(Button.HeightProperty, s);
                    add += buckets[i].Count;
                }
            }
        }
        public void DeleteBucket(List<List<SortBar>> buckets)
        {
            for (int i = 0; i < 10; i++)
            {
                if (buckets[i].Count > 0)
                {
                    canvas.Children.Remove(canvas.FindName("Bucket" + i.ToString()) as Button);
                    canvas.UnregisterName("Bucket" + i.ToString());
                }
            }
        }
    }
}
