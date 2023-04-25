using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.OnlineId;
using Windows.System;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using static System.Net.Mime.MediaTypeNames;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheLastOfThem_LosBichines
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 


    // Struct de informacion para la siguiente pagina
    public class Info
    {
        public int segundos;
        public int tropasDesplegadas, tropasRetiradas;
        public int oro;
        public bool win;

        Info()
        {
            segundos = 0; tropasDesplegadas = 0; tropasRetiradas = 0; oro = 0; win = false;
        }

        public Info (int s, int td, int tr, int o, bool w)
        {
            segundos = s; tropasDesplegadas = td; tropasRetiradas = tr; oro = o; win = w;
        }
    }

    public sealed partial class BattlePage : Page
    {
        static Random random = new Random();
        //Enumerado de direcciones
        enum Direction { UP, DOWN, LEFT, RIGHT};
        bool control = true;

        // Constantes
        const double NUM_ROWS = 11;
        const double NUM_COLS = 26;
        double difX = 0.0;
        double difY = 0.0;

        // Referencia a la tropa que seleccionamos en el Menu de mejoras
        VMBButton BichinClickao;
        VMBichin BichinArrastrao;

        // Listas de Bichines (Panel de tropas y Panel de Botones del Menu de mejoras)
        public ObservableCollection<VMBichin> ListaBichines { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesDefensa { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesMineros { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBichin> BichinesAtaque { get; } = new ObservableCollection<VMBichin>();
        public ObservableCollection<VMBButton> BotonesBichines { get; } = new ObservableCollection<VMBButton>();
        public ObservableCollection<CVBichin> BichinesCanvas { get; } = new ObservableCollection<CVBichin>();

        // Timers
        private DispatcherTimer _timer;
        private DispatcherTimer _updateTimer;

        // Variables de tropas
        int currentTropes, maxtropes, goldNeeded, increaseCost, max, tropeCounter, tropesEliminated;

        // Variables para la pantalla de final de partida:
        int deployedTropes, killedTropes, matchDuration;

        // Variables de monedas
        int gold, goldPS, totalGold;

        // Variable de segundos
        int time;

        //Variables para los botones
        Visibility isActive;

        public BattlePage()
        {
            this.InitializeComponent();

            // Inicializamos variables
            BichinClickao = null;
            BichinArrastrao = null;
            currentTropes = gold = deployedTropes = killedTropes = matchDuration = 0;
            maxtropes = 16;
            goldPS = 1;
            goldNeeded = 15;
            increaseCost = 15;
            max = 32;
            isActive = Visibility.Collapsed;
            PopUp.Visibility = isActive;
            time = 0; totalGold = 0;
            tropeCounter = 0; tropesEliminated = 0;

            // Inicializamos timers y les añadimos su funcion
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (sender, o) => {
                if (control)
                {
                    gold += goldPS;
                    Gold.Text = gold.ToString() + " Oro";
                    OtroTextoDinero.Text = gold.ToString() + " Oro";
                    matchDuration++;
                    time++;
                    totalGold += goldPS;
                }
            };
            _timer.Start();

            _updateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.2) };
            _updateTimer.Tick += (sender, o) => { 
                if (control) checkButtons(); 
            };
            _updateTimer.Start();

            // Mostramos textos iniciales del HUD
            GoldPerSecond.Text = goldPS.ToString() + " Oro/s";
            InfoTropes.Text = currentTropes.ToString() + " / " + maxtropes.ToString();

            difX = MiTablero.ActualWidth / NUM_COLS;
            difY = MiTablero.ActualHeight / NUM_ROWS;
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

        // Funcion que comprueba que boton enseñar en el popUp de tropa
        private void checkButtons()
        {
            if (BichinClickao != null)
            {
                if (BichinClickao.Disponible)
                {
                    Desbloquear.Visibility = Visibility.Collapsed;
                    desbloquear.Visibility = Visibility.Collapsed;

                    if (BichinClickao.Nivel < 3)
                    {
                        int coste = 10 * BichinClickao.Nivel;
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
                    Mejorar.Visibility = Visibility.Collapsed;
                    mejorar.Visibility = Visibility.Collapsed;

                    int coste = 10 * BichinClickao.Nivel;
                    PriceText.Text = "Precio " + coste;

                    if (gold >= coste)
                    {
                        Desbloquear.Visibility = Visibility.Visible;
                        desbloquear.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        Desbloquear.Visibility = Visibility.Collapsed;
                        desbloquear.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        // Funciones de botones atras
        #region 
        // Abandonar la partida
        private void LeaveBattle(object sender, RoutedEventArgs e)
        {
            control = false; bool aux = false;
            if (random.Next(10) % 2 == 0) aux = true;
            Info info = new Info(time, tropeCounter, tropesEliminated, totalGold, aux);
            Frame.Navigate(typeof(EndPage), info);
        }

        // Funcion que oculta el popUp de tropa
        private void SecondBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            BichinClickao = null;

            Mejorar.Visibility = Visibility.Collapsed;
            mejorar.Visibility = Visibility.Collapsed;
            Desbloquear.Visibility = Visibility.Collapsed;
            desbloquear.Visibility = Visibility.Collapsed;
            PopUp.Visibility = Visibility.Collapsed;
        }

        // Funcion para quitar el menu de mejoras
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
        #endregion

        // Funciones de los botones auxiliares
        #region

        // Funcion que aumenta el numero de tropas
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

        // Funcion q abre el menu de mejoras
        private void Open_UpgradesMenu(object sender, RoutedEventArgs e)
        {
            UpgradesMenu.Visibility = Visibility.Visible;
        }
        #endregion

        // Funciones de Drag
        #region
        // Funcion para cuando queremos arrastrar una tropa
        private void OnItemDrag(object sender, DragItemsStartingEventArgs e)
        {
            // SE SOBREESCRIBE POR
            VMBichin Item = e.Items[0] as VMBichin;
            if (Item.Seleccionable)
            {
                e.Data.SetText(Item.Name);
                e.Data.RequestedOperation = DataPackageOperation.Copy;
                BichinArrastrao = Item;
            }
            else
            {
                e.Data.RequestedOperation = DataPackageOperation.None;
            }
        }

        // Metodo para cuando estas arrastrando una tropa sobre el tablero
        private void DraggingOverCanvas(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }
        #endregion

        // Funciones para el drop
        #region
        // Funcion para cuando soltamos una tropa en el tablero
        private void DropOnCanvas(object sender, DragEventArgs e)
        {
            Windows.Foundation.Point i = e.GetPosition(MiTablero);
            difX = MiTablero.ActualWidth / NUM_COLS;
            difY = MiTablero.ActualHeight / NUM_ROWS;

            if (e.Data.RequestedOperation == DataPackageOperation.Copy &&
                i.X < MiTablero.ActualWidth / 2 - difX * 1.2)
            {
                int mulX = Convert.ToInt32(i.X / difX);
                int mulY = Convert.ToInt32(i.Y / difY);
                int posX = Convert.ToInt32(mulX * difX + MiTablero.ActualOffset.X);
                int posY = Convert.ToInt32(mulY * difY + MiTablero.ActualOffset.Y);

                if (IsDroppable(mulX, mulY))
                {
                    gold -= 3 + BichinArrastrao.Nivel * 2;
                    Gold.Text = gold.ToString() + " Oro";
                    CVBichin newBicho = new CVBichin(BichinArrastrao, posX + 2, posY + 2, difX - 4, difY - 4);
                    newBicho.UseSystemFocusVisuals = true;
                    newBicho.KeyDown += ItemKeyDown;
                    MiCanvas.Children.Add(newBicho);
                    BichinesCanvas.Add(newBicho);
                    BichinArrastrao = null;
                    tropeCounter++;
                }
            }
        }

        //Compruebo si puedo droppear la tropa
        private bool IsDroppable(int mulX, int mulY)
        {
            if (gold >= 3 + BichinArrastrao.Nivel * 2)
            {
                if (mulY == NUM_ROWS) return false;
                if (mulX >= 7 && mulX < 10 && mulY >= 9 && mulY < NUM_ROWS)
                {
                    if (BichinArrastrao.Group == "Mineros")
                    {
                        goldPS += 1;
                        GoldPerSecond.Text = goldPS.ToString() + " Oro/s";
                        return true;
                    }
                    else return false;
                }
                else if (mulY >= 3 && mulY < 8 && mulX >= 0 && mulX < 5) return false;

                if (BichinArrastrao.Group != "Mineros") return true;
            }

            return false;
        }
        #endregion

        // Cosas del menu de mejoras
        #region 
        // Metodo para mejorar Tropas en el menu de mejoras
        private void upgradeTrope(object sender, RoutedEventArgs e)
        {
            // Resto oro en funcion del nivel
            gold -= BichinClickao.Nivel * 10;
            Gold.Text = gold.ToString() + " Oro";

            // Recorro los elementos del GridView buscando la tropa que quiero mejorar
            for (int i = 0; i < BotonesBichines.Count; i++)
            {
                if (BotonesBichines[i].Name == BichinClickao.Name)
                {
                    BotonesBichines[i].Nivel++;
                    PanelBotones.ItemsSource = null;
                    PanelBotones.ItemsSource = BotonesBichines;
                }
            }
            // Actualizo el boton de mejorar si tengo dinero para mejorarlo otra vez
            if (BichinClickao.Nivel + 1 * 10 > gold) { Mejorar.Visibility = Visibility.Collapsed; mejorar.Visibility = Visibility.Visible; }
        }

        // Metodo para desbloquear Tropas en el menu de mejoras
        private void unlockTrope(object sender, RoutedEventArgs e)
        {
            // Reduzco el dinero del jugador
            gold -= 10;
            Gold.Text = gold.ToString() + " Oro";

            bool found = false;
            int i = 0;
            // Busco mi tropa
            while (!found && i < BotonesBichines.Count)
            {
                // Si la encuentro
                if (BotonesBichines[i].Name == BichinClickao.Name)
                {
                    // Cambio sus elementos (dejo de mostrarlo como bloqueado)
                    BotonesBichines[i].Sombra = Visibility.Collapsed;
                    BotonesBichines[i].Mejorable = Visibility.Visible;
                    BotonesBichines[i].Bloqueado = Visibility.Collapsed;
                    BotonesBichines[i].Disponible = true;

                    // Busco la misma tropa pero en el GridView de arrastre
                    bool found2 = false;
                    int j = 0;
                    while (!found2 && j < ListaBichines.Count)
                    {
                        // Si la encuentro
                        if (ListaBichines[j].Name == BichinClickao.Name)
                        {
                            // Dependiendo del grupo que sea modifico en un GridView u otro (actualizo el itemsSource)
                            switch (ListaBichines[j].Group)
                            {
                                case "Defensas":
                                    {
                                        BichinesDefensa[ListaBichines[j].Id].Desbloqueado = Visibility.Collapsed;
                                        BichinesDefensa[ListaBichines[j].Id].Seleccionable = true;
                                        Defensas.ItemsSource = null;
                                        Defensas.ItemsSource = BichinesDefensa;
                                    }
                                    break;
                                case "Mineros":
                                    {
                                        BichinesMineros[ListaBichines[j].Id].Desbloqueado = Visibility.Collapsed;
                                        BichinesMineros[ListaBichines[j].Id].Seleccionable = true;
                                        Mineros.ItemsSource = null;
                                        Mineros.ItemsSource = BichinesMineros;
                                    }
                                    break;
                                case "Atacantes":
                                    {
                                        BichinesAtaque[ListaBichines[j].Id].Desbloqueado = Visibility.Collapsed;
                                        BichinesAtaque[ListaBichines[j].Id].Seleccionable = true;
                                        Atacantes.ItemsSource = null;
                                        Atacantes.ItemsSource = BichinesAtaque;
                                    }
                                    break;
                            }

                            found2 = true;
                        }
                        else j++;
                    }
                    // Actualizo el itemsSource de mi GridView
                    PanelBotones.ItemsSource = null;
                    PanelBotones.ItemsSource = BotonesBichines;

                    found = true;
                }

                else i++;
            }
        }

        // Funcion para desplegar el popUp de mejorar al clicar sobre una tropa en el Menu de Mejoras
        private void ClickOnTropeButton(object sender, ItemClickEventArgs e)
        {
            VMBButton b = e.ClickedItem as VMBButton;
            BichinClickao = b;
            isActive = Visibility.Visible;
            PopUp.Visibility = isActive;
        }
        #endregion

        // Metodo para cuando tecleo teniendo una tropa sobre el tablero
        private void ItemKeyDown(object sender, KeyRoutedEventArgs e)
        {
            CVBichin b = sender as CVBichin;
            if (b?.Parent == MiCanvas)
            {
                CompositeTransform ct = b.RenderTransform as CompositeTransform;
                switch (e.Key)
                {
                    case VirtualKey.Delete:
                    case VirtualKey.GamepadX:
                        BichinesCanvas.Remove(b);
                        (b.Parent as Canvas).Children.Remove(b);
                        tropesEliminated++;
                        break;
                    case VirtualKey.W:
                    case VirtualKey.GamepadRightThumbstickUp:
                        moveTrope(ref b, ct, Direction.UP);
                        e.Handled = true;
                        break;

                    case VirtualKey.S:
                    case VirtualKey.GamepadRightThumbstickDown:
                        moveTrope(ref b, ct, Direction.DOWN); 
                        e.Handled = true;
                        break;

                    case VirtualKey.A:
                    case VirtualKey.GamepadRightThumbstickLeft:
                        moveTrope(ref b, ct, Direction.LEFT);
                        e.Handled = true;
                        break;

                    case VirtualKey.D:
                    case VirtualKey.GamepadRightThumbstickRight:
                        moveTrope(ref b, ct, Direction.RIGHT);
                        e.Handled = true;
                        break;

                    case VirtualKey.GamepadLeftTrigger:
                    case VirtualKey.GamepadRightTrigger:
                    case VirtualKey.GamepadLeftShoulder:
                    case VirtualKey.GamepadRightShoulder:
                    case VirtualKey.GamepadLeftThumbstickUp:
                    case VirtualKey.GamepadLeftThumbstickDown:
                    case VirtualKey.GamepadLeftThumbstickRight:
                    case VirtualKey.GamepadLeftThumbstickLeft:
                        e.Handled = true;
                        break;
                }
            }
            
        }

        //Mover tropa
        #region
        private void moveTrope(ref CVBichin cV, CompositeTransform tr, Direction dir)
        {
            if (cV.Group != "Mineros")
            {
                switch (dir)
                {
                    case Direction.UP:
                        if (CanMoveUp(tr.TranslateX, tr.TranslateY))
                        {
                            tr.TranslateY -= difY;
                            cV.RenderTransform = tr;
                        }
                        break;

                    case Direction.DOWN:
                        if (CanMoveDown(tr.TranslateX, tr.TranslateY))
                        {
                            tr.TranslateY += difY;
                            cV.RenderTransform = tr;
                        }
                        break;

                    case Direction.LEFT:
                        if (CanMoveLeft(tr.TranslateX, tr.TranslateY))
                        {
                            tr.TranslateX -= difX;
                            cV.RenderTransform = tr;
                        }
                        break;

                    case Direction.RIGHT:
                        if (CanMoveRight(tr.TranslateX, tr.TranslateY))
                        {
                            tr.TranslateX += difX;
                            cV.RenderTransform = tr;
                        }
                        break;
                }
            }
        }

        // Comprobaciones de si podemos mover la tropa
        private bool CanMoveLeft(double posX, double posY)
        {
            if (posX < MiTablero.ActualOffset.X + difX) return false;

            else if (posY >= MiTablero.ActualOffset.Y + difY * 8 &&
                posX > MiTablero.ActualOffset.X + difX * 10 &&
                posX < MiTablero.ActualOffset.X + difX * 11) return false;

            else if (posY > MiTablero.ActualOffset.Y + difY * 3 &&
                posY < MiTablero.ActualOffset.Y + difY * 8 &&
                posX > MiTablero.ActualOffset.X + difX * 5 &&
                posX < MiTablero.ActualOffset.X + difX * 6) return false;

            return true;
        }

        private bool CanMoveRight(double posX, double posY)
        {
            if (posX < MiTablero.ActualOffset.X + difX * 12 && 
                posX > MiTablero.ActualOffset.X + difX * 11) return false;

            else if (posX > MiTablero.ActualOffset.X + difX * 6 &&
                posX < MiTablero.ActualOffset.X + difX * 7 &&
                posY > MiTablero.ActualOffset.Y + difY * 8) return false;

            return true;
        }

        private bool CanMoveUp(double posX, double posY)
        {
            if (posY < MiTablero.ActualOffset.Y + difY) return false;

            else if (posY > MiTablero.ActualOffset.Y + difY * 8 &&
                posY < MiTablero.ActualOffset.Y + difY * 9 &&
                posX < MiTablero.ActualOffset.X + difX * 5) return false;

            return true;
        }

        private bool CanMoveDown(double posX, double posY)
        {
            if (posY < MiTablero.ActualOffset.Y + difY * 10 && 
                posY > MiTablero.ActualOffset.Y + difY * 9) return false;

            else if (posY > MiTablero.ActualOffset.Y + difY * 2 &&
                posY < MiTablero.ActualOffset.Y + difY * 3 &&
                posX < MiTablero.ActualOffset.X + difY * 5) return false;

            else if (posY > MiTablero.ActualOffset.Y + difY * 7 &&
                posY < MiTablero.ActualOffset.Y + difY * 8 &&
                posX > MiTablero.ActualOffset.X + difX * 7 &&
                posX < MiTablero.ActualOffset.X + difX * 10) return false;

            return true;
        }
        #endregion        
    }
}
