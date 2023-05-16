using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace CourseWork.Views.Control
{
    public class NotControl : TemplatedControl
    {
        public static readonly StyledProperty<bool> FocusOnElementProperty =
            AvaloniaProperty.Register<NotControl, bool>("FocusOnElement");

        public bool FocusOnElement
        {
            get => GetValue(FocusOnElementProperty);
            set => SetValue(FocusOnElementProperty, value);
        }
    }
}
