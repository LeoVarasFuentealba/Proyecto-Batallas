using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragones_Y_Mazmorras
{
    class Partida
    {
        private string nom;
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        private JocRol tipusjoc;
        public JocRol TipusJoc
        {
            get { return tipusjoc; }
            set { JocRol Value = value; }
        }
        private List<Personatge> personatges;
        public List<Personatge> Personatges
        {
            get { return personatges; }
            set { personatges = value; }
        }
        private bool iniciada;
        public bool Iniciada
        {
            get { return iniciada; }
            set { iniciada = value; }
        }
        private bool acabada;
        public bool Acabada
        {
            get { return acabada; }
            set { acabada = value; }
        }

        public Partida(string nombre, JocRol tipoJoc)
        {
            nom = nombre;
            tipusjoc = tipoJoc;
            personatges = new List<Personatge>();
        }
        public void IniciarPartida()
        {
            iniciada = true;
            acabada = false;
        }
        public void TreurePuntsVida()
        {
            TreurePuntsVida(20);
        }

        public void TreurePuntsVida(int punts)
        {
            for (int i = 0; i < personatges.Count; i++)
            {
                Personatge personatge_act;
                int puntos;

                personatge_act = personatges[i];

                puntos = personatge_act.PuntsVida;
                personatge_act.PuntsVida = puntos - punts;
                if (personatge_act.PuntsVida < 0)
                {
                    personatge_act.PuntsVida = 0;
                }
            }
        }

        public void MostrarPartida()
        {
            string estado;

            if (iniciada)
            {
                estado = "EN CURS";
            } else if (acabada)
            {
                estado = "ACABADA";
            }
            else
            {
                estado = "SENSE INICIAR";
            }
            Console.WriteLine($"{nom} ({tipusjoc.Nom}): {estado}");
        }
    }
}
