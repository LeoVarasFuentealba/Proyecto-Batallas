using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Dragones_Y_Mazmorras
{
    internal class CampoBatalla
    {
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor Color { get; set; }
        public Point limiteSuperior { get; set; }
        public Point limiteInferior { get; set; }
        public CampoBatalla(int ancho, int altura, ConsoleColor color,
            Point Limitesuperior, Point Limiteinferior)
        {
            Color = color;
            Ancho = ancho;
            Altura = altura;
            limiteInferior = Limiteinferior;
            limiteSuperior = Limitesuperior;
            Init();
        }
        private void Init()
        {
            Console.SetWindowSize(Ancho, Altura);
            Console.Title = "CAMPO DE BATALLA";
            Console.BackgroundColor = Color;
            Console.Clear();
            Console.CursorVisible = false;
        }
        public void DibujarTiutlos()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(30, 6);
            Console.Write("ALIADOS");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(54, 6);
            Console.Write("ENEMIGOS");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        public void DibujarMarco()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            for (int i = limiteSuperior.Y; i <= limiteInferior.Y; i++)
            {
                Console.SetCursorPosition(limiteSuperior.X, i);
                Console.Write("║");
                Console.SetCursorPosition(limiteInferior.X, i);
                Console.Write("║");
                Console.SetCursorPosition(limiteInferior.X - 25, i);
                Console.Write("║");
            }
            for (int i = limiteSuperior.X; i <= limiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, limiteInferior.Y);
                Console.Write("═");
                Console.SetCursorPosition(i, limiteSuperior.Y);
                Console.Write("═");

                if (i > limiteSuperior.X && i != limiteInferior.X)
                {
                    Console.SetCursorPosition(i, limiteSuperior.Y + 2);
                    Console.Write("═");
                }
            }

            Console.SetCursorPosition(limiteSuperior.X, limiteSuperior.Y);
            Console.Write("╔");
            Console.SetCursorPosition(limiteSuperior.X, limiteInferior.Y);
            Console.Write("╚");
            Console.SetCursorPosition(limiteInferior.X, limiteSuperior.Y);
            Console.Write("╗");
            Console.SetCursorPosition(limiteInferior.X, limiteInferior.Y);
            Console.Write("╝");

            DibujarTiutlos();
        }
    }
}
