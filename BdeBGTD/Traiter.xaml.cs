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
using System.Windows.Shapes;

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
        ObservableCollection<ElementGTD> maListe;
        List<ElementGTD> laPoubelle = new List<ElementGTD>();
        ElementGTD unElement;
        //private List<TextBox> _lesTextBox;


        public Traiter(ObservableCollection<ElementGTD> _liste, ElementGTD _element, List<ElementGTD> _poubelle)
        {
            //_lesTextBox = new List<TextBox>();
            unElement = new ElementGTD();
            maListe = new ObservableCollection<ElementGTD>();
            laPoubelle = new List<ElementGTD>();
            
            InitializeComponent();
            //_lesTextBox.Add();
            //_lesTextBox.Add();
            
            DataContext = _element;
            unElement = _element;
            maListe = _liste;
            laPoubelle = _poubelle;

            
        }

        private void IncuberCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void IncuberCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ActionRapideCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;
        }

        private void ActionRapide_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            Close();
        }

        private void PlanifierActionCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= true;
        }

        private void PlanifierActionCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CalendrierPlanif planification = new CalendrierPlanif();
            planification.Owner = this;
            planification.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            planification.ShowDialog();
            Close();
        }

        private void PoubelleCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PoubelleCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            laPoubelle.Add(unElement);
            MessageBox.Show($"'{unElement.Nom}' a été enlevé de la liste avec succès");
            Close();
            
        }

        

        public static void ViderPoubelle(ObservableCollection<ElementGTD> maListe, List<ElementGTD> laPoubelle)
        {
            foreach(ElementGTD item in laPoubelle)
            {
                maListe.Remove(item);
            }
        }
    }
}
