using Database;

namespace ClientWFP
{
    public class Manager : User, IDatabaseReader, IDatabaseModifierer
    {
        public Manager(int id, string name) : base(id, name)
        {
        }

        protected override AccessType Access => AccessType.Full;

        public void AddClient(DataBase dataBase, Client client)
        {
            dataBase.Clients.Add(Id, client);
        }

        public void UpdateClientData(DataBase dataBase, Client client)
        {
            dataBase.Clients.Update(Id, client);
        }

        public Client[] FindClients(DataBase dataBase, string searchWord)
        {
            return dataBase.Clients.Find(searchWord);
        }

        public Client[] GetAllClients(DataBase dataBase)
        {
           return dataBase.Clients.GetAll();
        }

        public void RemoveClient(DataBase dataBase, Client client)
        {
            dataBase.Clients.Remove(client);
        }
    }
}
