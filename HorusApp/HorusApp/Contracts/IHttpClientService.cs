using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorusApp.Contracts
{
	public interface IHttpClientService
	{
		Task<HttpResponseMessage> PostAsync<TRequest>(string serviceUrl, TRequest request, Dictionary<string, string> headers = null, double timeout = 20);
		Task<HttpResponseMessage> GetAsync(string serviceUrl, Dictionary<string, string> headers = null, CancellationTokenSource cancellationTokenSource = null, double timeout = 20);
	}
}
