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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public Action logout_callback = () => { };
        public MainPage()
        {
            InitializeComponent();
        }

        private void logout_click(object sender, RoutedEventArgs e)
        {
            logout_callback();
        }

        public void response_message(bool res, string message)
        {
            /*txt_message.Visibility = Visibility.Visible;
            txt_message.Content = message;
            if (res) register_callback();*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerControl.MediaPlayer.Source = new Uri(txt_link.Text);
        }
    }
}
