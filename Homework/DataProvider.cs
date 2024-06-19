using ClientWFP.Users;
using Database;
using Database.DataStruct;
using Database.Exceptions;
using System.Text.RegularExpressions;
using System.Windows;

namespace ClientWFP

{
    public class DataProvider
    {
        public event Action ClientDataChanged;
        public event Action UserChanged;

        private User _user;
        private DataBase _database;

        public DataProvider(User user)
        {
            _user = user;
            _database = new DataBase();

            _database.Clients.DataChanged += () => ClientDataChanged?.Invoke();
        }

        public void ChangeUser(User user)
        {
            _user = user;
        }

        public void AddClients(Client client)
        {
            if(!_user.Permissions.Has(Permission.CanAddClient))
            {
                return;
            }

            try
            {
                _database.Clients.Add(_user.Id, client);
            }
            catch (InvalidUserIdException)
            {
                MessageBox.Show("Please log in to continue working");
            }
            catch (MissingRequiredFieldsException exception) 
            {
                MessageBox.Show(exception.Message);
            }

           
        }

        public void RemoveClients(Client client)
        {
            if (_user.Permissions.Has(Permission.CanRemoveClient))
            {
                _database.Clients.Remove(client);
            }
        }

        public Client[] GetAllClients()
        {
            if (!_user.Permissions.Has(Permission.CanReadClients))
            {
                return new Client[0];
            }

            Client[] clients = _database.Clients.GetAll();

            if (_user.Permissions.Has(Permission.CanReadPasswordData))
            {
                return clients;
            }

            clients = ApplyCensorship(clients);

            return clients;
        }

        internal Client[] FindClients(string searchWord)
        {
            if (!_user.Permissions.Has(Permission.CanReadClients))
            {
                return new Client[0];
            }

            Client[] clients = _database.Clients.Find(searchWord);

            if (_user.Permissions.Has(Permission.CanReadPasswordData))
            {
                return clients;
            }

            clients = ApplyCensorship(clients);

            return clients;
        }

        internal void UpdateClientData(Client newClient)
        {
            if (!_user.Permissions.Has(Permission.CanEditClient))
            {
                return;
            }

            try
            {
                _database.Clients.Update(_user.Id, newClient);
            }
            catch (InvalidUserIdException)
            {
                MessageBox.Show("Please log in to continue working");
            }
            catch (MissingRequiredFieldsException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private Client[] ApplyCensorship(Client[] clients)
        {
            for (int i = 0; i < clients.Length; i++)
            {
                Client client = clients[i];

                if (string.IsNullOrEmpty(client.PassportNumber))
                {
                    continue;
                }

                client.PassportNumber = Regex.Replace(client.PassportNumber, "[A-Za-z0-9]", "*");
                clients[i] = client;
            }
            
            return clients;
        }
    }
}
