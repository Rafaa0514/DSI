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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace TheLastOfThem_LosBichines
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TropesButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TropesPage));
        }

        private void BattleButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MatchMakingPage));
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsPage));
        }

        private void QuitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
