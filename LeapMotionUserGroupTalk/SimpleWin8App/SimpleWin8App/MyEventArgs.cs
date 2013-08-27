using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWin8App
{
    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(int fingerCount)
        {
            FingerCount = fingerCount;
        }

        public int FingerCount { get; set; }
    }
}
