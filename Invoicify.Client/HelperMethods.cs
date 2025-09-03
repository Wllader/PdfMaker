using System.Net.Http.Json;
using Data.DbModel;

namespace Invoicify.Client;

/// <summary>
/// Helper methods for HTTP communication with the Invoicify server API.
/// </summary>
public static class HelperMethods {
	/// <summary>
	/// Gets a list of objects from a remote API endpoint.
	/// </summary>
	/// <typeparam name="T">Type of objects to retrieve</typeparam>
	/// <param name="httpClient">HttpClient instance</param>
	/// <param name="url">API endpoint URL</param>
	/// <returns>List of objects of type T</returns>
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

	/// <summary>
	/// Gets a single object from a remote API endpoint.
	/// </summary>
	/// <typeparam name="T">Type of object to retrieve</typeparam>
	/// <param name="httpClient">HttpClient instance</param>
	/// <param name="url">API endpoint URL</param>
	/// <returns>Object of type T or null if not found</returns>
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