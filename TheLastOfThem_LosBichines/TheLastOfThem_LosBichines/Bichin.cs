using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TheLastOfThem_LosBichines
{
    public class Bichin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Imagen { get; set; }
        public Visibility Desbloqueado { get; set; }
        public int Nivel { get; set; }
        public bool Seleccionable { get; set; }

        public Bichin() { }
    }

    public class Model
    {
        public static List<Bichin> Bichines = new List<Bichin>()
        {
            new Bichin()
            {
                Id = 0,
                Name = "PateaPiedras",
                Imagen = "Assets\\Images\\Bichines\\Bichin-PateaPiedras.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 1,
                Name = "Escudero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Escudero.png",
                Desbloqueado = Visibility.Collapsed,
                Nivel = 1,
                Seleccionable = true,
            },
            new Bichin()
            {
                Id = 2,
                Name = "Loco",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Loco.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 3,
                Name = "ConstructorBallesta",
                Imagen = "Assets\\Images\\Bichines\\Bichin-ConstructorBallesta.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 4,
                Name = "ConstructorPared",
                Imagen = "Assets\\Images\\Bichines\\Bichin-ConstructorPared.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 5,
                Name = "Trampero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Trampero.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 6,
                Name = "MineroPequeño",
                Imagen = "Assets\\Images\\Bichines\\Bichin-MineroPequeño.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 7,
                Name = "Minero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Minero.png",
                Desbloqueado = Visibility.Collapsed,
                Nivel = 1,
                Seleccionable = true,
            },
            new Bichin()
            {
                Id = 8,
                Name = "MineroGrande",
                Imagen = "Assets\\Images\\Bichines\\Bichin-MineroGrande.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 9,
                Name = "Arquero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Arquero.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 10,
                Name = "Espadachin",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Espadachin.png",
                Desbloqueado = Visibility.Collapsed,
                Nivel = 1,
                Seleccionable = true,
            },
            new Bichin()
            {
                Id = 11,
                Name = "GymBro",
                Imagen = "Assets\\Images\\Bichines\\Bichin-GymBro.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 12,
                Name = "Bombardero",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Bombardero.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 13,
                Name = "Ariete",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Ariete.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 14,
                Name = "Gorila",
                Imagen = "Assets\\Images\\Bichines\\Bichin Gorila.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            }
        };

        public static IList<Bichin> GetAllBichines()
        {
            return Bichines;
        }

        public static IList<Bichin> GetDefenseBichines()
        {
            List<Bichin> dB = new List<Bichin>();
            for (int i = 0; i < 6; i++)
            {
                dB.Add(Bichines[i]);
            }
            return dB;
        }
        public static IList<Bichin> GetMineBichines()
        {
            List<Bichin> mB = new List<Bichin>();
            for (int i = 6; i < 9; i++)
            {
                mB.Add(Bichines[i]);
            }
            return mB;
        }
        public static IList<Bichin> GetAttackBichines()
        {
            List<Bichin> aB = new List<Bichin>();
            for (int i = 9; i < Bichines.Count; i++)
            {
                aB.Add(Bichines[i]);
            }
            return aB;
        }

        public static Bichin GetBichinById(int id)
        {
            return Bichines[id];
        }

        public static Bichin GetBichinByName(string name)
        {
            for (int i = 0; i < Bichines.Count; i++)
            {
                if (Bichines[i].Name == name) return Bichines[i];
            }
            return Bichines[0];
        }
    }
}
