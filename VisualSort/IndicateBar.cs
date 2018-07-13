using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace VisualSort
{
    class IndicateBar:Button
    {
        private int _index;
        private double gap;
        public int Index
        {
            set { _index = value; }
            get { return _index; }
        }
        public double LeftPosition
        {
            get { return _index * (Width + gap); }
        }
        public IndicateBar(double height, double width, Brush borderbrush, string name, double gap = 0) : base()
        {
            Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            BorderBrush = borderbrush;
            Height = height;
            Width = width;
            Index = 0;
            Content = name;
            VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            this.gap = gap;
        }
        public void MoveBar(int to_index)
        {
            DoubleAnimation move_animation = new DoubleAnimation { From = LeftPosition, To = (Width + gap) * to_index, Duration = TimeSpan.FromSeconds(Settings.TimeSpanS), EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut } };
            this.Index = to_index;
            BeginAnimation(Canvas.LeftProperty, move_animation);
        }

    }
}
