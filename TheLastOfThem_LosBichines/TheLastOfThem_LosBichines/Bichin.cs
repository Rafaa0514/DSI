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
        public string Type { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
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
                Type = "Defensa / Luchador",
                Description = "Lanza pedradas a los enemigos\nutilizando sus fornidas piernas.",
                Group = "Defensas",
                Imagen = "Assets\\Images\\Bichines\\Bichin-PateaPiedras.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 1,
                Name = "Escudero",
                Type = "Defensa / Luchador",
                Description = "Mantiene los enemigos a raya y les\ndevuelve sus ataques.",
                Group = "Defensas",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Escudero.png",
                Desbloqueado = Visibility.Collapsed,
                Nivel = 1,
                Seleccionable = true,
            },
            new Bichin()
            {
                Id = 2,
                Name = "Loco",
                Type = "Defensa / Luchador",
                Description = "Gira con su espada sin control para\nevitar que los enemigos penetren en\nlas defensas.",
                Group = "Defensas",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Loco.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 3,
                Name = "Ballestero",
                Type = "Defensa / Constructor",
                Description = "Construye una ballesta que\ndisparará a los enemigos desde una\nlarga distancia.",
                Group = "Defensas",
                Imagen = "Assets\\Images\\Bichines\\Bichin-ConstructorBallesta.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 4,
                Name = "MrMuro",
                Type = "Defensa / Constructor",
                Description = "Alza un muro delante de él que\nevitará que los enemigos pasen por\nahí.",
                Group = "Defensas",
                Imagen = "Assets\\Images\\Bichines\\Bichin-ConstructorPared.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 5,
                Name = "Tramposo",
                Type = "Defensa / Constructor",
                Description = "Coloca una trampa en una casilla.\nLos enemigos no sobrevivirán al caer\nen ella.",
                Group = "Defensas",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Trampero.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 0,
                Name = "MineroPequeño",
                Type = "Minero",
                Description = "Mina a gran velocidad aumentando\nconsiderablemente la producción de\noro pero es muy frágil ante ataques\nenemigos.",
                Group = "Mineros",
                Imagen = "Assets\\Images\\Bichines\\Bichin-MineroPequeño.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 1,
                Name = "Minero",
                Type = "Minero",
                Description = "Es capaz de extraer oro de la mina\naumentando su producción.",
                Group = "Mineros",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Minero.png",
                Desbloqueado = Visibility.Collapsed,
                Nivel = 1,
                Seleccionable = true,
            },
            new Bichin()
            {
                Id = 2,
                Name = "MineroGrande",
                Type = "Minero",
                Description = "Excava en la mina de forma lenta\npero segura, no es el más rápido\npero es muy confiable y resistente.",
                Group = "Mineros",
                Imagen = "Assets\\Images\\Bichines\\Bichin-MineroGrande.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 0,
                Name = "Arquero",
                Type = "Ataque / Luchador",
                Description = "Lanza flechas a los enemigos desde\nla distancia para avanzar con el\nataque.",
                Group = "Atacantes",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Arquero.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 1,
                Name = "Espadachin",
                Type = "Ataque / Luchador",
                Description = "Combate cuerpo a cuerpo en el\ncampo enemigo en favor del avance.",
                Group = "Atacantes",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Espadachin.png",
                Desbloqueado = Visibility.Collapsed,
                Nivel = 1,
                Seleccionable = true,
            },
            new Bichin()
            {
                Id = 2,
                Name = "GymBro",
                Type = "Ataque / Luchador",
                Description = "Gran combatiente que se deja la piel\nen contra el rival en su propio\nterreno de juego.",
                Group = "Atacantes",
                Imagen = "Assets\\Images\\Bichines\\Bichin-GymBro.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 3,
                Name = "Bombardero",
                Type = "Ataque / Asedio",
                Description = "Pirómano que lanzará bombas para\nderrumbar las construcciones\nenemigas.",
                Group = "Atacantes",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Bombardero.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 4,
                Name = "Ariete",
                Type = "Ataque / Asedio",
                Description = "Con gran fortaleza carga con un\ntronco afilado que usará para atacar\na las edificaciones rivales.",
                Group = "Atacantes",
                Imagen = "Assets\\Images\\Bichines\\Bichin-Ariete.png",
                Desbloqueado = Visibility.Visible,
                Nivel = 1,
                Seleccionable = false,
            },
            new Bichin()
            {
                Id = 5,
                Name = "Gorila",
                Type = "Ataque / Asedio",
                Description = "Gigante capaz de resistir los\nmayores golpes y derribar las\nmayores construcciones.",
                Group = "Atacantes",
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

        public static Bichin GetBichinByPath(string path)
        {
            for (int i = 0; i < Bichines.Count; i++)
            {
                if (Bichines[i].Imagen == path) return Bichines[i];
            }
            return Bichines[0];
        }
    }
}
