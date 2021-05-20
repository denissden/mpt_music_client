using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MPT_AUDIO_PLAYER
{
    /// <summary>
    /// Логика взаимодействия для Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        bool IsPlaying = false;
        bool IsDraggingSlider = false;
        public Player()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_tick;
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if (MediaPlayer.Source != null && MediaPlayer.NaturalDuration.HasTimeSpan && !IsDraggingSlider)
            {
                MediaSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                MediaSlider.Value = MediaPlayer.Position.TotalSeconds;
            }                                                                           
        }

        private void btn_play_click(object sender, RoutedEventArgs e)
        {
            if (IsPlaying) MediaPlayer.Pause();
            else MediaPlayer.Play();
            IsPlaying = !IsPlaying;
        }

        private void slider_drag_start(object sender, RoutedEventArgs e)
        {
            IsDraggingSlider = true;
        }

        private void slider_drag_end(object sender, RoutedEventArgs e)
        {
            IsDraggingSlider = false;
            MediaPlayer.Position = TimeSpan.FromSeconds(MediaSlider.Value);
        }
    }
}
