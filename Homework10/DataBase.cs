using System.Xml.Serialization;
using Utilites;

namespace Database
{
    public class DataBase
    {
        public event Action DataChenged;

        private static readonly string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../clients.xml");

        private List<Client> _clients = new();
        private int _currentId;

        public DataBase()
        {
            LoadDataBase();
            LoadCurrentId();
        }

        public void AddClient(int userId, Client client)
        {
            client.Id = ++_currentId;
            client.CreatedAt = Utils.DateTimeToUnixTimestamp(DateTime.Now);
            client.CreatedBy = userId;
            _clients.Add(client);

            SaveDataBase();
        }

        public void RemoveClient(Client client)
        {
            _clients.Remove(client);
            SaveDataBase();
        }

        public Client[] GetAllClients()
        {
            return _clients.ToArray();
        }

        public Client[] FindClients(string searchWord)
        {
            List<Client> result = new List<Client>();

            searchWord = searchWord.ToLower();

            foreach (Client client in _clients)
            {
                if (client.FirstName != null && client.FirstName.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
                else if (client.LastName != null && client.LastName.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
                else if (client.ThirdName != null && client.ThirdName.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
                else if (client.PhoneNumber != null && client.PhoneNumber.ToLower().Contains(searchWord))
                {
                    result.Add(client);
                }
            }

            return result.ToArray();
        }


        public void UpdateClientData(int userId, Client newClient)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                if (_clients[i].Id == newClient.Id)
                {
                    newClient.UpdatedAt = Utils.DateTimeToUnixTimestamp(DateTime.Now);
                    newClient.UpdatedBy = userId;
                    _clients[i] = newClient;    
                }
            }

            SaveDataBase();
        }


        private void LoadDataBase()
        {
            if (!File.Exists(DataPath))
            {
                return;
            }

            XmlSerializer xmlSerializer = new(typeof(List<Client>));
            Stream fStream = new FileStream(DataPath, FileMode.Open, FileAccess.Read);

            if (xmlSerializer.Deserialize(fStream) is List<Client> clients)
            {
                _clients = clients;
            }

            fStream.Close();
        }

        private void SaveDataBase()
        {
            XmlSerializer xmlSerializer = new(typeof(List<Client>));
            Stream fStream = new FileStream(DataPath, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fStream, _clients);
            fStream.Close();

            DataChenged?.Invoke();
        }

        private void LoadCurrentId()
        {
            if(_clients.Count == 0)
            {
                _currentId = 0;

                return;
            }

            _currentId = _clients.Select(client => client.Id).Max();
        }

    }
}
