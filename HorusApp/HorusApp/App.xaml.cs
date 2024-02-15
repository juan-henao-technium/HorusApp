using HorusApp.Contracts;
using HorusApp.Contracts.Repositories;
using HorusApp.Repositories;
using HorusApp.Services;
using HorusApp.ViewModels;
using HorusApp.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace HorusApp
{
	public partial class App
	{
		public App(IPlatformInitializer initializer)
			: base(initializer)
		{
		}

		protected override async void OnInitialized()
		{
			InitializeComponent();

			await NavigationService.NavigateAsync("/LoginPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

			containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ChallengesPage, ChallengesPageViewModel>();

			containerRegistry.Register<IDialogService, DialogService>();
			containerRegistry.RegisterSingleton<IHttpClientService, HttpClientService>();
			containerRegistry.RegisterSingleton<IJsonService, JsonService>();
			containerRegistry.RegisterSingleton<ISecureStorageService, SecureStorageService>();

			containerRegistry.RegisterSingleton<IUsersRepository, UsersRepository>();
			containerRegistry.RegisterSingleton<IChallengesRepository, ChallengesRepository>();
        }
	}
}
