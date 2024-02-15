using HorusApp.Contracts;
using HorusApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Repositories
{
	public class BaseRepository
	{
		protected readonly IJsonService jsonService;
		protected readonly IHttpClientService httpClientService;
		protected readonly ISecureStorageService secureStorageService;

		public BaseRepository(IJsonService jsonService,
			IHttpClientService httpClientService,
			ISecureStorageService secureStorageService)
		{
			this.jsonService = jsonService;
			this.httpClientService = httpClientService;
			this.secureStorageService = secureStorageService;
		}

		public async Task<ResponseBase<T>> GetServerErrorResponse<T>(HttpResponseMessage response, string url, object request = null)
		{
			var properties = new Dictionary<string, string>();

			properties.Add("StatusCode", response.StatusCode.ToString());
			properties.Add("ReasonPhrase", response.ReasonPhrase);

			if (request != null)
				properties.Add("Body", jsonService.Serialize(request));

			return new ResponseBase<T>()
			{
				HttpResponse = response,
				CustomError = await jsonService.GetSerializedResponse<CustomError>(response)
			};
		}

		public ResponseBase<T> GetUnknownErrorResponse<T>(Exception ex)
		{
			return new ResponseBase<T>()
			{
				CustomError = new CustomError()
				{
					Description = "Error inesperado"
				}
			};
		}
	}
}
