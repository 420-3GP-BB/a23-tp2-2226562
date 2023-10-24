using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesAffaire
{
    internal class Date
    {
        public int Annee { get; set; }
        public int Mois { get; set; }
        public int Jour { get; set; }

        public Date(int annee, int mois, int jour)
        {
            Annee = annee;
            Mois = mois;
            Jour = jour;
        }

        public string ToString()
        {
            return $"{Annee}-{Mois}-{Jour}";
        }

    }
}
