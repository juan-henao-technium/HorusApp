using HorusApp.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HorusApp.Services
{
	public class JsonService : IJsonService
	{
		private JsonSerializer serializer = new JsonSerializer();

		public T Deserialize<T>(string text)
		{
			T deserializedObject = JsonConvert.DeserializeObject<T>(text);
			return deserializedObject;
		}

		public async Task<TResponse> GetSerializedResponse<TResponse>(HttpResponseMessage result)
		{
			using (var stream = await result.Content.ReadAsStreamAsync())
			using (var reader = new StreamReader(stream))
			using (var json = new JsonTextReader(reader))
			{
				return serializer.Deserialize<TResponse>(json);
			}
		}

		public string Serialize<T>(T entity)
		{
			return JsonConvert.SerializeObject(entity);
		}
	}
}
