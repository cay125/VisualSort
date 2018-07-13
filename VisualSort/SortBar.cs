using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows;

namespace VisualSort
{
    public class SortBar:Button
    {
        private int _index;
        private double gap, real_height;
        private SolidColorBrush normal = new SolidColorBrush(Colors.LightBlue);
        private SolidColorBrush[] hightlight = new SolidColorBrush[3];
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        public double LeftPosition
        {
            get { return (Width + gap) * Index; }
        }
        public static bool operator <  (SortBar s1,SortBar s2){ return s1.real_height < s2.real_height; }
        public static bool operator >  (SortBar s1, SortBar s2){ return s1.real_height > s2.real_height; }
        public static bool operator <= (SortBar s1, SortBar s2){ return s1.real_height <= s2.real_height; }
        public static bool operator >= (SortBar s1, SortBar s2){ return s1.real_height >= s2.real_height; }
        public SortBar(double height,double width,int index,double gap=0):base()
        {
            Background = normal;
            VerticalContentAlignment = VerticalAlignment.Bottom;
            this.gap = gap;
            if (height <= 0)
                Height = 80;
            else
                Height = height;
            Width = width;
            Index = index;
            Content = height.ToString();
            real_height = height;
            hightlight[0] = new SolidColorBrush(Colors.Orange);
            hightlight[1] = new SolidColorBrush(Colors.Green);
            hightlight[2] = new SolidColorBrush(Colors.BlueViolet);
            BorderThickness = new Thickness(0);
        }
        
        public void InitSortBar()
        {
            DoubleAnimation init_animation = new DoubleAnimation {From=0,To=Height,Duration=TimeSpan.FromSeconds(Settings.InitTimeSpan), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };
            BeginAnimation(HeightProperty, init_animation);
        }
        public void MoveBar(int to_index)
        {
            DoubleAnimation move_animation = new DoubleAnimation { From = LeftPosition, To = (Width + gap) * to_index, Duration = TimeSpan.FromSeconds(Settings.TimeSpanS), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };
            this.Index = to_index;
            BeginAnimation(Canvas.LeftProperty, move_animation);
        }
        public void MoveBarToTemp(int to_index)
        {
            DoubleAnimation index_animation = new DoubleAnimation { From = LeftPosition, To = (Width + gap) * to_index, Duration = TimeSpan.FromSeconds(Settings.TimeSpanS), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };          
            this.Index = to_index;
            MoveBarUp();
            BeginAnimation(Canvas.LeftProperty, index_animation);
        }
        public void MoveBarUp()
        {
            DoubleAnimation height_animation = new DoubleAnimation { From = 0, To = 100, Duration = TimeSpan.FromSeconds(Settings.TimeSpanS), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };
            BeginAnimation(Canvas.BottomProperty, height_animation);
        }
        public void MoveBarDown()
        {
            DoubleAnimation down_animation = new DoubleAnimation { From = 100, To = 0, Duration = TimeSpan.FromSeconds(Settings.TimeSpanS), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };
            BeginAnimation(Canvas.BottomProperty, down_animation);
        }
        public void HightLight(int i)
        {
            if (i != 0 && i != 1 && i != 2) 
                Background = normal;
            else
                Background = hightlight[i];
        }
        public void UnHightLight()
        {
            Background = normal;
            Opacity = 1;
        }
        public void DisOpacity()
        {
            Opacity = 0.3;
        }
    }
}
