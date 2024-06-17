using Database.DataStruct;
using System.IO;

namespace BankSystem.Models
{
    internal class Logger
    {
        private string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Data/logs.txt");

        public Logger()
        {
            DataProvider.BankAccountChanged += OnBankAccountChanged;
            DataProvider.BankAccountCreated += OnBankAccountCreated;
            DataProvider.TransactionAdded += OnTransactionAdded;
        }

        private void OnTransactionAdded(User user, Transaction transaction)
        {
            string log = $"New {transaction.Direction} transaction. Amount: {transaction.Sum} Account number: {transaction.AccountID}. Crated by {user.FullName}";
            AddLog(log);
        }

        private void OnBankAccountCreated(User user, BankAccount account)
        {
            string log = $"New account {account.Id} Crated by {user.FullName}";
            AddLog(log);
        }

        private void OnBankAccountChanged(User user, BankAccount account)
        {
            string log = $"Account {account.Id} modified by {user.FullName}";
            AddLog(log);
        }

        public void AddLog(string log)
        {
          
            using (StreamWriter writer = new StreamWriter(_path, true))
            {
                writer.WriteLine($"{DateTime.Now}: {log}");
            }
            
        }
    }
}
