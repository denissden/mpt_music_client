using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT_AUDIO_PLAYER
{
    class Debug
    {
        public static MainWindow window;

        public static void Show(string message)
        {
            if (window != null)
                window.show_debug(message);
        }
    }
}
