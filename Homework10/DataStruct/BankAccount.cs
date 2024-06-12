namespace Database.DataStruct
{
    public class BankAccount : IDatabaseTable
    {
        public int Id { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int ClientID { get; set; }
        public BankAccountType AccountType { get; set; }
        public float Balance { get; set; }

    }

    
}
