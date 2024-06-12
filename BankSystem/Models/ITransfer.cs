using Database.DataStruct;

namespace BankSystem.Models
{
    public interface ITransfer<in T> where T : AccountWrapper<BankAccount>
    {
        public void Transfer(T fromAccount, T toAccount, float amount);
    }
}
