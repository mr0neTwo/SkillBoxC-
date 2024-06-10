using Database.Tables;

namespace Database
{
    public class DataBase
    {
        public static string RootDataFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Data");

        public Clients Clients { get; }
        public BankAccounts BankAccounts { get; }
        public Transactions Transactions { get; }

        public DataBase()
        {
            Clients = new Clients();
            BankAccounts = new BankAccounts();
            Transactions = new Transactions();

            CreateRootDataFolder();
        }

        private void CreateRootDataFolder()
        {
            if(Directory.Exists(RootDataFolder))
            {
                return;
            }

            Directory.CreateDirectory(RootDataFolder);
        }
    }
}
