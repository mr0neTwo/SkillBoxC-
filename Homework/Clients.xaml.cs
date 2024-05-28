using Database;
using System.Windows;
using System.Windows.Controls;

namespace ClientWFP

{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        private IDatabaseReader _databaseReader;
        private IDatabaseModifierer _databaseModifierer;
        private DataBase _dataBase;
        private ClientViewModel _clientViewModel;

        public Clients(DataBase dataBase)
        {
            InitializeComponent();
            _dataBase = dataBase;

            _clientViewModel = new ClientViewModel();
            _clientViewModel.UpdateClients(dataBase.GetAllClients());
            DataContext = _clientViewModel;

            _dataBase.DataChenged += OnDataChanged;
        }

        private void OnDataChanged()
        {
            UpdateAllClients();
        }


        private void OnCreateButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_databaseModifierer == null)
            {
                return;
            }

            CreateClientWindow clientFormWindow = new CreateClientWindow();
            bool? result = clientFormWindow.ShowDialog();

            if (result == true)
            {
                Client newClient = clientFormWindow.Client;
                _databaseModifierer.AddClient(_dataBase, newClient);
            }
        }

        private void OnFindButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_databaseReader == null)
            {
                return;
            }

            string searchWord = SearchTextBox.Text;
            Client[] clients = _databaseReader.FindClients(_dataBase, searchWord);
            _clientViewModel.UpdateClients(clients);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                UpdateAllClients();
            }
        }

        private void UpdateAllClients()
        {
            if (_databaseReader == null)
            {
                return;
            }

            Client[] clients = _databaseReader.GetAllClients(_dataBase);
            _clientViewModel.UpdateClients(clients);
        }

        internal void ChangeUser(User user)
        {            
            if(user is Manager manager)
            {
                _databaseReader = manager;
                _databaseModifierer = manager;
                CreateNewClientButton.Visibility = Visibility.Visible;
            }

            if (user is Consultant consultant)
            {
                _databaseReader = consultant;
                _databaseModifierer = null;
                CreateNewClientButton.Visibility = Visibility.Collapsed;
            }

            UpdateAllClients();
        }

        private void OnClientDoubleClicked(object sender, EventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            var selectedItem = dataGrid.SelectedItem;

            if (selectedItem == null || selectedItem is not Client client)
            {
                return;
            }

            if (_databaseModifierer == null)
            {
                return;
            }

            CreateClientWindow clientFormWindow = new CreateClientWindow();
            clientFormWindow.Client = client;
            bool? result = clientFormWindow.ShowDialog();

            if (result == true)
            {
                Client newClient = clientFormWindow.Client;
                _databaseModifierer.UpdateClientData(_dataBase, newClient);
            }
        }
    }
}
