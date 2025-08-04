using System.Globalization;

namespace Data;

public static class CurrencyTransformer {
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