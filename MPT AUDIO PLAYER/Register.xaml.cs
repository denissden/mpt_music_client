using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        public Action back_click = () => { };
        public Action register_callback = () => { };
        public Register()
        {
            InitializeComponent();
        }

        private void back_click_(object sender, RoutedEventArgs e)
        {
            back_click();
        }

        private async void register_click(object sender, RoutedEventArgs e)
        {
            txt_message.Visibility = Visibility.Hidden;
            await Network.Register(
               txt_login.Text,
               txt_password.Password,
               txt_email.Text,
               response_message
               );
        }

        public void response_message(bool res, string message)
        {
            txt_message.Visibility = Visibility.Visible;
            txt_message.Content = message;
            if (res) register_callback();
        }
    }
}
