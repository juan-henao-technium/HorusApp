using HorusApp.Contracts;
using HorusApp.Contracts.Repositories;
using HorusApp.Models;
using HorusApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Repositories
{
	public class UsersRepository : BaseRepository, IUsersRepository
	{
		public UsersRepository(IJsonService jsonService, IHttpClientService httpClientService, ISecureStorageService secureStorageService) : base(jsonService, httpClientService, secureStorageService)
		{
		}

		public async Task<ResponseBase<User>> UserLogin(User userRequest)
		{
			try
			{
				string url = "https://horuschallenges.azurewebsites.net/api/UserSignIn";

				var result = await httpClientService.PostAsync(url, userRequest);

				if (result.IsSuccessStatusCode)
				{
					return new ResponseBase<User>()
					{
						HttpResponse = result,
						Data = await jsonService.GetSerializedResponse<User>(result)
					};
				}
				else
				{
					return await GetServerErrorResponse<User>(result, url);
				}
			}
			catch (Exception ex)
			{
				return GetUnknownErrorResponse<User>(ex);
			}
		}
	}
}
