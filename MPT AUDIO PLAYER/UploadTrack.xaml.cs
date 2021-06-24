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
using System.IO;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace MPT_AUDIO_PLAYER
{
    /// <summary>
    /// Логика взаимодействия для UploadTrack.xaml
    /// </summary>
    public partial class UploadTrack : UserControl
    {
        public Action back_click = () => { };
        string TrackPath;
        public UploadTrack()
        {
            InitializeComponent();
        }

        private void back_click_(object sender, RoutedEventArgs e)
        {
            back_click();
        }
        private void select_file_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Filter = "Music (MP3)|*.MP3";
            var res = dia.ShowDialog();
            if (res == true) { 
                TrackPath = dia.FileName;
                txt_filename.Visibility = Visibility.Visible;
                string name = Path.GetFileName(dia.FileName);
                txt_filename.Content = name;
                if (txt_name.Text.Length == 0)
                {
                    int dash = name.IndexOf("-");
                    if (dash != -1)
                    {
                        txt_artist.Text = name.Substring(0, dash).Trim();
                        int a = name.Length;
                        txt_name.Text = name.Substring(dash + 1, name.Length - dash - 1).Trim().Replace(".mp3", "");
                    }

                }
            }
        }
        private async void upload_click(object sender, RoutedEventArgs e)
        {
            txt_message.Visibility = Visibility.Hidden;
            if (TrackPath == null) return;
            using (var f = new FileStream(TrackPath, FileMode.Open, FileAccess.Read))
            {
                await Network.UploadTrack(
                   txt_name.Text,
                   txt_artist.Text,
                   f,
                   response_message
                   );
            }
        }

        public void response_message(bool res, string message)
        {
            txt_message.Visibility = Visibility.Visible;
            txt_message.Content = message;
        }
    }
}
