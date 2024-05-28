using Database;

namespace ClientWFP
{
    public interface IDatabaseReader
    {
        Client[] GetAllClients(DataBase dataBase);
        Client[] FindClients(DataBase dataBase, string searchWord);
    }
}
