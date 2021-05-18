using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
                                                     
namespace MPT_AUDIO_PLAYER
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            if (IsPlaying) Player.Pause();
            else Player.Play();
            IsPlaying = !IsPlaying;
        }
    }
}
