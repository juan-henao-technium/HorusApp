using HorusApp.Contracts;
using HorusApp.Contracts.Repositories;
using HorusApp.Models;
using HorusApp.Repositories;
using HorusApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace HorusApp.ViewModels
{
	public class ChallengesPageViewModel : ViewModelBase
	{
		private readonly IChallengesRepository challengesRepository;
		private readonly IDialogService dialogService;
		private readonly ISecureStorageService secureStorageService;

		public ChallengesPageViewModel(INavigationService navigationService,
			IChallengesRepository challengesRepository,
			IDialogService dialogService,
			ISecureStorageService secureStorageService) : base(navigationService)
		{
			this.challengesRepository = challengesRepository;
			this.dialogService = dialogService;
			this.secureStorageService = secureStorageService;
		}

		private List<Challenge> challenges;
		public List<Challenge> Challenges
		{
			get { return challenges; }
			set { SetProperty(ref challenges, value); }
		}

		private double totalChallenges;
		public double TotalChalleges
		{
			get { return totalChallenges; }
			set { SetProperty(ref totalChallenges, value); }
		}

		private double completedChallenges;
		public double CompletedChallenges
		{
			get { return completedChallenges; }
			set { SetProperty(ref completedChallenges, value); }
		}

		public ICommand LogoutCommand => new DelegateCommand(async () => await Logout());
		public ICommand SelectChallegeCommand => new DelegateCommand<Challenge>(async (Challenge selectedChallenge) => await SelectChallege(selectedChallenge));

		public async override void OnNavigatedTo(INavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);
			await GetUserChallenges();
		}

		private async Task SelectChallege(Challenge selectedChallenge)
		{
			await dialogService.ShowConfirmationAsync(selectedChallenge.Title, selectedChallenge.Description);

		}

		private async Task GetUserChallenges()
		{
			try
			{
				dialogService.ShowLoading();

				var response = await challengesRepository.GetUserChallenges();

				if (response.HttpResponse.IsSuccessStatusCode)
				{
					Challenges = response.Data;
					if (Challenges != null && Challenges.Any())
					{
						TotalChalleges = Challenges.Count();
						CompletedChallenges = Challenges.Count(c => c.CurrentPoints >= c.TotalPoints);
					}
				}
				else
				{
					if (response.HttpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
						await Logout();
					else
						await dialogService.ShowConfirmationAsync("Horus", response?.CustomError?.Description ?? "Ha ocurrido un error inesperado");
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

		private async Task Logout()
		{
			try
			{
				secureStorageService.Remove("Token");
				await NavigationService.NavigateAsync("/LoginPage");
			}
			catch (Exception ex)
			{
				await dialogService.ShowConfirmationAsync("Horus", "Ha ocurrido un error inesperado");
			}
		}
	}
}
