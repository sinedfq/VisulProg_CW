using Avalonia;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.LogicalElements
{
    public abstract class LogicalElement : Element
    {
        protected Point startPoint;

        public Point StartPoint
        {
            get => startPoint;
            set
            {
                Point oldPoint = StartPoint;
                SetAndRaise(ref startPoint, value);
                if (ChangeStartPoint != null)
                {
                    ChangeStartPointEventArgs args = new ChangeStartPointEventArgs
                    {
                        OldStartPoint = oldPoint,
                        NewStartPoint = StartPoint
                    };
                    ChangeStartPoint(this, args);
                }
            }
        }
        public event EventHandler<ChangeStartPointEventArgs> ChangeStartPoint;
    }
}
