using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using HorusApp.Contracts;

namespace HorusApp.Services
{
	public class HttpClientService : IHttpClientService
	{
		private HttpClient client;

		public HttpClientService()
		{
			client = new HttpClient(new NativeMessageHandler());
		}

		public async Task<HttpResponseMessage> PostAsync<TRequest>(string serviceUrl, TRequest request, Dictionary<string, string> headers = null, double timeout = 20)
		{
			try
			{
				if (headers != null)
				{
					foreach (var item in headers)
					{
						if (client.DefaultRequestHeaders.Contains(item.Key))
							client.DefaultRequestHeaders.Remove(item.Key);

						client.DefaultRequestHeaders.Add(item.Key, item.Value);
					}
				}

				string bodyRequest = JsonConvert.SerializeObject(request);
				return await client.PostAsync(serviceUrl, new StringContent(bodyRequest, Encoding.UTF8, "application/json"));
			}
			catch (OperationCanceledException)
			{
				return await PostAsync(serviceUrl, request, headers, timeout);
			}
		}

		public async Task<HttpResponseMessage> GetAsync(string serviceUrl, Dictionary<string, string> headers = null, CancellationTokenSource cancellationTokenSource = null, double timeout = 20)
		{
			try
			{
				if (headers != null)
				{
					foreach (var item in headers)
					{
						if (client.DefaultRequestHeaders.Contains(item.Key))
							client.DefaultRequestHeaders.Remove(item.Key);

						client.DefaultRequestHeaders.Add(item.Key, item.Value);
					}
				}

				return await client.GetAsync(serviceUrl);
			}
			catch (OperationCanceledException)
			{
				return await GetAsync(serviceUrl, headers, cancellationTokenSource, timeout);
			}
		}
	}
}
