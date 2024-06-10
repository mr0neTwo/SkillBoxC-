using Database.DataStruct;

namespace BankSystem.ViewModels
{
    public struct TransferData
    {
        public string OwnerName { get; set; }
        public BankAccountType AccountType { get; set; }
        public int AccountId { get; set; }

        public override string ToString()
        {
            return $"{OwnerName} [{AccountType}] #{AccountId}";
        }
    }
}
