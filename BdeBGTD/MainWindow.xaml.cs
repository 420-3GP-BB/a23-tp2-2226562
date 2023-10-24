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
using System.Windows.Shapes;
using ClassesAffaire;
using System.Data.Common;

namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            QuitterCmd.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));

            AjouterEntreeCmd.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));

            TraiterCmd.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));
        }

        public static int nbEntrées = 0;

        public static RoutedCommand AProposCmd = new RoutedCommand();

        public static RoutedCommand QuitterCmd = new RoutedCommand();

        public static RoutedCommand AjouterEntreeCmd = new RoutedCommand();

        public static RoutedCommand TraiterCmd = new RoutedCommand();


        

        





        public void AProposCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AProposCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Bdeb GTD \n" +
                            "Version 1.0 \n" +
                            "Auteur: Hamza Oumeziane");
        }

        private void QuitterCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= true;
        }

        private void QuitterCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Hello world!");
        }

        private void AjouterEntreeCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AjouterEntreeCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Entrée ajoutée avec succès ");
            nbEntrées++;
        }

        private void TraiterCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(nbEntrées > 0)
            {
                e.CanExecute = true;
            }
        }

        private void TraiterCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("TraitementTest");
        }
    }
}
