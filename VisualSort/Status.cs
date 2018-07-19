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
        private bool _isrunning, _isdatavalid;
        public Status()
        {
            IsGenerateEn = true;
            IsDataValid = false;
        }
        public bool IsGenerateEn
        {
            get { return _isrunning; }
            set
            {
                _isrunning = value;
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsGenerateEn"));
            }
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
