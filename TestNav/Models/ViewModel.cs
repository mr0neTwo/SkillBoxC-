using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestNav.Models;

public abstract class ViewModel
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged([CallerMemberName] string name = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
