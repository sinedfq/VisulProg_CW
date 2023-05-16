using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace CourseWork.Views.Control
{
    public class XorControl : TemplatedControl
    {
        public static readonly StyledProperty<bool> FocusOnElementProperty =
            AvaloniaProperty.Register<XorControl, bool>("FocusOnElement");

        public bool FocusOnElement
        {
            get => GetValue(FocusOnElementProperty);
            set => SetValue(FocusOnElementProperty, value);
        }
    }
}
