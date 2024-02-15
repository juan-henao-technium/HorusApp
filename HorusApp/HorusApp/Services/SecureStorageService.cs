using HorusApp.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HorusApp.Services
{
	public class SecureStorageService : ISecureStorageService
	{
		public async Task<T> GetAsync<T>(string key)
		{
			return JsonConvert.DeserializeObject<T>(await SecureStorage.GetAsync(key));
		}

		public async Task<bool> HasValueAsync(string key)
		{
			return await SecureStorage.GetAsync(key) != null;
		}

		public void Remove(string key)
		{
			SecureStorage.Remove(key);
		}

		public async Task SetAsync<T>(T content, string key)
		{
			SecureStorage.Remove(key);
			await SecureStorage.SetAsync(key, JsonConvert.SerializeObject(content));
		}
	}
}
