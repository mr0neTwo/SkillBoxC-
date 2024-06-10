using Database.DataStruct;

namespace Database.Tables
{
    public class BankAccounts : DatabaseTable<BankAccount>
    {
        public override string DataPath => Path.Combine(DataBase.RootDataFolder, "bankAccounts.xml");

        public BankAccount[] FindByClientID(int clientID)
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();

            foreach (BankAccount bankAccount in _items)
            {
                if (bankAccount.ClientID == clientID)
                {
                    bankAccounts.Add(bankAccount);
                }
            }

            return bankAccounts.ToArray();
        }

        public BankAccount FindById(int accountId)
        {
            foreach (BankAccount bankAccount in _items)
            {
                if (bankAccount.Id == accountId)
                {
                   return bankAccount;
                }
            }

            return new BankAccount();
        }
    }
}
