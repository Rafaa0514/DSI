using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI.Xaml;

namespace TheLastOfThem_LosBichines
{

    public struct DeltaTeclado { public double X, Y, A, Z; }
    public class Controller
    {
        //Para manejar los mandos
        private readonly object myLock = new object();
        private List<Gamepad> myGamepads = new List<Gamepad>();
        public Gamepad mainGamepad = null;
        //Lectura y escritura de los mandos
        public GamepadReading reading, prereading;
        private GamepadVibration vibration;


        //Cambios por teclado

        DeltaTeclado delta;

        // Teclado por defecto
        VirtualKey Izquierda = VirtualKey.A;
        VirtualKey Izquierda1 = VirtualKey.GamepadDPadLeft;
        VirtualKey Derecha = VirtualKey.D;
        VirtualKey Derecha1 = VirtualKey.GamepadDPadRight;
        VirtualKey Arriba = VirtualKey.W;
        VirtualKey Arriba1 = VirtualKey.GamepadDPadUp;
        VirtualKey Abajo = VirtualKey.S;
        VirtualKey Abajo1 = VirtualKey.GamepadDPadDown;
        VirtualKey GirDer = VirtualKey.E;
        VirtualKey GirDer1 = VirtualKey.GamepadRightTrigger;
        VirtualKey GirIzq = VirtualKey.Q;
        VirtualKey GirIzq1 = VirtualKey.GamepadLeftTrigger;
        VirtualKey ZoomMas = VirtualKey.C;
        VirtualKey ZoomMas1 = VirtualKey.GamepadRightShoulder;
        VirtualKey ZoomMen = VirtualKey.Z;
        VirtualKey ZoomMen1 = VirtualKey.GamepadLeftShoulder;


        //Varibles Gesto
        public bool Gesto = false;
        float TiempoGesto = 0;
        int EstadoGesto = 0;

        private DispatcherTimer _timer;

        MainPage mainPage = null;

        public Controller(MainPage mP)
        {
            mainPage = mP;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.01) };

            _timer.Tick += (sender, o) => {

                if (mainGamepad != null)
                {
                    LeeMando();                      //Lee GamePAd
                    DetectaGestosMando();            //Detecta Gestos del Mando
                    ZMMando();                       //ZonaMuerta JoyStick y Triggers
                    //mainPage.AplicaGesto();                    //Aplica Gestos en IU
                    //mainPage.AplicaTecladoContinuo();          //Aplica Teclado continuo en IU
                    //mainPage.ActualizaIU();                    //Aplica cambios en IU y VM
                    FeedBack();
                }
            };

            _timer.Start();

            Gamepad.GamepadAdded += (object sender, Gamepad e) =>
            {
                // Check if the just-added gamepad is already in myGamepads;
                // if it isn't, add it.
                lock (myLock)
                {
                    bool gamepadInList = myGamepads.Contains(e);
                    if (!gamepadInList)
                    {
                        myGamepads.Add(e);
                        mainGamepad = myGamepads[0];
                    }
                }
            };

            Gamepad.GamepadRemoved += (object sender, Gamepad e) =>
            {
                lock (myLock)
                {
                    int indexRemoved = myGamepads.IndexOf(e);
                    if (indexRemoved > -1)
                    {
                        if (mainGamepad == myGamepads[indexRemoved])
                        {
                            mainGamepad = null;
                        }
                        myGamepads.RemoveAt(indexRemoved);
                    }
                }
            };
        }

        public void LeeMando()
        {
            prereading = reading;
            reading = mainGamepad.GetCurrentReading();
        }
        public void DetectaGestosMando()
        {

        }
        public void ZMMando()
        {
            if (reading.RightThumbstickX < -0.1) reading.RightThumbstickX -= -0.1;
            else if (reading.RightThumbstickX > 0.1) reading.RightThumbstickX -= 0.1;
            else reading.RightThumbstickX = 0;

            if (reading.RightThumbstickY < -0.1) reading.RightThumbstickY -= -0.1;
            else if (reading.RightThumbstickY > 0.1) reading.RightThumbstickY -= 0.1;
            else reading.RightThumbstickY = 0;
        }
        public void FeedBack()
        {
            vibration.RightMotor = Math.Max(Math.Abs(reading.RightThumbstickX), Math.Abs(reading.RightThumbstickY));

            vibration.LeftMotor = Math.Abs(Math.Max(reading.RightTrigger, reading.LeftTrigger));
            mainGamepad.Vibration = vibration;
        }
    }
}
