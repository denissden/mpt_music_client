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
    /// Логика взаимодействия для PremiumPage.xaml
    /// </summary>
    public partial class PremiumPage : UserControl
    {
        public PremiumPage()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            if (CurrentUser.premium)
            {
                lbl_main.Content = "You already have premium";
                btn_main.Content = "Remove premium";
            }
            else
            {
                lbl_main.Content = "You don't have premium";
                btn_main.Content = "Get premium";
            }
        }

        private async void toggle_premium(object sender, RoutedEventArgs e)
        {
            await Network.TogglePremium(!CurrentUser.premium, toggle_premium_callback);
        }

        private void toggle_premium_callback(bool success, string res)
        {
            CurrentUser.load_premium();
            if (success)
                Success.Show("Success");
            else
                Error.Show("Oops, error: " + res);
            init();
        }
    }
}
