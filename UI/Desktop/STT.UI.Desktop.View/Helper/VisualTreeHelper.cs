using System.Windows;

namespace STT.UI.Desktop.View.Helper
{
    internal static class VisualTreeHelper
    {
        public static T GetParent<T>(DependencyObject root) where T: DependencyObject
        {
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(root);
            while (parent != null && parent.GetType() != typeof(T))
            {
                parent = System.Windows.Media.VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }
    }
}
