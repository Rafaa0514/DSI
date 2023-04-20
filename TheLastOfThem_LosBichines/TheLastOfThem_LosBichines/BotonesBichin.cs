using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TheLastOfThem_LosBichines
{
    public class BotonBichin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Imagen { get; set; }
        public string BotonBloqueado { get; set; }
        public string BotonMejorar { get; set; }
        public Visibility Sombra { get; set; }
        public Visibility Bloqueado { get; set; }
        public Visibility Mejorable { get; set; }
        public int Nivel { get; set; }

        public BotonBichin() { }
    }

    public class ButtonModel
    {
        public static List<BotonBichin> Elementos = new List<BotonBichin>()
        {
            new BotonBichin()
            {
                Id = 0,
                Name = "PateaPiedras",
                Imagen = "Assets\\Images\\Bichines\\Bichin-PateaPiedras.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 1,
                Name = "Escudero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Escudero.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Collapsed,
                Bloqueado = Visibility.Collapsed,
                Mejorable = Visibility.Visible,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 2,
                Name = "Loco",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Loco.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 3,
                Name = "ConstructorBallesta",
                Imagen = "Assets\\Images\\Bichines\\Bichin-ConstructorBallesta.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 4,
                Name = "ConstructorPared",
                Imagen = "Assets\\Images\\Bichines\\Bichin-ConstructorPared.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 5,
                Name = "Trampero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Trampero.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 6,
                Name = "MineroPequeño",
                Imagen = "Assets\\Images\\Bichines\\Bichin-MineroPequeño.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 7,
                Name = "Minero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Minero.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Collapsed,
                Bloqueado = Visibility.Collapsed,
                Mejorable = Visibility.Visible,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 8,
                Name = "MineroGrande",
                Imagen = "Assets\\Images\\Bichines\\Bichin-MineroGrande.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 9,
                Name = "Arquero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Arquero.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 10,
                Name = "Espadachin",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Espadachin.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Collapsed,
                Bloqueado = Visibility.Collapsed,
                Mejorable = Visibility.Visible,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 11,
                Name = "Gymbro",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Gymbro.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 12,
                Name = "Bombardero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Bombardero.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 13,
                Name = "Ariete",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Ariete.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
            new BotonBichin()
            {
                Id = 14,
                Name = "Gorila",
                Imagen = "Assets\\Images\\Bichines\\Bichin Gorila.png",
                BotonBloqueado = "Assets\\Images\\blockedSymbol.png",
                BotonMejorar = "Assets\\Images\\upgradeSymbol.png",
                Sombra = Visibility.Visible,
                Bloqueado = Visibility.Visible,
                Mejorable = Visibility.Collapsed,
                Nivel = 1,
            },
        };

        public static IList<BotonBichin> GetAllButtons()
        {
            return Elementos;
        }


    }

    public class VMBButton : BotonBichin
    {
        public Image img;
        public Windows.UI.Xaml.Shapes.Rectangle rect;
        public VMBButton(BotonBichin b)
        {
            rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            rect.Width = rect.Height = 65;

            Id = b.Id;
            Name = b.Name;
            Imagen = b.Imagen;
            BotonBloqueado = b.BotonBloqueado;
            BotonMejorar = b.BotonMejorar;
            Sombra = b.Sombra;
            Bloqueado = b.Bloqueado;
            Mejorable = b.Mejorable;
            Nivel = b.Nivel;

            img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + b.Imagen;
            img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
            img.Width = 65;
        }
    }
}
