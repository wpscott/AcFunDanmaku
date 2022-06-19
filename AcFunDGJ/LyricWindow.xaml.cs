using System.Windows.Input;

namespace AcFunDGJ;

public partial class LyricWindow
{
    public LyricWindow()
    {
        InitializeComponent();
    }

    private void LyricWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}