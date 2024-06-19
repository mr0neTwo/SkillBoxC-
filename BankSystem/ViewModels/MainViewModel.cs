using BankSystem.Models;
using BankSystem.Views;
using BankSystemLogic.Models;
using Database.DataStruct;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BankSystem.ViewModels
{
    public class MainViewModel : ViewModel, ITransfer<AccountWrapper<BankAccount>>
    {
        public string SearchWord 
        { 
            get => _searchWord;
            set 
            { 
                _searchWord = value;
                OnPropertyChanged(nameof(SearchWord));
                UpdateClientsData(value);
            } 
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public BankAccount SelectedBankAccount
        {
            get => _selectedBankAccount;
            set
            {
                _selectedBankAccount = value;
                OnPropertyChanged(nameof(SelectedBankAccount));
            }
        }

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<BankAccount> BankAccounts { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        public ICommand SelectClientCommand => new DelegateCommand(SelectClient);
        public ICommand SelectBankAccountCommand => new DelegateCommand(SelectBankAccount);
        public ICommand NewDebitAccountCommand => new DelegateCommand(NewDebitAccount, CanAddAccount);
        public ICommand NewCreditAccountCommand => new DelegateCommand(NewCreditAccount, CanAddAccount);
        public ICommand NewTransactionCommand => new DelegateCommand(NewTransaction, CanAddTransaction);


        private string _searchWord = string.Empty;
        private Client _selectedClient;
        private BankAccount _selectedBankAccount;

        public MainViewModel()
        {
            Client[] clients = DataProvider.GetAllClients();
            Clients = new ObservableCollection<Client>(clients);
            BankAccounts = new ObservableCollection<BankAccount>();
            Transactions = new ObservableCollection<Transaction>();
        }

        public void Transfer(AccountWrapper<BankAccount> fromAccount, AccountWrapper<BankAccount> toAccount, float amount)
        {
            Transaction outcomingTransaction = new()
            {
                Direction = Direction.Outcoming,
                Sum = amount,
                AccountID = fromAccount.Id,
                Comment = $"transfer to account #{toAccount.Id}",

            };

            Transaction incomingTransaction = new()
            {
                Direction = Direction.Incoming,
                Sum = amount,
                AccountID = toAccount.Id,
                Comment = $"transfer from account #{fromAccount.Id}",

            };

            DataProvider.AddTransaction(outcomingTransaction);
            DataProvider.AddTransaction(incomingTransaction);

            UpdateAccountBalance(fromAccount.Id);
            UpdateAccountBalance(toAccount.Id);
        }


        private void SelectClient(object obj)
        {
            if (obj is Client selectedClient)
            {
               SelectedClient = selectedClient;
            }
            UpdateAccountsData();
            Transactions.Clear();
        }

        private void SelectBankAccount(object obj)
        {
            if (obj is BankAccount selectedClient)
            {
                SelectedBankAccount = selectedClient;
            }
            UpdateTransactionData();
        }

        private void NewDebitAccount(object obj)
        {
            AddNewBankAccountOnDataBase(BankAccountType.Debit);
            UpdateAccountsData();
        }

        private void NewCreditAccount(object obj)
        {
            AddNewBankAccountOnDataBase(BankAccountType.Credit);
            UpdateAccountsData();
        }

        private void NewTransaction(object obj)
        {
            TransactionWindow transactionWindow = new TransactionWindow();

            if (transactionWindow.DataContext is not TransactionViewModel transactionViewModel)
            {
                transactionWindow.Close();

                return;
            }

            transactionViewModel.ClientId = SelectedClient.Id;
            bool? result = transactionWindow.ShowDialog();

            if (result == false)
            {
                return;
            }

            Transaction transaction = transactionViewModel.Transaction;

            if (transaction.Direction == Direction.Transfer)
            {
                BankAccount targetBankAccount = DataProvider.FindBankAccountById(transactionViewModel.TransferData.AccountId);

                AccountWrapper<BankAccount> sourceAccountWrapper = new AccountWrapper<BankAccount>(SelectedBankAccount);
                AccountWrapper<BankAccount> targetAccountWrapper = new AccountWrapper<BankAccount>(targetBankAccount);
                Transfer(sourceAccountWrapper, targetAccountWrapper, transaction.Sum);

                UpdateTransactionData();
                UpdateAccountsData();

                return;
            }
            
            transaction.AccountID = SelectedBankAccount.Id;
            DataProvider.AddTransaction(transaction);

            UpdateAccountBalance(SelectedBankAccount.Id);
            UpdateTransactionData();
            UpdateAccountsData();
        }

        private bool CanAddAccount(object obj)
        {
            return SelectedClient.Id != 0;
        }

        private bool CanAddTransaction(object obj)
        {
            return SelectedBankAccount != null && SelectedBankAccount.Id != 0;
        }

        private void AddNewBankAccountOnDataBase(BankAccountType accountType)
        {
            BankAccount account = new()
            {
                ClientID = SelectedClient.Id,
                AccountType = accountType,
                Balance = 0
            };

            DataProvider.AddBankAccount(account);
        }

        private void UpdateClientsData(string searchWord)
        {
            Clients.Clear();
            Client[] clients = DataProvider.FindClients(searchWord);

            foreach (Client client in clients)
            {
                Clients.Add(client);
            }
        }

        private void UpdateAccountsData()
        {
            BankAccounts.Clear();

            BankAccount[] bankAccounts = DataProvider.FindBankAccountsByClientId(SelectedClient.Id);

            foreach (BankAccount bankAccount in bankAccounts)
            {
                BankAccounts.Add(bankAccount);
            }
        }

        private void UpdateTransactionData()
        {
            Transactions.Clear();

            Transaction[] transactions = DataProvider.FindTransactionsByAccountId(SelectedBankAccount.Id);

            foreach (Transaction transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }

        private void UpdateAccountBalance(int accountId)
        {
            Transaction[] transactions = DataProvider.FindTransactionsByAccountId(accountId);
            float balance = CalculateBalance(transactions);

            BankAccount bankAccount = DataProvider.FindBankAccountById(accountId);
            bankAccount.Balance = balance;
            DataProvider.UpdateBankAccount(bankAccount);
        }

        private static float CalculateBalance(IEnumerable<Transaction> transactions)
        {
            float balance = 0;

            foreach (Transaction transaction in transactions)
            {
                if (transaction.Direction == Direction.Incoming)
                {
                    balance += transaction.Sum;
                }
                else
                {
                    balance -= transaction.Sum;
                }
            }

            return balance;
        }
    }
}
