using HorusApp.Contracts;
using HorusApp.Contracts.Repositories;
using HorusApp.Models;
using HorusApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorusApp.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
		private readonly IUsersRepository usersRepository;
		private readonly ISecureStorageService secureStorageService;
		private readonly IDialogService dialogService;

		public LoginPageViewModel(INavigationService navigationService,
			IUsersRepository usersRepository,
			ISecureStorageService secureStorageService,
			IDialogService dialogService)
			: base(navigationService)
		{
			this.usersRepository = usersRepository;
			this.secureStorageService = secureStorageService;
			this.dialogService = dialogService;
			IsPassword = true;
		}

		private bool isPassword;

		public bool IsPassword
		{
			get { return isPassword; }
			set { SetProperty(ref isPassword, value); }
		}

		public string Email { get; set; }
		public string Password { get; set; }
		public ICommand LoginCommand => new DelegateCommand(async () => await Login());
		public ICommand ShowMessageCommand => new DelegateCommand<string>(async (string message) => await ShowMessage(message));
		public ICommand TogglePasswordVisibilityCommand => new DelegateCommand(() => TogglePasswordVisibility());

		private void TogglePasswordVisibility()
		{
			IsPassword = !IsPassword;
		}

		public async override void OnNavigatedTo(INavigationParameters parameters)
		{
			if (await secureStorageService.HasValueAsync("Token"))
			{
				await NavigationService.NavigateAsync("/ChallengesPage");
			}
		}

		private async Task Login()
		{
			try
			{
				dialogService.ShowLoading();

				if (await LoginValidation())
				{
					var user = new User()
					{
						Email = Email.Trim(),
						Password = Password
					};

					var response = await usersRepository.UserLogin(user);

					if (response.HttpResponse.IsSuccessStatusCode)
					{
						await secureStorageService.SetAsync(response.Data.AuthorizationToken, "Token");
						await NavigationService.NavigateAsync("/ChallengesPage");
					}
					else
					{
						await dialogService.ShowConfirmationAsync("Horus", response?.CustomError?.Description ?? "Usuario o contraseña incorrectos");
					}
				}
			}
			catch (Exception ex)
			{
				await dialogService.ShowConfirmationAsync("Horus", "Ha ocurrido un error inesperado");
			}
			finally
			{
				dialogService.HideLoading();
			}
		}

		private async Task ShowMessage(string message)
		{
			try
			{
				await dialogService.ShowConfirmationAsync("Horus", message);
			}
			catch (Exception ex)
			{
				await dialogService.ShowConfirmationAsync("Horus", "Ha ocurrido un error inesperado");
			}
		}

		private async Task<bool> LoginValidation()
		{
			if (string.IsNullOrEmpty(Email))
			{
				await dialogService.ShowConfirmationAsync("Horus", "El campo Email no puede estar vacío");

				return false;
			}
			else if (!Regex.IsMatch(Email.Trim(), @"^([+\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$"))
			{
				await dialogService.ShowConfirmationAsync("Horus", "El correo electrónico no tiene el formato adecuado Ej: example@mail.com");

				return false;
			}

			if (string.IsNullOrEmpty(Password))
			{

				await dialogService.ShowConfirmationAsync("Horus", "El campo Contraseña no puede estar vacío");

				return false;
			}

			return true;
		}
	}
}
