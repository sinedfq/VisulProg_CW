using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.LogicalElements
{
    public class InputElement : LogicalElement
    {
        private bool signalOut;

        public InputElement() 
        {
            SignalOut = false;
        }

        public bool SignalOut
        {
            get => signalOut;
            set => SetAndRaise(ref signalOut, value);
        }
    }
}
