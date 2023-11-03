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
using System.Xml.Linq;

namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for AjoutEntree.xaml
    /// </summary>
    public partial class AjoutEntree : Window
    {

        public static RoutedCommand AjouterEntree = new RoutedCommand();

        public static RoutedCommand AnnulerEntree = new RoutedCommand();

        private List<TextBox> _lesTextBox;

        ObservableCollection<ElementGTD> _laListe;

        ElementGTD _element;

        public AjoutEntree(ObservableCollection<ElementGTD> liste)
        {
            _lesTextBox = new List<TextBox>();
            _element = new ElementGTD();

            InitializeComponent();

            _lesTextBox.Add(TextBoxNomElement);
            _lesTextBox.Add(DescriptionElement);

            
            DataContext = _element;
            _laListe = liste;
        }

        private void AjouterEntree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ChampsTextePleins();
        }

        private void AjouterEntree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
 
            _laListe.Add(_element);

            _element = new ElementGTD();
            DataContext = _element;

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

        private void AnnulerEntree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AnnulerEntree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
