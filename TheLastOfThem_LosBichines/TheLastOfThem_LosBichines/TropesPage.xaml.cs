using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class TropesPage : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        // Referencia a la tropa que seleccionamos en el Menu de tropas
        VMBichin BichinClickao = null;

        // Listas de Bichines (Panel de tropas)
        public ObservableCollection<VMBichin> ListaBichines { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesDefensa { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesMineros { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesAtaque { get; } = new ObservableCollection<VMBichin>();

        private bool _bichinSelected = false;
        public bool BichinNotSelected => !_bichinSelected;
        public Visibility ShowingInfoWindow => (!_bichinSelected) ? Visibility.Visible : Visibility.Collapsed;
        public string SelectedTropeName => (BichinClickao != null)?BichinClickao.Name:"";
        public string SelectedTropeType => (BichinClickao != null) ? BichinClickao.Type : "";
        public string SelectedTropeDescription => (BichinClickao != null) ? BichinClickao.Description : "";
        public string SelectedTropeImage => (BichinClickao != null) ? BichinClickao.Imagen : "Assets\\Images\\Bichines\\Bichin-PateaPiedras.png";


        public TropesPage()
        {
            this.InitializeComponent();
            _bichinSelected = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BichinNotSelected)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowingInfoWindow)));

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (BichinesDefensa != null)
                foreach (Bichin d in Model.GetDefenseBichines())
                {
                    VMBichin VMitem = new VMBichin(d);
                    BichinesDefensa.Add(VMitem);
                    ListaBichines.Add(VMitem);
                }

            if (BichinesMineros != null)
                foreach (Bichin d in Model.GetMineBichines())
                {
                    VMBichin VMitem = new VMBichin(d);
                    BichinesMineros.Add(VMitem);
                    ListaBichines.Add(VMitem);
                }

            if (BichinesAtaque != null)
                foreach (Bichin d in Model.GetAttackBichines())
                {
                    VMBichin VMitem = new VMBichin(d);
                    BichinesAtaque.Add(VMitem);
                    ListaBichines.Add(VMitem);
                }

            base.OnNavigatedTo(e);
        }
        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void Bichin_OnClick(object sender, ItemClickEventArgs e)
        {
            BichinClickao = e.ClickedItem as VMBichin;

            UnselectBichin();

            BackToTropesButton.Focus(FocusState.Programmatic);
        }

        private void BackToTropesButton_OnClick(object sender, RoutedEventArgs e)
        {
            _bichinSelected = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BichinNotSelected)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowingInfoWindow)));
        }

        private void UnselectBichin()
        {
            _bichinSelected = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BichinNotSelected)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowingInfoWindow)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTropeName)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTropeType)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTropeDescription)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTropeImage)));
        }
    }
}
