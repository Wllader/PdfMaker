using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Force.Crc32;
using Microsoft.EntityFrameworkCore;
using QRCoder.Core;

namespace Data;

[Owned]
public class QrPayment {
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
		var _spd = spd.Split('*');
		var _spdData = _spd[2].Split(':');
		var _crc = _spd[3].Split(':');
		
		for (int i = 0; i < _spdData.Length; i += 2) {
			_qrPayment._data[_spdData[i]] = _spdData[i + 1];
		}

		if (Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(_spd[2])) != Convert.ToUInt32(_crc[1], 16)) {
			throw new Exception("CRC32 does not match");
		}

		_qrPayment.BankNumber = _qrPayment._data["ACC"][2..4];
		_qrPayment.Account = _qrPayment._data["ACC"];
		_qrPayment.AlternativeAccount = _qrPayment._data.GetValueOrDefault("ALT-ACC");
		_qrPayment.Amount = _qrPayment._data.TryGetValue("AM", out var value) ? decimal.Parse(value) : null;
		_qrPayment.Currency = _qrPayment._data.GetValueOrDefault("CC");
		_qrPayment.VariableSymbol = _qrPayment._data.TryGetValue("X-VS", out var value2) ? int.Parse(value2) : null;
		_qrPayment.RecipientName = _qrPayment._data.GetValueOrDefault("RN");
		_qrPayment.MessageForRecipient = _qrPayment._data.GetValueOrDefault("MSG");

		return _qrPayment;
	}
	
	public string GetSpr() {
		_data = new();

		_data["ACC"] = Account;

		if (AlternativeAccount != null) {
			_data["ALT-ACC"] = AlternativeAccount;
		}
		
		if (Amount != null) {
			_data["AM"] = Amount.ToString();
		}
		
		if (Currency != null) {
			_data["CC"] = Currency;
		}
		
		if (MessageForRecipient != null) {
			_data["MSG"] = MessageForRecipient;
		}
		
		if (RecipientName != null) {
			_data["RN"] = RecipientName;
		}
		
		if (VariableSymbol != null) {
			_data["X-VS"] = VariableSymbol.ToString();
		}

		var _sortedData = _data
			.OrderBy(x => x.Key)
			.ThenBy(x => x.Value);

		string spdData = "";
		foreach (var (key, value) in _sortedData) {
			spdData += $"{key}:{value}*";
		}
		
		var _spdBytes = Encoding.UTF8.GetBytes(spdData);
		string crc = $"CRC32:{Crc32Algorithm.Compute(_spdBytes):X8}";
		
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