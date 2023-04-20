using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheLastOfThem_LosBichines
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 

    public sealed partial class BattlePage : Page
    {

        VMBButton BichinClickao;

        public ObservableCollection<VMBichin> ListaBichines { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesDefensa { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesMineros { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesAtaque { get; } = new ObservableCollection<VMBichin>();

        public ObservableCollection<VMBButton> BotonesBichines { get; } = new ObservableCollection<VMBButton>();

        private DispatcherTimer _timer;
        // Variables de tropas
        int currentTropes, maxtropes, goldNeeded, increaseCost, max;

        // Variables para la pantalla de final de partida:
        int deployedTropes, killedTropes, matchDuration;

        // Variables de monedas
        int gold, goldPS;

        //Variables para los botones
        Visibility isActive;

        public BattlePage()
        {
            this.InitializeComponent();

            BichinClickao = null;

            currentTropes = gold = deployedTropes = killedTropes = matchDuration = 0;
            maxtropes = 16;
            goldPS = 1;
            goldNeeded = 15;
            increaseCost = 15;
            max = 32;

            isActive = Visibility.Collapsed;
            PopUp.Visibility = isActive;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (sender, o) => {
                gold += goldPS;
                Gold.Text = gold.ToString() + " Oro";
                OtroTextoDinero.Text = gold.ToString() + " Oro";
                matchDuration++;
            };

            _timer.Start();

            GoldPerSecond.Text = goldPS.ToString() + " Oro/s";
            InfoTropes.Text = currentTropes.ToString() + " / " + maxtropes.ToString();
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

            if (BotonesBichines != null)
            {
                foreach(BotonBichin bb in ButtonModel.GetAllButtons())
                {
                    VMBButton VMitem = new VMBButton(bb);
                    BotonesBichines.Add(VMitem);
                }
            }
            base.OnNavigatedTo(e);
        }

        private void OnItemDrag(UIElement sender, DragStartingEventArgs args)
        {

        }

        private void DropOnCanvas(object sender, DragEventArgs e)
        {

        }

        private void upgradeTrope(object sender, RoutedEventArgs e)
        {
            gold -= BichinClickao.Nivel * 10;
            Gold.Text = gold.ToString() + " Oro";

            //foreach (VMBButton bichin in BotonesBichines)
            //{
            //    if (bichin.Name == BichinClickao.Name)
            //    {
            //        bichin.Nivel++;
            //        GridViewItem gv = PanelBotones.Items[bichin.Id] as GridViewItem;
            //        Grid g = PanelBotones.Items[bichin.Id] as Grid;
            //        ((TextBlock)g.Children[3]).Text = bichin.Nivel.ToString();
            //    }
            //}
        }

        private void unlockTrope(object sender, RoutedEventArgs e)
        {
            gold -= 10;
            Gold.Text = gold.ToString() + " Oro";
            Grid g = null;

            //foreach(VMBichin bichin in ListaBichines)
            //{
            //    if (bichin.Name == BichinClickao.Name)
            //    {
            //        bichin.Desbloqueado = Visibility.Collapsed;
            //        if (bichin.Group == "Defensas")
            //        {
            //            BichinesDefensa[bichin.Id].Seleccionable = true;
            //        }
            //    }
            //}
        }

        private void DraggingOverCanvas(object sender, DragEventArgs e)
        {

        }

        private void ItemKeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            BichinClickao = null;
            Mejorar.Visibility = Visibility.Collapsed;
            mejorar.Visibility = Visibility.Collapsed;
            Desbloquear.Visibility = Visibility.Collapsed;
            desbloquear.Visibility = Visibility.Collapsed;
            PopUp.Visibility = Visibility.Collapsed;
            UpgradesMenu.Visibility = Visibility.Collapsed;
        }

        private void ClickOnTropeButton(object sender, ItemClickEventArgs e)
        {
            VMBButton b = e.ClickedItem as VMBButton;
            BichinClickao = b;
            isActive = Visibility.Visible;
            PopUp.Visibility = isActive;
            
            if (b.Disponible == true)
            {
                if (b.Nivel < 3)
                {
                    int coste = 10 * b.Nivel;
                    PriceText.Text = "Precio " + coste;
                    if (gold >= coste)
                    {
                        Mejorar.Visibility = Visibility.Visible;
                        mejorar.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        mejorar.Visibility = Visibility.Visible;
                        Mejorar.Visibility = Visibility.Collapsed;
                    }
                }
            }

            else
            {
                PriceText.Text = "Precio " + 10;
                if (gold >= 10)
                {
                    Desbloquear.Visibility = Visibility.Visible;
                    desbloquear.Visibility = Visibility.Collapsed;
                }
                else
                {
                    desbloquear.Visibility = Visibility.Visible;
                    Desbloquear.Visibility = Visibility.Collapsed;
                }
            }

            
        }

        private void SecondBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            BichinClickao = null;

            Mejorar.Visibility = Visibility.Collapsed;
            mejorar.Visibility = Visibility.Collapsed;
            Desbloquear.Visibility = Visibility.Collapsed;
            desbloquear.Visibility = Visibility.Collapsed;
            PopUp.Visibility = Visibility.Collapsed;
        }

        private void Open_UpgradesMenu(object sender, RoutedEventArgs e)
        {
            UpgradesMenu.Visibility = Visibility.Visible;
        }

        private void MoreTropes(object sender, RoutedEventArgs e)
        {
            if (maxtropes < max && gold >= goldNeeded)
            {
                gold -= goldNeeded;
                maxtropes += 4;
                goldNeeded += increaseCost;
            }
            InfoTropes.Text = currentTropes.ToString() + " / " + maxtropes.ToString();
            Gold.Text = gold.ToString() + " Oro";
            OtroTextoDinero.Text = gold.ToString() + " Oro";
        }
    }
}
