using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AcFunDanmuSongRequest;
using AcFunDGJ.ViewModels;

namespace AcFunDGJ
{
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
}