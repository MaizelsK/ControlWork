using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWork
{
    public class TimerInterval
    {
        public static int Interval { get; set; }

        static TimerInterval()
        {
            Interval = 15000;
        }
    }
}
