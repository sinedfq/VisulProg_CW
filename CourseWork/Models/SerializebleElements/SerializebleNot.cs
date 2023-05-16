using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.SerializebleElements
{
    public class SerializebleNot : SerializebleLogicalElement
    {
        public bool Input { get; set; }
        public bool IsConnected { get; set; }
    }
}
