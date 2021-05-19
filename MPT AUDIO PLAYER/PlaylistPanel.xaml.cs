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
        public PlaylistPanel()
        {
            InitializeComponent();
            lbx_playlists.ItemsSource = Playlists;
        }
        
        public void load_playlists()
        {

        }

    }
}
