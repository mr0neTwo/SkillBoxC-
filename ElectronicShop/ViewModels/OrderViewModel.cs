using System.Windows;
using DataBaseADO;

namespace ElectronicShop.ViewModels
{
	internal class OrderViewModel : ViewModel
	{
		public string ProductName
		{
			get => _order.ProductName;
			set
			{
				_order.ProductName = value;
				OnPropertyChanged();
			}
		}
		
		public string ProductCode
		{
			get => _order.ProductCode.ToString();
			set
			{
				if (int.TryParse(value, out int productCode))
				{
					_order.ProductCode = productCode;
					OnPropertyChanged();
				}
			}
		}
		
		public string Email
		{
			get => _order.Email;
			set
			{
				_order.Email = value;
				OnPropertyChanged();
			}
		}

		public DelegateCommand SaveCommand => new(Save);
		public DelegateCommand CancelCommand => new(Cancel);

		public Order Order
		{
			get => _order;
			set
			{
				_order = value;
				OnPropertyChanged(nameof(ProductName));
				OnPropertyChanged(nameof(ProductCode));
				OnPropertyChanged(nameof(Email));
			}
		}
		
		private Order _order;

		private void Save(object obj)
		{
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



