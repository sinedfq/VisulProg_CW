using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace CourseWork.Views.Control
{
    public class InputControl : TemplatedControl
    {
        public static readonly StyledProperty<bool> SignalProperty =
            AvaloniaProperty.Register<InputControl, bool>("Signal");
        public static readonly StyledProperty<bool> FocusOnElementProperty =
            AvaloniaProperty.Register<InputControl, bool>("FocusOnElement");

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
