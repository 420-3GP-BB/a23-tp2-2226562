using GTD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Shapes;
using ClassesAffaire;
using System.Xml.Linq;

namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for Traiter.xaml
    /// </summary>
    public partial class Traiter : Window
    {
        public static RoutedCommand PoubelleCmd = new RoutedCommand();
        public static RoutedCommand IncuberCmd = new RoutedCommand();
        public static RoutedCommand PlanifierActionCmd = new RoutedCommand();
        public static RoutedCommand ActionRapideCmd = new RoutedCommand();
        public static RoutedCommand Retour = new RoutedCommand();
        ObservableCollection<ElementGTD> maListe;
        ObservableCollection<ElementGTD> mesActions;
        ObservableCollection<ElementGTD> mesSuivis;
        List<ElementGTD> mesProchainesActions;
        ElementGTD unElement;
        int elementCourant = 0;
        GestionnaireGTD unGestionnaire;
        //List<ElementGTD> laPoubelle = new List<ElementGTD>();
        //ElementGTD unElement;
        //private List<TextBox> _lesTextBox;


        public Traiter(GestionnaireGTD _gestionnaire)
        {
            unElement = new ElementGTD();
            
            InitializeComponent();

            unGestionnaire = _gestionnaire;
            maListe = unGestionnaire.ListeEntrees;
            mesActions = unGestionnaire.ListeActions;
            mesSuivis = unGestionnaire.ListeSuivis;
            mesProchainesActions = unGestionnaire.ProchainesActions;

            DataContext = unGestionnaire.ListeEntrees[elementCourant];

          

            
        }

        private void IncuberCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void IncuberCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CalendrierSuivi suivi = new CalendrierSuivi();
            suivi.Owner = this;
            suivi.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            suivi.ShowDialog();

            Close();

            if (elementCourant < maListe.Count)
            {
                DataContext = maListe[elementCourant];
            }
            else
            {
                Close();
            }
        }

        private void ActionRapideCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;
        }

        private void ActionRapide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            unElement = maListe[elementCourant];

            mesActions.Add(unElement);
            unElement.Statut = "Action";
            unElement.DateRappel = DateOnly.FromDateTime(DateTime.Now);

            maListe.Remove(unElement);

            if (elementCourant < maListe.Count)
            {
                DataContext = maListe[elementCourant];
            }
            else
            {
                Close();
            }
        }

        private void PlanifierActionCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= true;
        }

        private void PlanifierActionCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            unElement = maListe[elementCourant];

            CalendrierPlanif planification = new CalendrierPlanif(unElement);
            planification.Owner = this;
            planification.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            planification.ShowDialog();

            mesProchainesActions.Add(unElement);

            maListe.Remove(unElement);


            if (elementCourant < maListe.Count)
            {
                DataContext = maListe[elementCourant];
            }
            else
            {
                Close();
            }
        }

        private void PoubelleCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PoubelleCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            maListe.Remove(maListe[elementCourant]);
            if(elementCourant < maListe.Count)
            {
                DataContext = maListe[elementCourant];
            }
            else
            {
                Close();
            }
            
            
        }

        


        private void Retour_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Retour_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close() ;
        }
    }
}
