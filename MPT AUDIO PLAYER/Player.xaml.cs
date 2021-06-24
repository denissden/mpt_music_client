using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        Playlist CurrentPlaylist;
        Playlist CurrentPlaylistRandom;
        int PlaylistIndex = 0;

        public Player()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.DataBind);
            timer.Interval = TimeSpan.FromMilliseconds(200);
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

        private void btn_back_click(object sender, RoutedEventArgs e)
        {
            if (PlaylistIndex > 0)
            {
                PlaylistIndex -= 1;
            }
            set_track_by_index();
        }

        private void btn_forward_click(object sender, RoutedEventArgs e)
        {
            if (PlaylistIndex < CurrentPlaylist.tracks.Count - 1)
            {
                PlaylistIndex += 1;
            }
            else if (btn_repeat.IsChecked == true && PlaylistIndex == CurrentPlaylist.tracks.Count - 1)
            {
                PlaylistIndex = 0;
            }
            set_track_by_index();
        }

        private void btn_random_click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            CurrentPlaylistRandom = new Playlist();
            CurrentPlaylistRandom.tracks = CurrentPlaylist.tracks.OrderBy(item => rnd.Next()).ToList();
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

        private void volume_drag_end(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Volume = VolumeSlider.Value / 100;
        }

        public void set_track(Track t)
        { 
            MediaPlayer.Source = new Uri(Network.URL + "/track/" + t.id);
            lbl_track.Content = t.name;
        }

        public void set_track_by_index()
        {
            if (btn_random.IsChecked == true)
                set_track(CurrentPlaylistRandom.tracks[PlaylistIndex]);
            else
                set_track(CurrentPlaylist.tracks[PlaylistIndex]);
        }

        public void set_playlist(Playlist p, int i)
        {
            CurrentPlaylist = p;
            btn_random_click(null, null);
        }

    }
}
