using Database.DataStruct;
using Database.Exceptions;

namespace Database.Tables
{
    public class Transactions : DatabaseTable<Transaction>
    {
        public override string DataPath => Path.Combine(DataBase.RootDataFolder, "transaction.xml");

        public Transaction[] FindByAccountID(int accountID)
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach(Transaction transaction in _items)
            {
                if(transaction.AccountID == accountID)
                {
                    transactions.Add(transaction);
                }
            }

            return transactions.ToArray();
        }

        public override void Add(int userId, Transaction transaction)
        {
            if(transaction.Sum == 0)
            {
                throw new MissingRequiredFieldsException("Sum");
            }

            base.Add(userId, transaction);
        }
    }
}
