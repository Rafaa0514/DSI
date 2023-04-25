using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class LogInMenu : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _userName;
        private string _password;

        public Visibility IsLogInAllowed => (UserName?.Trim().Length > 2 && UserName?.Trim().Length < 15 && Password?.Trim().Length > 2) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsLogInNotAllowed => ((IsLogInAllowed == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed);

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLogInAllowed)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLogInNotAllowed)));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLogInAllowed)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLogInNotAllowed)));
            }
        }



         public LogInMenu()
        {
            this.InitializeComponent();
        }

        private void AcceptButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), _userName);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) 
            {
                Frame.GoBack();
            }
        }
    }
}
