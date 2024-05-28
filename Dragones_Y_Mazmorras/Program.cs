using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Dragones_Y_Mazmorras
{
    class Program
    {
        CampoBatalla campo_batalla;
        public void Iniciar()
        {
            campo_batalla = new CampoBatalla(120, 30, ConsoleColor.Cyan, new Point(20, 5), new Point(70, 25));
            campo_batalla.DibujarMarco();
        }
        /*
        private static void MostrarMagos(List<Personatge> personatges)
        {
            string descripcion;
            foreach (var mago in personatges.OfType<Mag>())
            {
                descripcion = mago.ToString();

                Console.WriteLine(descripcion);
            }
        }
        private static void MostrarGuerreros(List<Personatge> personatges)
        {
            string descripcion;
            foreach (var guerrer in personatges.OfType<Guerrer>())
            {
                descripcion = guerrer.ToString();

                Console.WriteLine(descripcion);
            }
        }
        private static void MostrarNoClase(List<Personatge> personatges)
        {
            string descripcion;
            foreach (var personaje in personatges.OfType<Personatge>())
            {
                if (!(personaje is Mag || personaje is Guerrer))
                {
                    descripcion = personaje.ToString();

                    Console.WriteLine(descripcion);
                }
            }
        }
        
        private static void MostrarPartidaPersonatges(Partida partida, List<Personatge> personatges)
        {
            partida.MostrarPartida();
            MostrarNoClase(personatges);
            MostrarMagos(personatges);
            MostrarGuerreros(personatges);
            Console.WriteLine();
        }
        private static void MostrarPartidas(List<Partida> partidas)
        {
            partidas[0].Nom = "Primer Asalto Acabado";
            for (int i = 0; i < partidas.Count; i++)
            {
                MostrarPartidaPersonatges(partidas[i], partidas[i].Personatges);
            }
        }
        */
        static void Main(string[] args)
        {
            string descripcion; /*,opcion;*/
            int poss_in, cont_a, cont_r, cont_e, ataque, defensa, ataque_verdadero;
            bool enemigos_vivos, aliados_vivos;
            //Partida new_partida;
            //JocRol tipo_juego;
            Program programa = new Program();
            Guerrer guerrero_a, guerrero_e, guerrero;
            List<Personatge> personatges;
            //List<Partida> partidas;
            Dictionary<string, string> tipos_armas = new Dictionary<string, string>();
            Dictionary<string, Guerrer> guerreros = new Dictionary<string, Guerrer>();
            Queue<Guerrer> aliados_r, enemigos_r;
            Queue<Guerrer> aliados_a, enemigos_a;
            List<Point> posiciones_reserva_A, posiciones_batalla_A, posiciones_batalla_E, posiciones_reserva_E;

            aliados_r = new Queue<Guerrer>();
            aliados_a = new Queue<Guerrer>();
            enemigos_r = new Queue<Guerrer>();
            enemigos_a = new Queue<Guerrer>();

            posiciones_reserva_A = new List<Point>();
            posiciones_batalla_A = new List<Point>();
            posiciones_batalla_E = new List<Point>();
            posiciones_reserva_E = new List<Point>();

            poss_in = 2;
            for (int i = 0; i < 12; i++) //guardamos las posiciones para los guerreros activos y en reserva
            {
                posiciones_reserva_A.Add(new Point(5, poss_in));
                posiciones_reserva_E.Add(new Point(72, poss_in));
                poss_in += 3;

                if (i < 5)
                {
                    posiciones_batalla_A.Add(new Point(25, poss_in + 3));
                    posiciones_batalla_E.Add(new Point(50, poss_in + 3));
                }
            }

            programa.Iniciar(); //creamos el campo de batalla y lo dibujamos

            /*
            opcion = Funciones.MostrarMenu();
            Console.WriteLine();

            partidas = new List<Partida>();

            tipo_juego = new JocRol();
            tipo_juego.Nom = "Dungeons & Dragon";
            tipo_juego.MaxJugadors = 10;

            new_partida = new Partida("Primer Asalto", tipo_juego);
            partidas.Add(new_partida);

            new_partida.MostrarPartida();
            Console.WriteLine();

            new_partida.IniciarPartida();
            new_partida.Personatges = personatges;
            MostrarPartidas(partidas);
            */

            personatges = new List<Personatge>();

            guerreros = Funciones.CrearGuerrerArxiu(guerreros, personatges, tipos_armas);
            Funciones.AsignarBandos(guerreros, aliados_r, enemigos_r, aliados_a, enemigos_a); //asignamos los bandos de cada guerrero (mitad y mitad)

            cont_a = 0;
            cont_e = 0;
            aliados_vivos = aliados_r.Count > 0 || aliados_a.Count > 0;
            enemigos_vivos = enemigos_r.Count > 0 || enemigos_a.Count > 0;
            while (aliados_vivos && enemigos_vivos) //el bucle termina cuando no quedan guerrueros en alguno de los dos bandos
            {
                cont_r = 0;
                foreach (Guerrer guerreros1 in aliados_r) //dibujamos a todos los guerreros en reserva
                {
                    cont_r += 1;
                    guerreros1.Posicion = posiciones_reserva_A[cont_r];
                    guerreros1.Dibujar();
                }

                cont_r = 0;
                foreach (Guerrer guerreros1 in enemigos_r)
                {
                    cont_r += 1;
                    guerreros1.Posicion = posiciones_reserva_E[cont_r];
                    guerreros1.Dibujar();
                }

                if (cont_e < posiciones_batalla_E.Count) 
                {
                    if (enemigos_a.Count > 0 || enemigos_r.Count > 0)
                    {
                        if (enemigos_a.Count > 0)
                        {
                            guerrero_e = enemigos_a.Peek();
                        }
                        else //asignamos un guerrero para poder hacer la comprobacion en el siguiente if
                        {
                            guerrero_e = enemigos_r.Peek();
                        }

                        if (enemigos_r.Count > 0 && (guerrero_e.PuntsVida < 50 || enemigos_a.Count == 0)) //si el activo actual tiene menos de 50pv agregamos un nuevo activo a la fila, lo mismo si no hay activos
                        {
                            guerrero = enemigos_r.Peek();
                            enemigos_r.Dequeue();
                            enemigos_a.Enqueue(guerrero);
                            guerrero.Posicion = posiciones_batalla_E[cont_e];

                            cont_e += 1;
                        }
                    }
                }

                if (cont_a < posiciones_batalla_A.Count)
                {
                    if (aliados_a.Count > 0 || aliados_r.Count > 0)
                    {
                        if (aliados_a.Count > 0)
                        {
                            guerrero_a = aliados_a.Peek();
                        }
                        else
                        {
                            guerrero_a = aliados_r.Peek();
                        }

                        if (aliados_r.Count > 0 && (guerrero_a.PuntsVida < 50 || aliados_a.Count == 0))
                        {
                            guerrero = aliados_r.Peek();
                            aliados_r.Dequeue();
                            aliados_a.Enqueue(guerrero);
                            guerrero.Posicion = posiciones_batalla_A[cont_a];

                            cont_a += 1;
                        }
                    }
                }

                Thread.Sleep(500);
                Console.Clear();
                programa.Iniciar();

                cont_r = 0;
                foreach (Guerrer guerreros1 in aliados_a)//dibujamos a los guerreros activos
                {
                    guerreros1.Posicion = posiciones_batalla_A[cont_r];
                    guerreros1.Dibujar();
                    cont_r += 1;

                    guerreros1.AumentoDeVida(); //aprobechamos el bucle para subirle los 10pv a los activos
                }

                cont_r = 0;
                foreach (Guerrer guerreros1 in enemigos_a)
                {
                    guerreros1.Posicion = posiciones_batalla_E[cont_r];
                    guerreros1.Dibujar();
                    cont_r += 1;

                    guerreros1.AumentoDeVida();
                }

                aliados_vivos = aliados_r.Count > 0 || aliados_a.Count > 0;
                enemigos_vivos = enemigos_r.Count > 0 || enemigos_a.Count > 0;

                if (enemigos_a.Count > 0 && aliados_a.Count > 0) //operaciones para la batalla
                {
                    guerrero_e = enemigos_a.Peek();
                    guerrero_a = aliados_a.Peek();

                    ataque = guerrero_e.ObtenirPuntsAtac();
                    defensa = guerrero_a.ObtenirPuntsDefensa();
                    ataque_verdadero = ataque - defensa;

                    if (ataque_verdadero > 0)
                    {
                        guerrero_a.PuntsVida -= ataque_verdadero;
                    }
                    else //en caso de que el daño sea menor a 1, se anulará el efecto del escudo
                    {
                        guerrero_a.PuntsVida -= ataque;
                    }
                    
                    ataque = guerrero_a.ObtenirPuntsAtac();
                    defensa = guerrero_e.ObtenirPuntsDefensa();
                    ataque_verdadero = ataque - defensa;
                    
                    if (ataque_verdadero > 0)
                    {
                        guerrero_e.PuntsVida -= ataque_verdadero;
                    }
                    else 
                    {
                        guerrero_e.PuntsVida -= ataque;
                    }

                    if (guerrero_a.PuntsVida <= 0)
                    {
                        aliados_a.Dequeue();
                        cont_a -= 1;

                        guerrero_a.PuntsVida = 0;
                    }
                    if (guerrero_e.PuntsVida <= 0)
                    {
                        enemigos_a.Dequeue();
                        cont_e -= 1;

                        guerrero_e.PuntsVida = 0;
                    }

                    Console.ForegroundColor = ConsoleColor.Green; //mostramos los datos de los guerreros
                    Console.SetCursorPosition(21, 4);
                    descripcion = guerrero_a.ToString();
                    Console.Write(descripcion);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(46, 4);
                    descripcion = guerrero_e.ToString();
                    Console.Write(descripcion);
                }
            }

            poss_in = 0;
            while (true) //bucle infinito que muestra un mensaje anunciando al bando ganador
            {
                poss_in++;
                Console.SetCursorPosition(poss_in, 2);
                Thread.Sleep(100);

                if (enemigos_a.Count == 0)
                {
                    Console.Write("EL BANDO ALDIADO GANA");
                }
                else if (aliados_a.Count == 0)
                {
                    Console.Write("EL BANDO ENEMIGO GANA");
                }
                Console.SetCursorPosition(poss_in - 1, 2);
                Console.Write(" ");
                
                if (poss_in > 115)
                {
                    poss_in = 0;

                    Console.SetCursorPosition(115, 2);
                    Console.Write("                       ");
                }
            }

            /*while (opcion != "S") {
                Console.WriteLine();

                if (opcion.Equals("A"))
                {
                    tipos_armas = Funciones.CompCodiTipoArma(tipos_armas);
                }
                else if (opcion.Equals("B"))        
                {
                    Funciones.MostrarTipoArmas(tipos_armas);
                }
                else if(opcion.Equals("C"))
                {
                    tipos_armas = Funciones.ModificarNomArma(tipos_armas);
                }
                else if (opcion.Equals("D"))
                {
                    tipos_armas = Funciones.EliminarArma(tipos_armas, guerreros);
                }
                else if (opcion.Equals("E"))
                {
                    tipos_armas = Funciones.CrearGuerrer(tipos_armas, guerreros, personatges);
                }
                else if (opcion.Equals("F"))
                {
                    guerreros = Funciones.CrearGuerrerArxiu(guerreros, personatges);
                }
                else if (opcion.Equals("G"))
                {
                    Funciones.MostrarArmaGuerreros(guerreros);
                }

                opcion = Funciones.MostrarMenu();
            }*/

        }
    }
}
