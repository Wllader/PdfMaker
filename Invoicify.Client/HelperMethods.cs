using System.Net.Http.Json;
using Data.DbModel;

namespace Invoicify.Client;

public static class HelperMethods {
	public static async Task<List<T>> GetRemoteObjectsAsync<T>(HttpClient httpClient, string url) {
		var resp = await httpClient.GetAsync(url);

		if (!resp.IsSuccessStatusCode) {
			Console.WriteLine($"Error: {resp.ReasonPhrase}");
			return [];
		}

		var content = await resp.Content.ReadFromJsonAsync<IEnumerable<T>?>();

		if (content is null) {
			Console.WriteLine("Error: Null returned");
			return [];
		}
		
		return content.ToList();
	}

	public static async Task<T?> GetRemoteObjectAsync<T>(HttpClient httpClient, string url) {
		var resp = await httpClient.GetAsync(url);

		if (!resp.IsSuccessStatusCode) {
			Console.WriteLine($"Error: {resp.ReasonPhrase}");
			return default;
		}

		var content = await resp.Content.ReadFromJsonAsync<T?>();

		if (content is null) {
			Console.WriteLine("Error: Null returned");
			return default;
		}
		
		return content;
	}
}