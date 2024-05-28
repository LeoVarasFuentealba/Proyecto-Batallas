using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragones_Y_Mazmorras
{
    class JocRol
    {
        private string nom;
        public string Nom
        {
            get { return nom; }
            set {nom = value; }
        }
        private int maxjugadors;
        public int MaxJugadors
        {
            get { return maxjugadors; }
            set { maxjugadors = value; }
        }

    }
}
