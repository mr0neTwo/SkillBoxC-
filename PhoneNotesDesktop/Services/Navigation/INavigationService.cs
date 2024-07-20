using PhoneNotesDesktop.ViewModels;

namespace PhoneNotesDesktop.Services.Navigation;

public interface INavigationService
{
	public ViewModel CurrentView { get; }

	public void NavigateTo<T>() where T : ViewModel;
}
