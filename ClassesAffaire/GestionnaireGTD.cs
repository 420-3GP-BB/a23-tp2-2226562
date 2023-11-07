using System.Collections.ObjectModel;

namespace GTD
{
    public class GestionnaireGTD
    {
        int _indiceCourant;
        public ObservableCollection<ElementGTD> ListeEntrees { get; private set; }
        public ObservableCollection<ElementGTD> ListeActions { get; private set; }
        public ObservableCollection<ElementGTD> ListeSuivis { get; private set; }
        public List<ElementGTD> ProchainesActions { get; private set; }

        public GestionnaireGTD()
        {
            ListeEntrees = new ObservableCollection<ElementGTD>();
            ListeActions = new ObservableCollection<ElementGTD>();
            ListeSuivis = new ObservableCollection<ElementGTD>();
            ProchainesActions = new List<ElementGTD> { };
        }

        public void Ajouter(ElementGTD element)
        {
            ListeEntrees.Add(element);
        }

        public ElementGTD? ElementCourant
        {
            get => _indiceCourant >= 0 && _indiceCourant < ListeEntrees.Count ? ListeEntrees[_indiceCourant] : null;
        }

        public bool ProchainExiste
        {
            get => _indiceCourant < ListeEntrees.Count - 1;
        }

        public void AllerAuProchain()
        {
            if(ProchainExiste)
            {
                _indiceCourant++;
            }
        }


    }
}
