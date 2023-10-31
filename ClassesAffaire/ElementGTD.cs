using ClassesAffaire;
using System.Xml;

namespace GTD
{
    public class ElementGTD
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public string DateRappel { get; set; }
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
            DateRappel = element.GetAttribute("dateRappel");
        }

        public ElementGTD() { }

        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("element_gtd");
            element.SetAttribute("nom", Nom);
            element.SetAttribute("statut", Statut);
            element.SetAttribute("dateRappel", DateRappel);
            element.InnerText = Description;

            return element;

        }

        public override string ToString()
        {
            return Nom;
        }



    }



}