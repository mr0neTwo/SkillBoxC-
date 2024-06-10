using BankSystem.Models;
using Database.DataStruct;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BankSystem.ViewModels
{
    public class TransactionViewModel : ViewModel
    {
        public ObservableCollection<Direction> DirectionOptions { get; set; }
        public ObservableCollection<TransferType> TransferTypeOptions { get; set; }
        public ObservableCollection<TransferData> TransferDataOptions { get; set; }

        public Direction Direction { 
            get => _transaction.Direction;
            set 
            { 
                _transaction.Direction = value;
                OnPropertyChanged(nameof(Direction));
                OnPropertyChanged(nameof(ShowTransferOptions));
            }
        }

        public TransferType TransferType
        {
            get => _transactionType;
            set
            {
                _transactionType = value;
                OnPropertyChanged(nameof(TransferType));
                UpdateTransferDataOptions();
            }
        }

        public TransferData TransferData 
        {
            get => _transactionData;
            set
            {
                _transactionData = value;
                OnPropertyChanged(nameof(TransferData));
            }
        }

        public float Sum
        {
            get => _transaction.Sum;
            set
            {
                _transaction.Sum = value;
                OnPropertyChanged(nameof(Sum));
            }
        }

        public string Comment
        {
            get => _transaction.Comment;
            set
            {
                _transaction.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);
        public ICommand CreateCommand => new DelegateCommand(Create);

        public bool ShowTransferOptions => _transaction.Direction == Direction.Transfer;
        public Transaction Transaction => _transaction;
        public int ClientId 
        { 
            private get => clientId; 
            set
            {
                clientId = value;
                UpdateTransferDataOptions();
            }
        }

        private Transaction _transaction;
        private TransferType _transactionType;
        private TransferData _transactionData;
        private int clientId;

        public TransactionViewModel()
        {
            DirectionOptions = new ObservableCollection<Direction>(Enum.GetValues(typeof(Direction)).Cast<Direction>());
            TransferTypeOptions = new ObservableCollection<TransferType>(Enum.GetValues<TransferType>());
            TransferDataOptions = new ObservableCollection<TransferData>();

            Direction = Direction.Outcoming;
            TransferType = TransferType.Internal;
        }

        private void UpdateTransferDataOptions()
        {
            BankAccount[] bankAccounts = TransferType switch
            {
                TransferType.Internal => DataProvider.FindBankAccountsByClientId(ClientId),
                TransferType.External => DataProvider.GetAllBankAccounts(),
                _ => throw new NotImplementedException()
            };

            TransferDataOptions.Clear();
            foreach (BankAccount bankAccount in bankAccounts)
            {
                if (TransferType == TransferType.External && bankAccount.ClientID == ClientId)
                {
                    continue;
                }

                TransferData transferData = new()
                {
                    AccountId = bankAccount.Id,
                    AccountType = bankAccount.AccountType,
                    OwnerName = DataProvider.FindClientById(bankAccount.ClientID).FullName
                };

                TransferDataOptions.Add(transferData);
            }
        }

        private void CloseWindow(object obj)
        {
            if (obj is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }

        private void Create(object obj)
        {
            if (obj is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}
