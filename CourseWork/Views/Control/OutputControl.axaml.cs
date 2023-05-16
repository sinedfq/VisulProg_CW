using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace CourseWork.Views.Control
{
    public class OutputControl : TemplatedControl
    {
        public static readonly StyledProperty<bool> SignalProperty =
            AvaloniaProperty.Register<OutputControl, bool>("Signal");
        public static readonly StyledProperty<bool> FocusOnElementProperty =
            AvaloniaProperty.Register<OutputControl, bool>("FocusOnElement");

        public bool FocusOnElement
        {
            get => GetValue(FocusOnElementProperty);
            set => SetValue(FocusOnElementProperty, value);
        }

        public bool Signal
        {
            get => GetValue(SignalProperty);
            set => SetValue(SignalProperty, value);
        }
    }
}
