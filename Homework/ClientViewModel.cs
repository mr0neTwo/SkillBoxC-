using ClientWFP.Users;
using Database.DataStruct;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ClientWFP
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Client> Clients { get; set; }
        public Visibility CreateButtonVisibility 
        { 
            get => _createButtonVisibility; 
            set 
            {
                _createButtonVisibility = value;
                OnPropertyChanged(nameof(CreateButtonVisibility));
            }
        }

        private AppData _appData;

        private Visibility _createButtonVisibility;

        public void Initialize(AppData appData)
        {
            _appData = appData;

            appData.UserChanged += OnUserChanged;

            Clients = new ObservableCollection<Client>(_appData.DataProvider.GetAllClients());

            _appData.DataProvider.ClientDataChanged += OnClientsDataChanged;
        }

        private void OnUserChanged()
        {
            bool canAddClient = _appData.CurrentUser.Permissions.Has(Permission.CanAddClient);
            CreateButtonVisibility = canAddClient ? Visibility.Visible : Visibility.Collapsed;
            UpdateClients();
        }

        private void OnClientsDataChanged()
        {
            UpdateClients();
        }

        internal void UpdateClients()
        {
            Clients.Clear();
            Client[] clients = _appData.DataProvider.GetAllClients();
            
            foreach (Client client in clients)
            {
                Clients.Add(client);
            }

            OnPropertyChanged(nameof(Clients));
        }

        internal void AddClient(Client newClient)
        {
            _appData.DataProvider.AddClients(newClient);
        }

        internal void FindClients(string searchWord)
        {
            Clients.Clear();

            Client[] clients = _appData.DataProvider.FindClients(searchWord);

            foreach (Client client in clients)
            {
                Clients.Add(client);
            }

             OnPropertyChanged(nameof(Clients));
        }

        internal void UpdateClientData(Client newClient)
        {
            _appData.DataProvider.UpdateClientData(newClient);
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
               
        }
    }

}