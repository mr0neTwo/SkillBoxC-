using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ElectronicShop.ViewModels;

internal class ViewModel : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged([CallerMemberName] string name = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
