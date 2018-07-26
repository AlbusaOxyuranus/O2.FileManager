using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace O2.FileManager.Helpers.Behaviors
{
    public class ClickBehavior
    {
        public static DependencyProperty DoubleClickCommandProperty = DependencyProperty.RegisterAttached("DoubleClick",
            typeof(ICommand),
            typeof(ClickBehavior),
            new FrameworkPropertyMetadata(null, DoubleClickChanged));

        public static void SetDoubleClick(DependencyObject target, ICommand value)
        {
            target.SetValue(DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClick(DependencyObject target)
        {
            return (ICommand) target.GetValue(DoubleClickCommandProperty);
        }

        private static void DoubleClickChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as ListViewItem;
            if (element != null)
            {
                if (e.NewValue != null && e.OldValue == null)
                    element.MouseDoubleClick += element_MouseDoubleClick;
                else if (e.NewValue == null && e.OldValue != null) element.MouseDoubleClick -= element_MouseDoubleClick;
            }
        }

        private static void element_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = (UIElement) sender;
            var command = (ICommand) element.GetValue(DoubleClickCommandProperty);
            command.Execute(null);
        }
    }
}