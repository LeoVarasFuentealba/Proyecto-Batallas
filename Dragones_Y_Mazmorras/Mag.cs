using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragones_Y_Mazmorras
{
    internal class Mag : Personatge
    {
        private int indexmagia;
        public int IdexMagia
        {
            get { return indexmagia; }
            set { int Value = value; }
        }

        public Mag(int indexMagia)
        {
            indexmagia = indexMagia;
            PuntsVida = 150;
        }
        public override int ObtenirPuntsAtac()
        {
            int punts;

            punts = indexmagia * 10;

            return punts;
        }
        public override string ToString()
        {
            string descripcion;
            int puntos;

            puntos = ObtenirPuntsAtac();
            descripcion = Nom + " (" + PuntsVida + "): A" + puntos;


            return descripcion;
        }
    }
}
