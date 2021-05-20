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
        public int index { get; set; }
        public string name { get; set; }
        public string artist { get; set; }
        public DateTime duration { get; set; }
        public TrackContainer()
        {
            InitializeComponent();
        }
    }
}
