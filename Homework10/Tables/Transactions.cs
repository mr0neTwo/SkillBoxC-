using Database.DataStruct;

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
    }
}
