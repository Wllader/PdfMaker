using System.Globalization;

namespace Data;

/// <summary>
/// Provides currency formatting utilities for invoices.
/// </summary>
public static class CurrencyTransformer {
	/// <summary>
	/// Gets a NumberFormatInfo for the given ISO currency code.
	/// Returns a default format if the code is not recognized.
	/// </summary>
	/// <param name="iso">ISO currency code (e.g. "CZK", "USD", "EUR")</param>
	/// <returns>NumberFormatInfo for the currency</returns>
	public static NumberFormatInfo CurrencyFormat(string? iso) =>
		CurrencyFormats.GetValueOrDefault(
			iso ?? "",
			new NumberFormatInfo {
				CurrencySymbol = iso ?? "",
				CurrencyDecimalDigits = 2,
				CurrencyDecimalSeparator = ",",
				CurrencyGroupSeparator = " ",
				CurrencyPositivePattern = 3
			}
		);

	// CurrencyFormats contains custom formatting for supported currencies.
	private static readonly Dictionary<string, NumberFormatInfo> CurrencyFormats = new() {
		["CZK"] = new NumberFormatInfo {
			CurrencySymbol = "Kč",
			CurrencyDecimalDigits = 2,
			CurrencyDecimalSeparator = ",",
			CurrencyGroupSeparator = " ",
			CurrencyPositivePattern = 3 // "n Kč"
		},
		["USD"] = new NumberFormatInfo {
			CurrencySymbol = "$",
			CurrencyDecimalDigits = 2,
			CurrencyDecimalSeparator = ".",
			CurrencyGroupSeparator = ",",
			CurrencyPositivePattern = 0 // "$n"
		},
		["EUR"] = new NumberFormatInfo {
			CurrencySymbol = "€",
			CurrencyDecimalDigits = 2,
			CurrencyDecimalSeparator = ",",
			CurrencyGroupSeparator = ".",
			CurrencyPositivePattern = 3 // "n €"
		}
	};
}