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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Action register_click = () => { };
        public Action login_callback = () => { };
        public Login()
        {
            InitializeComponent();
        }

        private void register_click_(object sender, RoutedEventArgs e)
        {
            register_click();
        }

        private async void login_click(object sender, RoutedEventArgs e)
        {
            txt_message.Visibility = Visibility.Hidden;
            await Network.Login(
               txt_login.Text,
               txt_password.Password,
               response_message
               );
        }

        public void response_message(bool res, string message)
        {
            txt_message.Visibility = Visibility.Visible;
            txt_message.Content = message;
            if (res) login_callback();
        }
    }
}
