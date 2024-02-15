using HorusApp.Models.Responses;
using HorusApp.Models;
using HorusApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HorusApp.Contracts;
using HorusApp.Contracts.Repositories;

namespace HorusApp.Repositories
{
	public class ChallengesRepository : BaseRepository, IChallengesRepository
	{
		public ChallengesRepository(IJsonService jsonService, IHttpClientService httpClientService, ISecureStorageService secureStorageService) : base(jsonService, httpClientService, secureStorageService)
		{
		}

		public async Task<ResponseBase<List<Challenge>>> GetUserChallenges()
		{
			try
			{
				string url = "https://horuschallenges.azurewebsites.net/api/Challenges";

				var result = await httpClientService.GetAsync(url, headers: new Dictionary<string, string> {
					{ "Authorization", await secureStorageService.GetAsync<string>("Token") }
				});

				if (result.IsSuccessStatusCode)
				{
					return new ResponseBase<List<Challenge>>()
					{
						HttpResponse = result,
						Data = await jsonService.GetSerializedResponse<List<Challenge>>(result)
					};
				}
				else
				{
					return await GetServerErrorResponse<List<Challenge>>(result, url);
				}
			}
			catch (Exception ex)
			{
				return GetUnknownErrorResponse<List<Challenge>>(ex);
			}
		}
	}
}
