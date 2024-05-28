using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Dragones_Y_Mazmorras
{
    class Funciones
    {
        public static List<Personatge> GenerarPersonajesPart1(Dictionary<string, string> armas, Dictionary<string, Guerrer> guerreros)
        {
            List<Personatge> personatges;
            Mag mago_sel;
            Guerrer guerrer_sel;
            Personatge personaje;

            personatges = new List<Personatge>();

            personaje = new Personatge(10);
            personaje.Nom = "ROSITA";
            personatges.Add(personaje);

            guerrer_sel = new Guerrer("MARIANO");
            guerreros.Add("MAR", guerrer_sel);
            guerrer_sel.AfegirArma("MAZO");
            armas.Add("MAZ", "MAZO");
            guerrer_sel.AfegirArma("ESPADA");
            armas.Add("ESP", "ESPADA");
            personatges.Add(guerrer_sel);

            guerrer_sel = new Guerrer("FERNANDO");
            guerreros.Add("FER", guerrer_sel);
            guerrer_sel.AfegirArma("DAGA");
            armas.Add("DAG", "DAGA");
            personatges.Add(guerrer_sel);

            mago_sel = new Mag(1000);
            mago_sel.Nom = "GANDALF";
            personatges.Add(mago_sel);

            return personatges;
        }
        public static List<Personatge> GenerarPersonajesPart2()
        {
            List<Personatge> personatges;
            Guerrer guerrer_sel;

            personatges = new List<Personatge>();

            guerrer_sel = new Guerrer("PARCHES");
            guerrer_sel.Armes.Add("ESTOQUE");
            personatges.Add(guerrer_sel);

            return personatges;
        }

        public static string MostrarMenu()
        {
            string opcion;
            bool verf_opcion;

            do {
                Console.WriteLine("A) AFEGIR TIPUS ARMA");
                Console.WriteLine("B) MOSTRAR TIPUS D’ARMES");
                Console.WriteLine("C) MODIFICAR NOM TIPUS D’ARMA");
                Console.WriteLine("D) ELIMINAR TIPUS D’ARMA");
                Console.WriteLine("E) AFEGIR GUERRER");
                Console.WriteLine("F) INSERIR GUERRER DES DE ARXIU");
                Console.WriteLine("G) MOSTRAR ARMES GUERRER");
                Console.WriteLine("S) SALIR DEL PROGRAMA");
                Console.WriteLine("");
                Console.Write("Opcion: ");
                opcion = Console.ReadLine().Trim().ToUpper();

                verf_opcion = opcion.Equals("A") || opcion.Equals("B") || opcion.Equals("C") || opcion.Equals("D") || opcion.Equals("E") || opcion.Equals("F") || opcion.Equals("G") || opcion.Equals("S");
            } while (!verf_opcion);

            return opcion;
        }

        public static Dictionary<string, string> CompCodiTipoArma(Dictionary<string,string> tipos_armas)
        {
            string codigo, nom_arma;
            bool correcte;
            Regex comp_formato = new Regex(@"^[a-zA-Z]{3}$");

            Console.Write("NOMBRE DE LA ARMA: ");
            nom_arma = Console.ReadLine().Trim().ToUpper();

            Console.Write("CODIGO DE LA ARMA: ");
            codigo = Console.ReadLine().Trim().ToUpper();
            correcte = comp_formato.IsMatch(codigo);

            for (int i = 0; i < tipos_armas.Count; i++)
            {
                if (codigo == tipos_armas.ElementAt(i).Key)
                {
                    correcte = false;
                }
            }

            ConsoleColor originalColor = Console.ForegroundColor;
            if (!correcte)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine();
                Console.WriteLine("EL FORMATO DEL CODIGO ES INCORRECTO");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;

                tipos_armas.Add(codigo, nom_arma);

                Console.WriteLine();
                Console.WriteLine("TIPO DE ARMA AGREGADA");
            }

            Console.ForegroundColor = originalColor;
            Console.WriteLine();

            return tipos_armas;
        }
        public static void MostrarTipoArmas(Dictionary<string, string> tipos_armas)
        {
            LiniaVerde();
            for (int i = 0; i < tipos_armas.Count; i++)
            {
                Console.WriteLine($"{tipos_armas.ElementAt(i).Key}: {tipos_armas.ElementAt(i).Value}");
            }
            LiniaVerde();
            Console.WriteLine();
        }
        public static void MostrarArmaGuerreros(Dictionary<string, Guerrer> guerreros)
        {
            string codigo;

            LiniaVerde();
            Console.WriteLine("ESCRIBA EL CODIGO DEL GUERRERO:");

            for (int i = 0; i < guerreros.Count; i++)
            {
                Console.WriteLine($"{guerreros.ElementAt(i).Key}: {guerreros.ElementAt(i).Value.Nom}"); //mostrar guerreros
            }

            LiniaVerde();
            codigo = Console.ReadLine().Trim().ToUpper();
            LiniaVerde();

            for (int i = 0; i < guerreros.Count; i++)
            {
                if (guerreros.ElementAt(i).Key == codigo)
                {
                    for (int j = 0; j < guerreros.ElementAt(i).Value.Armes.Count; j++)
                    {
                        Console.WriteLine($"{guerreros.ElementAt(i).Value.Armes[j]}");
                    }
                }
            }
            LiniaVerde();
        }
        public static void LiniaVerde()
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("-----------------------------------------------------------");
            Console.ForegroundColor = originalColor;
        }
        public static Dictionary<string, string> ModificarNomArma(Dictionary<string, string> tipos_armas)
        {
            string codigo, nou_nom;

            MostrarTipoArmas(tipos_armas);
            Console.WriteLine("ESCRIBA EL CODIOGO DE LA ARMA: ");
            codigo = Console.ReadLine().Trim().ToUpper();

            if (tipos_armas.ContainsKey(codigo))
            {
                Console.WriteLine("ESCRIBA EL NUEVO NOMBRE DE LA ARMA: ");
                nou_nom = Console.ReadLine().Trim().ToUpper();

                tipos_armas[codigo] = nou_nom;
            }

            return tipos_armas;
        }

        public static Dictionary<string, string> EliminarArma(Dictionary<string, string> tipos_armas, Dictionary<string, Guerrer> guerreros)
        {
            string codigo, nombre;
            bool encontrado;
            Guerrer guerrero;

            MostrarTipoArmas(tipos_armas);
            Console.Write("ESCRIBA EL CODIOGO DE LA ARMA: ");
            codigo = Console.ReadLine().Trim().ToUpper();

            Console.WriteLine();

            nombre = "";
            for (int i = 0; i < tipos_armas.Count; i++)
            {
                if (tipos_armas.ElementAt(i).Key == codigo)
                {
                    nombre = tipos_armas.ElementAt(i).Value;
                }
            }

            encontrado = false;
            if (tipos_armas.ContainsKey(codigo)) //TIENE QUE DETECTAR SI ALGUN GUERRERO TIENE LA MISMA ARMA
            {
                for (int i = 0; i < guerreros.Count; i++)
                {
                    guerrero = guerreros.ElementAt(i).Value;
                    for (int j = 0; j < guerrero.Armes.Count && !encontrado; j++)
                    {
                        if (guerrero.Armes[j] == nombre)
                        {
                            encontrado = true;
                        }
                    }
                }
            }

            if (!encontrado)
            {
                tipos_armas.Remove(codigo);
            }

            return tipos_armas;
        }
        public static Dictionary<string, Guerrer> CrearGuerrerArxiu(Dictionary<string, Guerrer> guerreros, List<Personatge> personatges, Dictionary<string, string> tipos_armas)
        {
            string arxiu, linia;
            List<Guerrer> new_guerrers = new List<Guerrer>();
            string[] datos;
            Guerrer guerrero;

            Regex comp_formato = new Regex(@"^[A-Z_]{3,6}:\d{1,3}:((T)|(F))(:[A-Z]{3})+");

            arxiu = "codis.txt";
            if (File.Exists(arxiu))
            {
                using (StreamReader sr = new StreamReader(arxiu)) //leemos el archivo seleccionado
                {
                    linia = sr.ReadLine();

                    while (linia != null) //seguimos el bucle hasta que la linea sea null
                    {
                        datos = linia.Split(':');
                        if (comp_formato.IsMatch(linia))
                        {
                            if (guerreros.ContainsKey(datos[0])) //preguntar para que sirve una excepcion:
                            {
                                throw new Exception($"El código '{datos[0]}' ya existe en la lista de guerreros.");
                            }

                            guerrero = new Guerrer(datos[0]); //creamos y agregamos al guerrero
                            guerreros.Add(datos[0], guerrero);
                            personatges.Add(guerrero);

                            guerrero.PuntsVida = Convert.ToInt32(datos[1]);
                            guerrero.VidaInicial = guerrero.PuntsVida;

                            if (datos[2] == "T") //revisamos si escudo es t o f
                            {
                                guerrero.Escut = true;
                            }
                            else
                            {
                                guerrero.Escut = false;
                            }

                            for (int i = 3; i < datos.Length; i++)
                            {
                                tipos_armas.Add(datos[i], datos[i]);
                                guerrero.Armes.Add(datos[i]);
                            }
                        }

                        linia = sr.ReadLine(); //leemos la siguiente linia
                    }
                }
            }

            arxiu = "log.txt";
            if (File.Exists(arxiu))
            {
                using (StreamWriter sw = new StreamWriter(arxiu, true)) //escribimos sobre el archivo "encriptat.txt"
                {
                    for (int i = 0; i < new_guerrers.Count; i++)
                    {
                        linia = new_guerrers[i].ToString();
                        sw.WriteLine($"{linia}");
                    }
                    
                }
            }

            return guerreros;
        }
        public static Dictionary<string, string> CrearGuerrer(Dictionary<string, string> tipos_armas, Dictionary<string, Guerrer> guerreros, List<Personatge> personatges)
        {
            string datos_guerrer;
            string[] datos;
            Guerrer guerrero;
         
            Regex comp_formato = new Regex(@"^[A-Z_]{3,6}:\d{1,3}:((T)|(F))(:[A-Z]{3})+");

            Console.WriteLine("Introduza al guerrero con el siguiente dormato: <nom_guerrer>:<punts_vida>:<si_escut>:<tipuscodi_arma1>: .. :<tipuscodi_armaN>");
            datos_guerrer = Console.ReadLine().Trim().ToUpper();

            datos = datos_guerrer.Split(':');

            if (comp_formato.IsMatch(datos_guerrer))
            {
                guerrero = new Guerrer(datos[0]); //creamos y agregamos al guerrero
                guerreros.Add(datos[0], guerrero);
                personatges.Add(guerrero);

                guerrero.PuntsVida = Convert.ToInt32(datos[1]); 

                if (datos[2] == "T") //revisamos si escudo es t o f
                {
                    guerrero.Escut = true;
                }
                else
                {
                    guerrero.Escut = false;
                }

                for (int i = 2; i < datos.Length; i++)
                {
                    tipos_armas.Add(datos[i], datos[i]);
                    guerrero.Armes.Add(datos[i]);
                }
            }

            return tipos_armas;
        }
        public static void AsignarBandos(Dictionary<string, Guerrer> guerreros, Queue<Guerrer> aliados_r, Queue<Guerrer> enemigos_r, Queue<Guerrer> aliados_a, Queue<Guerrer> enemigos_a)
        {
            Guerrer guerrero;

            for (int i = 0; i < guerreros.Count; i++)
            {
                guerrero = guerreros.ElementAt(i).Value;
                if (i % 2 == 0)
                {
                    enemigos_r.Enqueue(guerreros.ElementAt(i).Value);
                    guerrero.BandoAliado = false;
                }
                else
                {
                    aliados_r.Enqueue(guerreros.ElementAt(i).Value);
                    guerrero.BandoAliado = true;
                }
            }
        }
    }
}
