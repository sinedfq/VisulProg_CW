using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.LogicalElements
{
    public class SemiSummatorGate : TwoInputsGate
    {
        public override bool Output()
        {
            return input1 && input2;
        }

        public bool Output2()
        {
            return input1 ^ input2;   
        }
    }
}
