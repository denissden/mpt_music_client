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
    /// Логика взаимодействия для PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : UserControl
    {
        public string playlist_name { get; set; }
        public int playlist_id;
        List<Track> tracks_list;
        public PlaylistPage(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            tracks.ItemsSource = tracks_list;
            playlist_id = id;
        }

        public async void load_tracks()
        {
            await Network.LoadPlaylist(playlist_id, onload_callback);
        }

        public void onload_callback(bool success, Playlist? p)
        {
            if (p != null)
            {
                tracks_list = p.Value.tracks;
            }
        }
    }
}
