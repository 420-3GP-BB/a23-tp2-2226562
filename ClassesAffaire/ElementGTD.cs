using ClassesAffaire;

namespace GTD
{
    public class ElementGTD
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public Date DateRappel { get; set; }
        public Statut Statut { get; set; }

        public ElementGTD(string nom, Statut statut, string description, Date dateRappel) 
        { 
            Nom = nom;
            Statut = statut;
            Description = description;
            DateRappel = dateRappel;
            
        }

        public ElementGTD(string nom, Statut statut)
        {
            Nom = nom;
            Statut = statut;
        }

        public ElementGTD(string nom, Statut statut, string description)
        {
            Nom = nom;
            Statut = statut;
            Description = description;
        }

        public ElementGTD(string nom, Statut statut, Date dateRappel)
        {
            Nom = nom;
            Statut = statut;
            DateRappel = dateRappel;
        }

    }


    public enum Statut
    {
        Entree,
        Action,
        Suivi,
        Archive
    }
}