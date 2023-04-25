using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class EndPage : Page
    {
        Color color;

        public EndPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var info = e.Parameter as Info;

            string code = "";
            if (info.win)
            {
                code = "#FFD1EE91";
                Titulo.Text = "-VICTORIA-";
            }
            else
            {
                code = "#FFE93578";
                Titulo.Text = "-DERROTA-";
            }
            Titulo.Foreground = GetSolidColorBrush(code);

            TropesStat.Foreground = GetSolidColorBrush(code);
            TropesValue.Foreground = GetSolidColorBrush(code);
            TropesntStat.Foreground = GetSolidColorBrush(code);
            TropesntValue.Foreground = GetSolidColorBrush(code);
            GoldStat.Foreground = GetSolidColorBrush(code);
            GoldValue.Foreground = GetSolidColorBrush(code);
            TimeStat.Foreground = GetSolidColorBrush(code);
            TimeValue.Foreground = GetSolidColorBrush(code);

            int min = info.segundos / 60;
            int seg = info.segundos % 60;

            TropesValue.Text = info.tropasDesplegadas.ToString();
            TropesntValue.Text = info.tropasRetiradas.ToString();
            GoldValue.Text = info.oro.ToString();
            TimeValue.Text = min.ToString() + " min " + seg.ToString() + " seg";
        }

        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        private void Leave(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
