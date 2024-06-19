using Database.DataStruct;
using Database.Exceptions;

namespace Database.Tables
{
    public class Clients : DatabaseTable<Client>
    {
        public override string DataPath => Path.Combine(DataBase.RootDataFolder, "clients.xml");

        public Client[] Find(string searchWord)
        {
            List<Client> result = new List<Client>();

            searchWord = searchWord.ToLower();

            foreach (Client client in _items)
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

        public Client FindById(int clientID)
        {
            foreach (Client client in _items)
            {
                if(client.Id == clientID)
                {
                    return client;
                }
            }

            return new Client();
        }

        public override void Add(int userId, Client client)
        {
            if (string.IsNullOrEmpty(client.FirstName))
            {
                throw new MissingRequiredFieldsException("FirstName");
            }

            if (string.IsNullOrEmpty(client.LastName))
            {
                throw new MissingRequiredFieldsException("LastName");
            }

            base.Add(userId, client);
        }

        public override void Update(int userId, Client client)
        {
            if (string.IsNullOrEmpty(client.FirstName))
            {
                throw new MissingRequiredFieldsException("FirstName");
            }

            if (string.IsNullOrEmpty(client.LastName))
            {
                throw new MissingRequiredFieldsException("LastName");
            }

            base.Update(userId, client);
        }
    }
}
