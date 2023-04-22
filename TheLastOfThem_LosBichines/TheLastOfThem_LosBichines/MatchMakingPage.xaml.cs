using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheLastOfThem_LosBichines
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MatchMakingPage : Page
    {
        private DispatcherTimer _timer;
        int value;
        bool navigated;

        public MatchMakingPage()
        {
            value = 0;
            navigated = false;
            this.InitializeComponent(); 
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.1) };

            _timer.Tick += (sender, o) => {
                if (!navigated)
                {
                    if (value >= 0 && value <= 95) value += 5;
                    else if (value > 95) value = 100;
                    Barra.Value = value;
                    if (value == 100) { GoToBattlePage(); value = -1; }
                }
            };
            _timer.Start();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void GoToBattlePage()
        {
            Frame.Navigate(typeof(BattlePage));
            navigated = true;
        }
    }
}
