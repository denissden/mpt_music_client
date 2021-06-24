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
    /// Логика взаимодействия для TrackContainer.xaml
    /// </summary>
    public partial class TrackContainer : UserControl
    {
        public Track track { get; set; }
        public int index { get; set; }
        public TrackList parent;

        public TrackContainer(Track t, int i, TrackList p)
        {
            index = i;
            track = t;
            parent = p;
            InitializeComponent();
            menu_playlists.ItemsSource = new PlaylistPanel().Playlists;
            DataContext=this;
        }

        private async void click_add_to_playlist(object sender, RoutedEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            int tag;
            if (int.TryParse(mi.Tag.ToString(), out tag))
                await Network.AddTrackToPlaylist(track.id, tag, add_playlist_callback);  
        }

        private void add_playlist_callback(bool success, string message)
        {
            if (success)
                Success.Show("Added track to playlist");
            else
                Error.Show(message);
        }

        private void grid_left_mouse_down(object sender, MouseButtonEventArgs e)
        {
            if (MainPage.current_main_page != null)
            {
                MainPage.current_main_page.PlayerControl.set_track(track);
                if (parent != null)
                    MainPage.current_main_page.PlayerControl.set_playlist(parent.playlist, index - 1);
                MainPage.current_main_page.PlayerControl.MediaPlayer.Play();
            }
        }
    }
}
