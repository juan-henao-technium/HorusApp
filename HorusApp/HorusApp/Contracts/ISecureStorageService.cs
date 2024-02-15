using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Contracts
{
	public interface ISecureStorageService
	{
		Task SetAsync<T>(T content, string key);
		Task<T> GetAsync<T>(string key);
		void Remove(string key);
		Task<bool> HasValueAsync(string key);
	}
}
