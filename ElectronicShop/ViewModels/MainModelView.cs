using System.Collections.ObjectModel;
using ElectronicShop.Models;
using System.Windows.Input;
using DataBaseADO;
using ElectronicShop.Views;

namespace ElectronicShop.ViewModels
{
    internal sealed class MainModelView : ViewModel
    {
        public ObservableCollection<Client> Clients { get; private set; }
        public ObservableCollection<Order> Orders { get; private set; }

        public bool IsClientSelected
        {
            get => _isClientSelected;
            set
            {
                _isClientSelected = value;
                OnPropertyChanged();
            }
        }
        
        public bool IsOrderSelected
        {
            get => _isOrderSelected;
            set
            {
                _isOrderSelected = value;
                OnPropertyChanged();
            }
        }

        private Client SelectedClient
        {
            get => _client;
            set
            {
                _client = value;
                IsClientSelected = _client.Id != 0;
            }
        }
        
        private Order SelectedOrder
        {
            get => _order;
            set
            {
                _order = value;
                IsOrderSelected = _order.Id != 0;
            }
        }

        public ICommand AddNewClientCommand => new DelegateCommand(AddClient);
        public ICommand SelectClientCommand => new DelegateCommand(SelectClient);
        public ICommand SelectOrderCommand => new DelegateCommand(SelectOrder);
        public ICommand EditClientCommand => new DelegateCommand(EditClient);
        public ICommand RemoveClientCommand => new DelegateCommand(RemoveClient);
        public ICommand RemoveAllClientCommand => new DelegateCommand(RemoveAllClient);
        public ICommand AddNewOrderCommand => new DelegateCommand(AddNewOrder);
        public ICommand RemoveOrderCommand => new DelegateCommand(RemoveOrder);
        public ICommand RemoveAllOrdersCommand => new DelegateCommand(RemoveAllOrders);

        
        private DataProvider _dataProvider;

        private Client _client;
        private Order _order;
        
        private bool _isClientSelected;
        private bool _isOrderSelected;

        public MainModelView()
        {
            LoadData();
        }

        private void LoadData()
        {
            _dataProvider = new DataProvider();

            Client[] clients = _dataProvider.GetAllClients();
            
            Clients = new ObservableCollection<Client>(clients);
            Orders = new ObservableCollection<Order>();
        }

        private void SelectClient(object obj)
        {
            if (obj is not Client client)
            {
                return;
            }
            
            SelectedClient = client;
            UpdateOrders();
        }

        private void SelectOrder(object obj)
        {
            if (obj is not Order order)
            {
                return;
            }

            SelectedOrder = order;
        }

        private void AddClient(object obj)
        {
            ClientWindow clientWindow = new();
            bool? result = clientWindow.ShowDialog();

            if (result == null || (bool)!result)
            {
                return;
            }

            if (clientWindow.DataContext is not ClientViewModel clientViewModel)
            {
                return;
            }

            Client client = clientViewModel.Client;
            _dataProvider.AddClient(client);
            UpdateClients();
        }

        private void EditClient(object obj)
        {
            ClientWindow clientWindow = new();
            
            if (clientWindow.DataContext is not ClientViewModel clientViewModel)
            {
                return;
            }

            clientViewModel.Client = SelectedClient;
            
            bool? result = clientWindow.ShowDialog();
            
            if (result == null || (bool)!result)
            {
                return;
            }
            
            Client client = clientViewModel.Client;
            _dataProvider.UpdateClient(client);

            UpdateClients();
            UpdateOrders();
        }

        private void RemoveClient(object obj)
        {
            if (string.IsNullOrEmpty(SelectedClient.Email))
            {
                return;
            }

            _dataProvider.RemoveClient(SelectedClient);
            
            UpdateClients();
            UpdateOrders();
        }

        private void RemoveAllClient(object obj)
        {
            _dataProvider.RemoveAllClients();
            UpdateClients();
            UpdateOrders();
        }

        private void AddNewOrder(object obj)
        {
            OrderWindow orderWindow = new();

            if (orderWindow.DataContext is not OrderViewModel orderViewModel)
            {
                return;
            }

            Order order = new() { Email = SelectedClient.Email };
            orderViewModel.Order = order;

            bool? result = orderWindow.ShowDialog();

            if (result == null || (bool)!result)
            {
                return;
            }

            order = orderViewModel.Order;
            _dataProvider.AddOrder(order);
            UpdateOrders();
        }

        private void RemoveOrder(object obj)
        {
            if (!IsOrderSelected)
            {
                return;
            }

            _dataProvider.RemoveOrder(SelectedOrder);
            UpdateOrders();
        }

        private void RemoveAllOrders(object obj)
        {
            if (string.IsNullOrEmpty(SelectedClient.Email))
            {
                return;
            }
            
            _dataProvider.RemoveAllOrder(SelectedClient.Email);
            UpdateOrders();
        }

        private void UpdateClients()
        {
            SelectedClient = default;
            Client[] clients = _dataProvider.GetAllClients();
            Clients.Clear();

            foreach (Client client in clients)
            {
                Clients.Add(client);
            }
        }
        
        private void UpdateOrders()
        {
            SelectedOrder = default;
            Orders.Clear();
            
            if (string.IsNullOrEmpty(SelectedClient.Email))
            {
                return;
            }

            Order[] orders = _dataProvider.GetOrders(SelectedClient.Email);
            
            foreach (Order order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
