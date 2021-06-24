using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class PlaylistPanel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<Playlist> _playlists = new ObservableCollection<Playlist>();
        public ObservableCollection<Playlist> Playlists
        {
            get { return _playlists; }
            set
            {
                if (_playlists == value) return;
                _playlists = value;
                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Playlists"));
            }
        }
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
            Playlists = new ObservableCollection<Playlist>(p);
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
                load_playlists();
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
               Error.Show("Error creating playlist");
            }
        }

        private void lbl_playlist_mouse_left(object sender, MouseButtonEventArgs e)
        {
           if (lbx_playlists.SelectedIndex != -1)
                main_page.change_window(new PlaylistPage(Playlists[lbx_playlists.SelectedIndex].id));
        }

        private void click_search(object sender, RoutedEventArgs e)
        {
                main_page.change_window(new TrackSearch());
        }

        private void click_upload(object sender, RoutedEventArgs e)
        {
            main_page.change_window(new UploadTrack());
        }        
        
        private void click_premium(object sender, RoutedEventArgs e)
        {
            main_page.change_window(new PremiumPage());
        }
    }
}
