using System.Windows.Input;
using System.Windows.Shell;
using AcFunDanmuSongRequest;
using AcFunDGJ.ViewModels;

namespace AcFunDGJ;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        WindowChrome.SetWindowChrome(this, new WindowChrome());

        (DataContext as DGJViewModel)!.Player = Player;

        Loaded += (_, _) =>
        {
            if (DGJ.ShowLyrics) (DataContext as DGJViewModel)!.LyricsCommand.Execute(null);

            (DataContext as DGJViewModel)!.ListCommand.Execute(null);
        };
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                Player.Stop();
                (DataContext as DGJViewModel)!.CloseCommand.Execute(null);
                Close();
                break;
        }
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Cursor = Cursors.SizeAll;
        DragMove();
        Cursor = Cursors.Arrow;
    }
}