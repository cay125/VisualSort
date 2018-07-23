using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VisualSort
{
    class Status : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isdatavalid, _isrunning;
        private string _toRunContent;
        public Status()
        {
            IsRunning = false;
            IsDataValid = false;
        }
        public bool IsRunning
        {
            get { return _isrunning; }
            set
            {
                _isrunning = value;
                if (_isrunning == true)
                    _toRunContent = "停止";
                else
                    _toRunContent = "运行";
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsGenerateEn"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ToRunContent"));
                }
            }
        }
        public string ToRunContent
        {
            get { return _toRunContent; }
        }
        public bool IsGenerateEn
        {
            get { return !_isrunning; }
        }
        public bool IsDataValid
        {
            get { return _isdatavalid; }
            set
            {
                _isdatavalid = value;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsDataValid"));
            }
        }
        
    }
}
