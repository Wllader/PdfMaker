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
	public string? BIC { get; set; }
	
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

	public static QrPayment FromSpdString(string spd) {
		var _qrPayment = new QrPayment();
		_qrPayment.Domestic = false;
		
		var _spd = spd.Split('*');
		var _spdData = _spd.
			Select(s => s.Split(":", count:2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries))
			.Where(parts => parts.Length == 2)
			.ToFrozenDictionary(parts => parts[0], parts => parts[1]);
		var _spdNoCrc = spd.Split("*CRC32:")[0].Split("SPD*1.0*")[1];
		
		foreach (var (k, v) in _spdData) {
			_qrPayment._data[k] = v;
		}

		if (Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(_spdNoCrc)) != Convert.ToUInt32(_spdData["CRC32"], 16)) {
			throw new Exception("CRC32 does not match");
		}

		_qrPayment.BankNumber = _qrPayment._data["ACC"][2..4];
		_qrPayment.Account = _qrPayment._data["ACC"];
		_qrPayment.AlternativeAccount = _qrPayment._data.GetValueOrDefault("ALT-ACC");
		_qrPayment.Amount = _qrPayment._data.TryGetValue("AM", out var value) ? decimal.Parse(value, CultureInfo.InvariantCulture) : null;
		_qrPayment.Currency = _qrPayment._data.GetValueOrDefault("CC");
		_qrPayment.VariableSymbol = _qrPayment._data.TryGetValue("X-VS", out var value2) ? int.Parse(value2) : null;
		_qrPayment.RecipientName = _qrPayment._data.GetValueOrDefault("RN");
		_qrPayment.MessageForRecipient = _qrPayment._data.GetValueOrDefault("MSG");

		return _qrPayment;
	}

	private string Domestic2International(string? acc) {
		if (acc is null) return String.Empty;
		var _account = $"{BankNumber}{int.Parse(acc):D16}";
		var _checksum = (int)(98 - BigInteger.Parse($"{_account}123500") % 97);
		return $"CZ{_checksum:D2}{_account}";
	}
	
	public string GetSpr() {
		_data = new();

		if (Domestic) {
			Account = Domestic2International(Account);
			AlternativeAccount = Domestic2International(AlternativeAccount);
			Domestic = false;
		}
		
		_data["ACC"] = Account + BIC;
		_data["ALT-ACC"] = AlternativeAccount ?? string.Empty;
		_data["AM"] = Amount.ToString() ?? string.Empty;
		_data["CC"] = Currency ?? string.Empty;
		_data["MSG"] = MessageForRecipient ?? string.Empty;
		_data["RN"] = RecipientName ?? string.Empty;
		_data["X-VS"] = VariableSymbol.ToString() ?? string.Empty;
		

		var _sortedData = _data
			.Where(x => !string.IsNullOrEmpty(x.Value))
			.OrderBy(x => x.Key)
			.ThenBy(x => x.Value);

		string spdData = "";
		foreach (var (key, value) in _sortedData) {
			spdData += $"{key}:{value}*";
		}
		spdData = spdData.TrimEnd('*');
		
		var _spdBytes = Encoding.UTF8.GetBytes(spdData);
		string crc = $"*CRC32:{Crc32Algorithm.Compute(_spdBytes):X8}";
		
		return "SPD*1.0*" + spdData + crc;
	}
	
	public static string GetQrCode(string spd) {
		using var qrGenerator = new QRCodeGenerator();
		var qrCodeData = qrGenerator.CreateQrCode(spd, QRCodeGenerator.ECCLevel.Q);
		var qrCode = new PngByteQRCode(qrCodeData);
		var graphic = qrCode.GetGraphic(20);
		
		return $"data:image/png;base64,{Convert.ToBase64String(graphic)}";
	}
	public string GetQrCode() => GetQrCode(GetSpr());
}