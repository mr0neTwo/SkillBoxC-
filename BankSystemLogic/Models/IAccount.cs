using Database.DataStruct;

namespace BankSystemLogic.Models
{
    public interface IAccount<out T> where T : BankAccount
    {
        T Account { get; }
    }
}
