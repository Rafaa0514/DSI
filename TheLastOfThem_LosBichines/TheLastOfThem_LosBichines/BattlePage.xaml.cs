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
        public ObservableCollection<VMBichin> ListaBichines { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesDefensa { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesMineros { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesAtaque { get; } = new ObservableCollection<VMBichin>();

        private DispatcherTimer _timer;
        // Variables de tropas
        int currentTropes, maxtropes, goldNeeded, increaseCost, max;

        // Variables para la pantalla de final de partida:
        int deployedTropes, killedTropes, matchDuration;

        // Variables de monedas
        int gold, goldPS;

        public BattlePage()
        {
            this.InitializeComponent();

            string[] names = new string[15];

            currentTropes = gold = deployedTropes = killedTropes = matchDuration = 0;
            maxtropes = 16;
            goldPS = 1;
            goldNeeded = 15;
            increaseCost = 15;
            max = 32;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (sender, o) => {
                gold += goldPS;
                Gold.Text = gold.ToString() + " Oro";
                matchDuration++;
            };

            _timer.Start();

            GoldPerSecond.Text = goldPS.ToString() + " Oro/s";
            InfoTropes.Text = currentTropes.ToString() + " / " + maxtropes.ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            //if (ListaBichines != null)
            //    foreach (Bichin dron in Model.GetAllBichines())
            //    {
            //        VMBichin VMitem = new VMBichin(dron);
            //        ListaBichines.Add(VMitem);
            //    }

            if (BichinesDefensa != null)
                foreach (Bichin d in Model.GetDefenseBichines())
                {
                    VMBichin VMitem = new VMBichin(d);
                    BichinesDefensa.Add(VMitem);
                }

            if (BichinesMineros != null)
                foreach (Bichin d in Model.GetMineBichines())
                {
                    VMBichin VMitem = new VMBichin(d);
                    BichinesMineros.Add(VMitem);
                }

            if (BichinesAtaque != null)
                foreach (Bichin d in Model.GetAttackBichines())
                {
                    VMBichin VMitem = new VMBichin(d);
                    BichinesAtaque.Add(VMitem);
                }
            base.OnNavigatedTo(e);
        }

        private void OnItemDrag(UIElement sender, DragStartingEventArgs args)
        {

        }

        private void DropOnCanvas(object sender, DragEventArgs e)
        {

        }

        private void DraggingOverCanvas(object sender, DragEventArgs e)
        {

        }

        private void ItemKeyDown(object sender, KeyRoutedEventArgs e)
        {

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
        }
    }
}
