using GTD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using ClassesAffaire;

namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for AjoutEntree.xaml
    /// </summary>
    public partial class AjoutEntree : Window
    {

        public static RoutedCommand AjouterEntree = new RoutedCommand();

        private List<TextBox> _lesTextBox;

        ObservableCollection<ElementGTD> _liste;

        public AjoutEntree(ElementGTD elemVide, ObservableCollection<ElementGTD> liste)
        {
            _lesTextBox = new List<TextBox>();
            
            InitializeComponent();

            DataContext = elemVide;

            _lesTextBox.Add(TextBoxNomElement);
            _lesTextBox.Add(DescriptionElement);

            //elemVide.Nom = TextBoxNomElement.Text;
            //elemVide.Statut = "Entree";
            //elemVide.Description = TextBoxNomElement.Text;

            //liste.Add(elemVide);

            liste = _liste;
        }

        private void AjouterEntree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ChampsTextePleins();
        }

        private void AjouterEntree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            if ( ! CheckboxFenetreOuverte.IsChecked.Value)
            {
                DialogResult = true;
                Close();
            }
        }

        private bool ChampsTextePleins()
        {
            bool reponse = true;
            foreach (TextBox textBox in _lesTextBox)
            {
                if (textBox.Text.Equals(""))
                {
                    reponse = false;
                    break;
                }
            }
            return reponse;
        }
    }
}
