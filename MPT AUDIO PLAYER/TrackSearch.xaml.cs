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
    /// Логика взаимодействия для TrackSearch.xaml
    /// </summary>
    public partial class TrackSearch : UserControl
    {
        public TrackSearch()
        {
            InitializeComponent();
        }

        private async void search_click(object sender, RoutedEventArgs e)
        {
            await Network.SearchTrack(txt_search.Text, search_callback);
        }

        private void search_callback(bool success, List<Track> responce_tracks)
        {
            if (success)
                tracks.set_tracks(responce_tracks);
        }
    }
}
