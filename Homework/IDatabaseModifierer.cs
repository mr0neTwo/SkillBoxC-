using Database;
using Database.DataStruct;

namespace ClientWFP
{
    public interface IDatabaseModifierer
    {
        void AddClient(DataBase dataBase, Client client);
        void RemoveClient(DataBase dataBase, Client client);
        public void UpdateClientData(DataBase dataBase, Client Client);
    }
}
