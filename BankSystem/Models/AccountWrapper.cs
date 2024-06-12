﻿using Database.DataStruct;

namespace BankSystem.Models
{
    public class AccountWrapper<T>: IAccount<T> where T: BankAccount
    {
        public int Id => Account.Id;
        public T Account { get; private set; }

        public AccountWrapper(T account)
        {
            Account = account;
        }
    }
}
