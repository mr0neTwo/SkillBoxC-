using System.Windows;
using System.Windows.Media;
using DatabaseEF.Entities;

namespace ElectronicShop.ViewModels
{
	internal sealed class ClientViewModel : ViewModel
	{
		public string FirstName
		{
			get => _client.FirstName;
			set
			{
				_client.FirstName = value;
				OnPropertyChanged();
			}
		}

		public string LastName
		{
			get => _client.LastName;
			set
			{
				_client.LastName = value;
				OnPropertyChanged();
			}
		}

		public string ThirdName
		{
			get => _client.ThirdName;
			set
			{
				_client.ThirdName = value;
				OnPropertyChanged();
			}
		}

		public string PhoneNumber
		{
			get => _client.PhoneNumber;
			set
			{
				_client.PhoneNumber = value;
				OnPropertyChanged();
			}
		}

		public string Email
		{
			get => _client.Email;
			set
			{
				_client.Email = value;
				OnPropertyChanged();
			}
		}

		public Brush EmailBorderBrush
		{
			get => _emailBorderBrush;
			set
			{
				_emailBorderBrush = value;
				OnPropertyChanged();
			}
		}

		public DelegateCommand SaveCommand => new(Save);
		public DelegateCommand CancelCommand => new(Cancel);

		public Client Client
		{
			get => _client;
			set
			{
				_client = value;
				OnPropertyChanged(nameof(FirstName));
				OnPropertyChanged(nameof(LastName));
				OnPropertyChanged(nameof(ThirdName));
				OnPropertyChanged(nameof(PhoneNumber));
				OnPropertyChanged(nameof(Email));
			}
		}

		private Client _client = new();
		private Brush _emailBorderBrush = Brushes.DarkGray;

		private void Save(object obj)
		{
			if (string.IsNullOrEmpty(Email))
			{
				EmailBorderBrush = Brushes.Crimson;

				return;
			}

			EmailBorderBrush = Brushes.DarkGray;

			if (obj is Window window)
			{
				window.DialogResult = true;
				window.Close();
			}
		}

		private void Cancel(object obj)
		{
			if (obj is Window window)
			{
				window.DialogResult = false;
				window.Close();
			}
		}
	}
}
