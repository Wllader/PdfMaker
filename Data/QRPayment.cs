using System.Collections.Frozen;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Numerics;
using System.Text;
using Force.Crc32;
using Microsoft.EntityFrameworkCore;
using QRCoder.Core;

namespace Data;

[Owned]
public class QrPayment {
	public bool Domestic { get; set; } = true;
	
	[MaxLength(46)]
	public string Account { get; set; } //ACC
	
	[MaxLength(8)]
	public string? Bic { get; set; }
	
	[MaxLength(4)]
	public string? BankNumber { get; set; }
	
	[MaxLength(93)]
	public string? AlternativeAccount { get; set; } //ALT-ACC
	
	[Column(TypeName = "decimal(10, 2)")]
	public decimal? Amount { get; set; } //AM
	
	[Length(3, 3)]
	public string? Currency { get; set; } //CC
	
	[MaxLength(16)]
	public int? VariableSymbol { get; set; } //X-VS
	
	[MaxLength(35)]
	public string? RecipientName { get; set; } //RN
	
	[MaxLength(60)]
	public string? MessageForRecipient { get; set; } //MSG
	
	private Dictionary<string, string> _data = new();

	private string? _img;

	public static QrPayment FromSprString(string spd) {
		var qrPayment = new QrPayment();
		qrPayment.Domestic = false;
		
		var spr = spd.Split('*');
		var sprData = spr.
			Select(s => s.Split(":", count:2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries))
			.Where(parts => parts.Length == 2)
			.ToFrozenDictionary(parts => parts[0], parts => parts[1]);
		var sprNoCrc = spd.Split("*CRC32:")[0].Split("SPD*1.0*")[1];
		
		foreach (var (k, v) in sprData) {
			qrPayment._data[k] = v;
		}

		if (Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(sprNoCrc)) != Convert.ToUInt32(sprData["CRC32"], 16)) {
			throw new Exception("CRC32 does not match");
		}

		qrPayment.BankNumber = qrPayment._data["ACC"][2..4];
		qrPayment.Account = qrPayment._data["ACC"];
		qrPayment.Bic = qrPayment._data.GetValueOrDefault("BIC");
		qrPayment.AlternativeAccount = qrPayment._data.GetValueOrDefault("ALT-ACC");
		qrPayment.Amount = qrPayment._data.TryGetValue("AM", out var value) ? decimal.Parse(value, CultureInfo.InvariantCulture) : null;
		qrPayment.Currency = qrPayment._data.GetValueOrDefault("CC");
		qrPayment.VariableSymbol = qrPayment._data.TryGetValue("X-VS", out var value2) ? int.Parse(value2) : null;
		qrPayment.RecipientName = qrPayment._data.GetValueOrDefault("RN");
		qrPayment.MessageForRecipient = qrPayment._data.GetValueOrDefault("MSG");

		return qrPayment;
	}

	private string Domestic2International(string? acc) {
		if (acc is null) return String.Empty;
		var account = $"{BankNumber}{int.Parse(acc):D16}";
		var checksum = (int)(98 - BigInteger.Parse($"{account}123500") % 97);
		return $"CZ{checksum:D2}{account}";
	}
	
	public string GetSpr() {
		_data = new();

		if (Domestic) {
			Account = Domestic2International(Account);
			AlternativeAccount = Domestic2International(AlternativeAccount);
			Domestic = false;
		}
		
		_data["ACC"] = Account;
		_data["BIC"] = Bic ?? string.Empty;
		_data["ALT-ACC"] = AlternativeAccount ?? string.Empty;
		_data["AM"] = Amount.ToString() ?? string.Empty;
		_data["CC"] = Currency ?? string.Empty;
		_data["MSG"] = MessageForRecipient ?? string.Empty;
		_data["RN"] = RecipientName ?? string.Empty;
		_data["X-VS"] = VariableSymbol.ToString() ?? string.Empty;
		

		var sortedData = _data
			.Where(x => !string.IsNullOrEmpty(x.Value))
			.OrderBy(x => x.Key)
			.ThenBy(x => x.Value);

		string spdData = "";
		foreach (var (key, value) in sortedData) {
			spdData += $"{key}:{value}*";
		}
		spdData = spdData.TrimEnd('*');
		
		var sprBytes = Encoding.UTF8.GetBytes(spdData);
		string crc = $"*CRC32:{Crc32Algorithm.Compute(sprBytes):X8}";
		
		return "SPD*1.0*" + spdData + crc;
	}
	
	public static string GetQrCode(string spr) {
		using var qrGenerator = new QRCodeGenerator();
		var qrCodeData = qrGenerator.CreateQrCode(spr, QRCodeGenerator.ECCLevel.Q);
		var qrCode = new PngByteQRCode(qrCodeData);
		var graphic = qrCode.GetGraphic(20);
		
		return $"data:image/png;base64,{Convert.ToBase64String(graphic)}";
	}

	public string GetQrCode() {
		return _img ??= GetQrCode(GetSpr());
	}
}