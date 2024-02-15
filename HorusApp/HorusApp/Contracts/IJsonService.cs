using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Contracts
{
	public interface IJsonService
	{
		T Deserialize<T>(string text);
		Task<TResponse> GetSerializedResponse<TResponse>(HttpResponseMessage result);
		string Serialize<T>(T entity);
	}
}
