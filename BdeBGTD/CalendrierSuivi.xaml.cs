using System;
using System.Collections.Generic;
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
    /// Interaction logic for CalendrierSuivi.xaml
    /// </summary>
    public partial class CalendrierSuivi : Window
    {
        public CalendrierSuivi()
        {
            InitializeComponent();
        }

        private void AnnulerPlanif(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AnnulerPlanif(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
