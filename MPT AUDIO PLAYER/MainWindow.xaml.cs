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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Error.window = this;
            Debug.window = this;
            new Thread(new ThreadStart(() => { Config c = Config.Read(); c.Load(); })).Start();
            open_login();
        }

        public void open_register()
        {
            cnt_main.Content = new Register() { back_click = open_login, register_callback = open_login };
        }
        public void open_login()
        {
            cnt_main.Content = new Login() { register_click = open_register, login_callback = open_main_page };
        }

        public void open_main_page()
        {
            cnt_main.Content = new MainPage() { };
        }

        public void show_error(string message)
        {
            txt_error_message.Text = message;
            error_message.Visibility = Visibility.Visible;
        }

        public void show_debug(string message)
        {
            txt_debug_message.Text = message;
        }

        private void close_error(object sender, RoutedEventArgs e)
        {
            error_message.Visibility = Visibility.Collapsed;
        }
    }
}
