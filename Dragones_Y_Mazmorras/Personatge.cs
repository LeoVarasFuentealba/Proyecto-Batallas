using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragones_Y_Mazmorras
{
    class Personatge
    {
        private string nom;
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        private int puntsvida;
        public int PuntsVida
        {
            get { return puntsvida; }
            set { puntsvida = value; }
        }
        public Personatge()
        {
            nom = "SD";
            puntsvida = 10;
        }
        public Personatge(int puntsVida)
        {
            nom = "SD";
            puntsvida = puntsVida;
        }
        public override string ToString() 
        {
            string descripcion;

            descripcion = nom + " (" + puntsvida + ")";

            return descripcion;
        }
        public virtual int ObtenirPuntsAtac() 
        {
            return 10;
        }
        public virtual int ObtenirPuntsDefensa()
        {
            return 0;
        }
    }
}
