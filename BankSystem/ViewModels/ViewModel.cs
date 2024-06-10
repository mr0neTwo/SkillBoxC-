using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankSystem.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string props = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(props));
        }
    }
}
