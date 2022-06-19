using System.Windows.Input;

namespace AcFunDGJ;

/// <summary>
///     ListWindow.xaml 的交互逻辑
/// </summary>
public partial class ListWindow
{
    public ListWindow()
    {
        InitializeComponent();
    }

    private void ListWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}