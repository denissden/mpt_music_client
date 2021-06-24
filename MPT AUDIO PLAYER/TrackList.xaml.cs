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
using System.Windows.Threading;

namespace MPT_AUDIO_PLAYER
{
    /// <summary>
    /// Логика взаимодействия для TrackList.xaml
    /// </summary>
    public partial class TrackList : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<TrackContainer> _tracks = new ObservableCollection<TrackContainer>();
        public ObservableCollection<TrackContainer> tracks
        {
            get { return _tracks; }
            set
            {
                if (_tracks == value) return;
                _tracks = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("tracks"));
            }
        }
        public Playlist playlist;

        public TrackList()
        {
            InitializeComponent();
            //items_tracks.ItemsSource = tracks;
            DataContext = this;
        }
        public void set_tracks(Playlist responce_playlist)
        {
            tracks.Clear();
            playlist = responce_playlist;
            var responce_tracks = responce_playlist.tracks;
            if (responce_tracks != null && responce_tracks.Count != 0)
                for (int i = 0; i < responce_tracks.Count; i++)
                    tracks.Add(new TrackContainer(responce_tracks[i], i + 1, this));
            else Error.Show("No tracks!");
        }
    }
}
                                     