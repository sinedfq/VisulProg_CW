﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.LogicalElements
{
    public class NotGate : Gate
    {
        private bool input;
        private bool isConnected;

        public NotGate()
        {
            IsConnected = false;
        }
        
        public override bool Output()
        {
            return isConnected && !input;
        }
        public bool Input
        {
            get => input;
            set => SetAndRaise(ref input, value);
        }
        public bool IsConnected
        {
            get => isConnected;
            set => SetAndRaise(ref isConnected, value);
        }
    }
}
