using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PhoneNotesDesktop.Services.AuthService;
using PhoneNotesDesktop.Services.DataProvider;
using PhoneNotesDesktop.Services.Navigation;
using PhoneNotesDesktop.ViewModels;
using PhoneNotesDesktop.Views;

namespace PhoneNotesDesktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application
{
	private readonly ServiceProvider _serviceProvider;
	
	public App()
	{
		IServiceCollection services = new ServiceCollection();
		
		services.AddSingleton<IDataProvider, ApiDataProvider>();
							  
		services.AddSingleton<MainWindow>
		(
			provider => new MainWindow()
			{
				DataContext = provider.GetRequiredService<MainViewModel>()
			}
		);
		
		services.AddSingleton<MainViewModel>();
		services.AddSingleton<ListViewModel>();
		services.AddSingleton<NoteFormViewModel>();
		services.AddSingleton<LoginViewModel>();

		services.AddSingleton<INavigationService, NavigationService>();
		services.AddSingleton<IAuthService, AuthService>();
		services.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

		_serviceProvider = services.BuildServiceProvider();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
		mainWindow.Show();
		
		base.OnStartup(e);
	}
}
