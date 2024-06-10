using Database.DataStruct;
using System.Windows;
using System.Windows.Controls;

namespace ClientWFP

{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        private ClientViewModel _clientViewModel;
        private readonly AppData _appData;

        public Clients(AppData appData)
        {
            InitializeComponent();

            _appData = appData;
            _clientViewModel = new ClientViewModel();
            _clientViewModel.Initialize(appData);
            DataContext = _clientViewModel;

        }

        private void OnCreateButtonClicked(object sender, RoutedEventArgs e)
        {
           
            ClientDataWindow clientFormWindow = new ClientDataWindow();
            bool? result = clientFormWindow.ShowDialog();

            if (result == true)
            {
                Client newClient = clientFormWindow.Client;
                _clientViewModel.AddClient(newClient);
            }
        }

        private void OnFindButtonClicked(object sender, RoutedEventArgs e)
        {
            string searchWord = SearchTextBox.Text;
            _clientViewModel.FindClients(searchWord);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                _clientViewModel.UpdateClients();
            }
        }

        private void OnClientDoubleClicked(object sender, EventArgs e)
        {
            DataGrid? dataGrid = sender as DataGrid;
            var selectedItem = dataGrid.SelectedItem;

            if (selectedItem == null || selectedItem is not Client client)
            {
                return;
            }

            ClientDataWindow clientFormWindow = new ClientDataWindow();
            clientFormWindow.Client = client;
            clientFormWindow.EnableFieldByPermissions(_appData.CurrentUser.Permissions);
            bool? result = clientFormWindow.ShowDialog();

            if (result == true)
            {
                Client newClient = clientFormWindow.Client;
               _clientViewModel.UpdateClientData(newClient);
            }
        }
    }
}
