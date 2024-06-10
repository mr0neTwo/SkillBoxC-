using Database;
using Database.DataStruct;

namespace ClientWFP
{
    public interface IDatabaseReader
    {
        Client[] GetAllClients(DataBase dataBase);
        Client[] FindClients(DataBase dataBase, string searchWord);
    }
}
