using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.LogicalElements
{
    public abstract class Gate : LogicalElement
    {
        public abstract bool Output();

    }
}
