using GTD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using ClassesAffaire;
using System.Data.Common;
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Reflection.Metadata;
using System.Diagnostics.Contracts;


namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand AProposCmd = new RoutedCommand();

        public static RoutedCommand QuitterCmd = new RoutedCommand();

        public static RoutedCommand AjouterEntreeCmd = new RoutedCommand();

        public static RoutedCommand TraiterCmd = new RoutedCommand();

        public static RoutedCommand AllerProchainJour = new RoutedCommand();

        public DateOnly date_courante = DateOnly.FromDateTime(DateTime.Now);

        private GestionnaireGTD _gestionnaire;
        private string _pathFichier;
        private char DIR_SEPARATOR = Path.DirectorySeparatorChar;
        private int nbEntrees;
        

        public MainWindow()
        {
            InitializeComponent();

            QuitterCmd.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            AjouterEntreeCmd.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));

            TraiterCmd.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            nbEntrees = 0;

            DateCourante.Text = date_courante.ToString();

            _gestionnaire = new GestionnaireGTD();

            _pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_SEPARATOR +
                          "Fichiers-3GP" + DIR_SEPARATOR + "bdeb_gtd.xml";

            //_lesProchainesActions = new List<ElementGTD>();

            ChargerFichierXml();

            BoiteEntrees.ItemsSource = _gestionnaire.ListeEntrees;
            ProchainesActions.ItemsSource = _gestionnaire.ListeActions;
            SystemeSuivi.ItemsSource = _gestionnaire.ListeSuivis;

        }

        private void ChargerFichierXml()
        {
            XmlDocument document = new XmlDocument();
            document.Load(_pathFichier);
            XmlElement racine = document.DocumentElement;
            XmlNodeList lesElementsXML = racine.GetElementsByTagName("element_gtd");

            foreach (XmlElement unElement in lesElementsXML)
            {
                ElementGTD elem = new ElementGTD(unElement);
                
                switch (elem.Statut)
                {
                    case "Entree":
                        _gestionnaire.ListeEntrees.Add(elem);
                        break;
                    case "Action":
                        if(elem.DateRappel > date_courante)
                        {
                            _gestionnaire.ListeActions.Add(elem);
                        }
                        else
                        {
                            _gestionnaire.AutresElements.Add(elem);
                        }
                        
                        break;
                    case "Suivi":
                        _gestionnaire.ListeSuivis.Add(elem);
                        break;
                    default:
                        break;

                }
            }

        }

        private void AProposCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AProposCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Bdeb GTD \n" +
                            "Version 1.0 \n" +
                            "Auteur: Hamza Oumeziane");
        }

        private void QuitterCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void QuitterCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SauvegarderXml();
            Close();
        }

        private void AjouterEntreeCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AjouterEntreeCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AjoutEntree fenetreEntree = new AjoutEntree(_gestionnaire.ListeEntrees);
            fenetreEntree.Owner = this;
            fenetreEntree.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            fenetreEntree.ShowDialog();
            

        }

        private void TraiterCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(_gestionnaire.ListeEntrees.Count > 0)
            {
              e.CanExecute = true;
            }
        }

        private void TraiterCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
                Traiter fenetreTraiter = new Traiter(_gestionnaire);
                fenetreTraiter.Owner = this;
                fenetreTraiter.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                fenetreTraiter.ShowDialog();
            

            //Traiter.ViderPoubelle(_gestionnaire.ListeEntrees, _poubelle);

             
        }

        private void AllerProchainJour_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AllerProchainJour_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<ElementGTD> poubelleActions = new List<ElementGTD>();
            List<ElementGTD> poubelleSuivis = new List<ElementGTD>();

            //int proActions = _gestionnaire.AutresElements.Count;
            //int suivi = _gestionnaire.ListeSuivis.Count;
            DateOnly nouvelleDate = date_courante.AddDays(1);
            DateCourante.Text = nouvelleDate.ToString();
            date_courante = nouvelleDate;
            foreach (ElementGTD elem in _gestionnaire.ListeSuivis)
            {
                if (elem.DateRappel == date_courante)
                {
                    poubelleSuivis.Add(elem);
                    _gestionnaire.ListeEntrees.Add(elem);
                    elem.Statut = "Entree";
                    elem.DateRappel = null;
                }

            }

            foreach(ElementGTD elem in poubelleSuivis)
            {
                _gestionnaire.ListeSuivis.Remove(elem);
            }


            foreach(ElementGTD elem in _gestionnaire.AutresElements)
            {
                if(elem.DateRappel == date_courante)
                {
                    poubelleActions.Add(elem);
                    _gestionnaire.ListeActions.Add(elem);
                }
            }

            foreach (ElementGTD elem in poubelleActions)
            {
                _gestionnaire.AutresElements.Remove(elem);
            }


        }

        private void SauvegarderXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement racine = doc.CreateElement("element_gtd");
            doc.AppendChild(racine);
            foreach (ElementGTD entree in _gestionnaire.ListeEntrees)
            {
                racine.AppendChild(entree.VersXML(doc));
            }

            foreach (ElementGTD actions in _gestionnaire.ListeActions)
            {
                racine.AppendChild(actions.VersXML(doc));
            }

            foreach (ElementGTD suivi in _gestionnaire.ListeSuivis)
            {
                racine.AppendChild(suivi.VersXML(doc));
            }

            foreach(ElementGTD prochains in _gestionnaire.AutresElements)
            {
                racine.AppendChild(prochains.VersXML(doc));
            }

            doc.Save(_pathFichier);
        }

        private void ProchainesActions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            // Sélectionnez l'élément double-cliqué
            //ElementGTD unElement = (ElementGTD)sender;

            //string selectedText = ProchainesActions.SelectedItem.ToString();

            ElementGTD unElement = (ElementGTD)ProchainesActions.SelectedItem;

            lesActions fenetreAction = new lesActions(unElement, _gestionnaire);
            fenetreAction.Owner = this;
            fenetreAction.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            fenetreAction.ShowDialog();

        }
    }
}
