using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace HaFT.Utilities.Http
{
	public static class Ext
	{
		private static StringContent Serialize(object content)
			=> new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

		private static async Task<string> GetStringAsync(HttpClient client, string url)
		{
			var response = await client.GetAsync(url);

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		public static async Task<T> GetAsync<T>(this HttpClient client, string url)
		{
			var content = await GetStringAsync(client, url);

			return JsonConvert.DeserializeObject<T>(content);
		}

		public static async Task<List<T>> GetListAsync<T>(this HttpClient client, string url)
		{
			var content = await GetStringAsync(client, url);

			return JsonConvert.DeserializeObject<List<T>>(content);
		}

		public static async Task PostAsync(this HttpClient client, string url, object content)
		{
			//var text = await Serialize(content).ReadAsStringAsync();

			var response = await client.PostAsync(url, Serialize(content));

			response.EnsureSuccessStatusCode();
		}

		public static async Task<string> PostAndReturnAsync(this HttpClient client, string url, object content)
		{
			//var text = await Serialize(content).ReadAsStringAsync();

			var response = await client.PostAsync(url, Serialize(content));

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		public static async Task<T> PostAndReturnAsync<T>(this HttpClient client, string url, object content)
		{
			var response = await client.PostAndReturnAsync(url, content);

			return JsonConvert.DeserializeObject<T>(response);
		}

		public static async Task PutAsync(this HttpClient client, string url, object content)
		{
			var response = await client.PutAsync(url, Serialize(content));

			response.EnsureSuccessStatusCode();
		}

		public static async Task<List<T>> PostAndReturnListAsync<T>(this HttpClient client, string url, object content)
		{
			var response = await client.PostAndReturnAsync(url, content);

			return JsonConvert.DeserializeObject<List<T>>(response);
		}
	}
}
