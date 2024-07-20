using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace TestNav;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private readonly ServiceProvider _serviceProvider;

	public App()
	{
		IServiceCollection services = new ServiceCollection();
		
		services.AddSingleton<IDataProvider, DataProvider>();
							  
		services.AddSingleton<MainWindow>(provider => new MainWindow(){ DataContext = provider.GetRequiredService<MainViewModel>()});
		
		services.AddSingleton<MainViewModel>();
		services.AddSingleton<ListPageViewModel>();
		services.AddSingleton<AddViewModel>();
		services.AddSingleton<BlueViewModel>();
		services.AddSingleton<RedViewModel>();

		services.AddSingleton<INavigationService, NavigationService>();
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
