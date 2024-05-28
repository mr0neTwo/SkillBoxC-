using Database;
using System.Text.RegularExpressions;

namespace ClientWFP
{
    public class Consultant : User, IDatabaseReader
    {
        public Consultant(int id, string name) : base(id, name)
        {
        }

        protected override AccessType Access => AccessType.Partial;

        public Client[] FindClients(DataBase dataBase, string searchWord)
        {
            Client[] clients = dataBase.FindClients(searchWord);
            clients = ApplyCensorship(clients);

            return clients;
        }

        public Client[] GetAllClients(DataBase dataBase)
        {
            Client[] clients = dataBase.GetAllClients();
            clients = ApplyCensorship(clients);

            return clients;
        }

        private Client[] ApplyCensorship(Client[] clients)
        {
            if (Access == AccessType.Partial)
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
            }

            return clients;
        }

    }
}
