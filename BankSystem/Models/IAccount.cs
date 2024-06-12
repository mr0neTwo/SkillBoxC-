using Database.DataStruct;

namespace BankSystem.Models
{
    internal interface IAccount<out T> where T : BankAccount
    {
        T Account { get; }
    }
}
