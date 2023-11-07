using GTD;
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
    /// Interaction logic for CalendrierPlanif.xaml
    /// </summary>
    public partial class CalendrierPlanif : Window
    {
        ElementGTD element;
        public CalendrierPlanif(ElementGTD elem)
        {
            InitializeComponent();
            element = elem;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar calendrier = sender as Calendar;

            if (calendrier.SelectedDate.HasValue)
            {
                DateTime date = calendrier.SelectedDate.Value;
                element.DateRappel = DateOnly.Parse(date.ToShortDateString());
                element.Statut = "Action";
            }

            Close();
        }
    }
}
