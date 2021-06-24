using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT_AUDIO_PLAYER
{
    public class CurrentUser
    {
        private static bool _premium = false;
        public static bool premium { 
            get { return _premium; } 
            set { 
                _premium = value;
                if (MainPage.current_main_page != null)
                {
                    MainPage.current_main_page.img_ad.Visibility = value ?
                        System.Windows.Visibility.Collapsed :
                        System.Windows.Visibility.Visible;
                }
            } }

        public async static void load_premium()
        {
            await Network.PremiumStatus(onload_callback);
        }

        private static void onload_callback(bool success, string res)
        {
            premium = res == "True";
            Debug.Show(premium.ToString());
        }
    }
}
