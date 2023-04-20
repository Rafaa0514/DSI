using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TheLastOfThem_LosBichines
{
    public class VMBichin : Bichin
    {
        public Image img;
        public Windows.UI.Xaml.Shapes.Rectangle rect;
        public VMBichin(Bichin b)
        {
            rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            rect.Width = rect.Height = 65;

            Id = b.Id;
            Name = b.Name;
            Imagen = b.Imagen;
            Desbloqueado = b.Desbloqueado;
            Seleccionable = b.Seleccionable;
            Nivel = b.Nivel;

            img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + b.Imagen;
            img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
            img.Width = 65;
        }
    }

    public class CVBichin : ContentControl
    {
        public string Nombre;
        public int id;
        public Grid g;
        public Image img;
        public Windows.UI.Xaml.Shapes.Rectangle rect;
        public CompositeTransform Transformacion;

        public CVBichin(VMBichin b, double x, double y)
        {
            g = new Grid();
            g.Name = Nombre;

            id = b.Id;
            Name = b.Name;

            img = new Image();
            img.Source = b.img.Source;
            img.Width = b.img.Width;

            rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
            rect.Width = rect.Height = 65;

            g.Children.Add(rect);
            g.Children.Add(img);

            this.Content = g;

            Transformacion = new CompositeTransform();
            Transformacion.TranslateX = x;
            Transformacion.TranslateY = y;
            this.RenderTransform = Transformacion;
        }
    }
}
