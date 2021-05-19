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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            open_login();
        }

        public void open_register()
        {
            Content = new Register() { back_click = open_login, register_callback = open_login };
        }
        public void open_login()
        {
            Content = new Login() { register_click = open_register, login_callback = open_main_page };
        }

        public void open_main_page()
        {
            Content = new MainPage() { };
        }


    }
}
