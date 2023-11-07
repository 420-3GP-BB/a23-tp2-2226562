using ClassesAffaire;
using System.Xml;

namespace GTD
{
    public class ElementGTD
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateOnly? DateRappel { get; set; }
        public string Statut { get; set; }

        /*

        public ElementGTD(string nom, string statut, string description, string dateRappel) 
        { 
            Nom = nom;
            Statut = statut;
            Description = description;
            DateRappel = dateRappel;
            
        }

        public ElementGTD(string nom, string statut)
        {
            Nom = nom;
            Statut = statut;
        }

        public ElementGTD(string nom, string statut, string description)
        {
            Nom = nom;
            Statut = statut;
            Description = description;
        }

        */
        public ElementGTD(XmlElement element)
        {
            Nom = element.GetAttribute("nom");
            Description = element.InnerText;
            Statut = element.GetAttribute("statut");

            if(element.GetAttribute("dateRappel") != "")
            {
                string[] laDate = element.GetAttribute("dateRappel").Split("-");
                int.TryParse(laDate[0], out int annee);
                int.TryParse(laDate[1], out int mois);
                int.TryParse(laDate[2], out int jour);
                DateRappel = new DateOnly(annee, mois, jour);
            }
            else
            {
                DateRappel = null;
            }
            
        }

        public ElementGTD() 
        {
            Statut = "Entree";
        }

        


        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("element_gtd");
            element.SetAttribute("nom", Nom);
            element.SetAttribute("statut", Statut);
            element.SetAttribute("dateRappel", DateRappel.ToString());
            element.InnerText = Description;

            return element;

        }

        public override string ToString()
        {
            if(Statut == "Suivi")
            {
                return $"{Nom} ({DateRappel.ToString})";
            }
            else
            {
                return Nom;
            }
            
        }



    }



}