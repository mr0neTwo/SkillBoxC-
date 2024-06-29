using ElectronicShop.Models;
using System.Data;
using System.Windows.Input;

namespace ElectronicShop.ViewModels
{
    sealed class MainModelView : ViewModel
    {
        private DataProvider _dataProvider;
        
        private DataTable _clientTable;
        private DataTable _ordersTable;

        public DataTable ClientTable
        {
            get => _clientTable; 
            set
            {
                _clientTable = value;
                OnPropertyChanged();
            }
        }
        
        public DataTable OrdersTable
        {
            get => _ordersTable; 
            set
            {
                _ordersTable = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand AddNewClientCommand => new DelegateCommand(AddClient);
        public ICommand SelectClientCommand => new DelegateCommand(SelectClient);
        public ICommand RemoveAllClientCommand => new DelegateCommand(RemeoveAllClient);
        public ICommand AddNewOrderCommand => new DelegateCommand(AddNewOrder);
        public ICommand RemoveAllOrdersCommand => new DelegateCommand(RemoveAllOrders);

        public MainModelView()
        {
            LoadData();
        }

        private void LoadData()
        {
            _dataProvider = new DataProvider();
            
            ClientTable = _dataProvider.GetAllClients();
            OrdersTable = _dataProvider.GetOrders();
        }

        private void SelectClient(object obj)
        {
            Console.WriteLine(obj.GetType());
            
            if (obj is DataRowView dataRow)
            {
                Console.WriteLine($"client {dataRow["FirstName"]}");
            }
        }

        private void AddClient(object obj)
        {
            Console.WriteLine("Add Client Button Clicked");
        }

        private void RemeoveAllClient(object obj)
        {
            Console.WriteLine("Remove All Client Button Clicked");
        }

        private void AddNewOrder(object obj)
        {
            Console.WriteLine("Add Order Button Clicked");
        }

        private void RemoveAllOrders(object obj)
        {
            Console.WriteLine("Remove All Orders Button Clicked");
        }
    }
}
