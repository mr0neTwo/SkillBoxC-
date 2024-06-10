using Database;
using Database.DataStruct;

namespace BankSystem.Models
{
    internal static class DataProvider
    {
        private static DataBase _database = new DataBase();
        private static int _userId = 1;

        internal static Client[] GetAllClients()
        {
            return _database.Clients.GetAll();
        }

        internal static void AddTransaction(Transaction transaction)
        {
            _database.Transactions.Add(_userId, transaction);
        }

        internal static void AddBankAccount(BankAccount account)
        {
            _database.BankAccounts.Add(_userId, account);
        }

        internal static Client[] FindClients(string searchWord)
        {
            return _database.Clients.Find(searchWord);
        }

        internal static BankAccount[] FindBankAccountsByClientId(int clientId)
        {
            return _database.BankAccounts.FindByClientID(clientId);
        }

        internal static Transaction[] FindTransactionsByAccountId(int bankAccountId)
        {
            return _database.Transactions.FindByAccountID(bankAccountId);
        }

        internal static void UpdateBankAccount(BankAccount bankAccount)
        {
            _database.BankAccounts.Update(_userId, bankAccount);
        }

        internal static BankAccount[] GetAllBankAccounts()
        {
            return _database.BankAccounts.GetAll();
        }

        internal static Client FindClientById(int clientID)
        {
            return _database.Clients.FindById(clientID);
        }

        internal static BankAccount FindBankAccountById(int accountId)
        {
            return _database.BankAccounts.FindById(accountId);
        }
    }
}
