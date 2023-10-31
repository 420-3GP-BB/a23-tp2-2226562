﻿using GTD;
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

        public MainWindow()
        {
            InitializeComponent();

            QuitterCmd.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            AjouterEntreeCmd.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));

            TraiterCmd.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));

            DateCourante.Text = date_courante.ToString();

            _gestionnaire = new GestionnaireGTD();

            _pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_SEPARATOR +
                          "Fichiers-3GP" + DIR_SEPARATOR + "bdeb_gtd.xml";

            ChargerFichierXml();



            BoiteEntrees.ItemsSource = _gestionnaire.ListeEntrees;

        }

        private void ChargerFichierXml()
        {
            XmlDocument document = new XmlDocument();
            document.Load(_pathFichier);
            XmlElement racine = document.DocumentElement;
            XmlNodeList lesElementsXML = racine.GetElementsByTagName("element_gtd");

            foreach (XmlElement unElement in lesElementsXML)
            {
                _gestionnaire.ListeEntrees.Add(new ElementGTD(unElement));
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
            //SauvegarderXml();
            Close();
        }

        private void AjouterEntreeCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AjouterEntreeCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Entrée ajoutée avec succès ");
            //nbEntrées++;
        }

        private void TraiterCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //if(nbEntrées > 0)
            //{
            //  e.CanExecute = true;
            //}
        }

        private void TraiterCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("TraitementTest");
        }

        private void AllerProchainJour_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AllerProchainJour_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DateOnly nouvelleDate = date_courante.AddDays(1);
            DateCourante.Text = nouvelleDate.ToString();
            date_courante = nouvelleDate;

            
        }

        private void SauvegarderXml()
        {
            /*
             * XmlDocument doc = new XmlDocument();
            XmlElement racine = doc.CreateElement("gtd");
            doc.AppendChild(racine);

            XmlElement element = doc.CreateElement("element_gtd");
            racine.AppendChild(element);

            foreach (ElementGTD unElem in _gestionnaire.ListeEntrees)
            {
                XmlElement nouveau = unElem.VersXML(doc);
                element.AppendChild(nouveau);
            }
            doc.Save(_pathFichier);
             */

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

            doc.Save(_pathFichier);
        }
    }
}
