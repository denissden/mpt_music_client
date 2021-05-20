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
    /// Логика взаимодействия для Playlist.xaml
    /// </summary>
    public partial class PlaylistPanel : UserControl
    {
        public List<Playlist> Playlists = new List<Playlist>();
        public MainPage main_page;
        public PlaylistPanel()
        {
            InitializeComponent();
            lbx_playlists.ItemsSource = Playlists;
            load_playlists();
        }
        
        public async void load_playlists()
        {
            await Network.LoadPlaylists(onload_callback);
        }

        private void onload_callback(bool success, List<Playlist> p)
        {
            Playlists = p;
            lbx_playlists.ItemsSource = Playlists;
        }

        private async void click_add_playlist(object sender, RoutedEventArgs e)
        {
            if (txt_playlist_name.Text.Length == 0)
            {
                txt_playlist_name.Visibility = Visibility.Visible;
            }
            else
            {
                btn_add_playlist.IsEnabled = false;
                await Network.AddPlaylist(txt_playlist_name.Text, add_playlist_callback);
            }
        }

        private void add_playlist_callback(bool success, Playlist? p)
        {
            btn_add_playlist.IsEnabled = true;
            if (success)
            {
                Playlists.Add(p.Value);
                txt_playlist_name.Visibility = Visibility.Collapsed;
            }
            else
            {
               // Error.Show("Error creating playlist");
            }
        }

        private void lbl_playlist_mouse_left(object sender, MouseButtonEventArgs e)
        {
            if (lbx_playlists.SelectedIndex != -1)
            main_page.change_window(new TextBox() { Text = Playlists[lbx_playlists.SelectedIndex].name });
        }
    }
}
