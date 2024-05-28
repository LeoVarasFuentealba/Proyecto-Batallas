using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dragones_Y_Mazmorras
{
    class Guerrer : Personatge
    {
        private List<String> armes;
        public List<String> Armes
        {
            get { return armes; }
            set { List<String> Value = value; }
        }
        private bool escut;
        public bool Escut
        {
            get { return escut; }
            set { escut = value; }
        }
        public Point Posicion { get; set; }
        public bool BandoAliado { get; set; }
        public int VidaInicial { get; set; }
        
        public Guerrer(string nom)
        {
            Nom = nom;
            PuntsVida = 200;
            armes = new List<string>();
        }
        public void AfegirArma(string arma)
        {
            string arma_act;
            bool arma_en_lista;

            arma_en_lista = false;
            if (armes.Count > 0)
            {
                for (int i = 0; i < armes.Count && !arma_en_lista; i++)
                {
                    arma_act = armes[i];
                    if (arma_act == arma)
                    {
                        arma_en_lista = true;
                    }
                }
            }
            
            if (!arma_en_lista)
            {
                armes.Add(arma);
            }
        }

        public override int ObtenirPuntsAtac()
        {
            int punts;

            punts = 10;
            if (armes.Count > 0)
            {
                punts = punts + armes.Count * 20;
            }

            return punts;
        }
        public override int ObtenirPuntsDefensa()
        {
            int punts;

            if (escut)
            {
                punts = 50;
            }
            else
            {
                punts = 20;
            }
            
            return punts;
        }
        public override string ToString()
        {
            string descripcion;
            int puntosAtc, puntosDef;

            puntosAtc = ObtenirPuntsAtac();
            puntosDef = ObtenirPuntsDefensa();

            descripcion = Nom + " (" + PuntsVida + "): A" + Convert.ToString(puntosAtc) + "/D" + Convert.ToString(puntosDef) + " " + Armes.Count;

            return descripcion;
        }
        public void AumentoDeVida()
        {
            if (VidaInicial < PuntsVida+10)
            {
                PuntsVida = VidaInicial;
            }
            else
            {
                PuntsVida += 10;
            }
            
        }
        public void Dibujar()
        {
            int y, x;
            //crear funciones para dibujar las cajas en posiciones decididas a partir de si son aliados o enemigos y si estan en el frente o no

            if (BandoAliado)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }

            x = Posicion.X;
            y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.WriteLine($"═════════════");
            Console.SetCursorPosition(x, y);
            Console.Write("╔");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("║");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("╚");
            Console.SetCursorPosition(x + 13, y);
            Console.Write("╗");
            Console.SetCursorPosition(x + 13, y + 1);
            Console.Write("║");
            Console.SetCursorPosition(x + 13, y + 2);
            Console.Write("╝");
            Console.SetCursorPosition(x + 4, y + 1);
            Console.WriteLine($"{Nom}");
            Console.SetCursorPosition(x + 1, y + 2);
            Console.WriteLine($"════════════");
        }
    }
}
