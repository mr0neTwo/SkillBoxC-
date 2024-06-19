using Database;
using Database.DataStruct;
using Database.Exceptions;
using System.Windows;
using BankSystemLogic.Models;

namespace BankSystem.Models
{
    internal static class DataProvider
    {
        public static event Action<User, BankAccount> BankAccountCreated;
        public static event Action<User, BankAccount> BankAccountChanged;
        public static event Action<User, Transaction> TransactionAdded;


        private static DataBase _database = new DataBase();
        private static User _user = new User(1, "Aegon", "Targaryen");


        internal static Client[] GetAllClients()
        {
            return _database.Clients.GetAll();
        }

        internal static void AddTransaction(Transaction transaction)
        {
            try
            {
                _database.Transactions.Add(_user.Id, transaction);
                TransactionAdded?.Invoke(_user, transaction);
            } 
            catch (InvalidUserIdException)
            {
                OfferToLogin();
            }
            catch (MissingRequiredFieldsException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        
        internal static void AddBankAccount(BankAccount account)
        {
            try
            {
                _database.BankAccounts.Add(_user.Id, account);
                BankAccountCreated?.Invoke(_user, account);
            }
            catch (InvalidUserIdException)
            {
                OfferToLogin();
            }

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
            try
            {
                _database.BankAccounts.Update(_user.Id, bankAccount);
                BankAccountChanged?.Invoke(_user, bankAccount);
            }
            catch (InvalidUserIdException)
            {
                OfferToLogin();
            }
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

        private static void OfferToLogin()
        {
            MessageBox.Show("Please log in to continue working");
        }
    }
}
