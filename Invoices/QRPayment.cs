using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Force.Crc32;
using QRCoder.Core;

namespace PDFMaker.Invoices;

public class QRPayment {
	[MaxLength(46)]
	public string Account { get; set; } //ACC
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
	
	public string GetSPD() {
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

	public string GetQRCode() {
		using var qrGenerator = new QRCodeGenerator();
		var qrCodeData = qrGenerator.CreateQrCode(GetSPD(), QRCodeGenerator.ECCLevel.Q);
		var qrCode = new PngByteQRCode(qrCodeData);
		var graphic = qrCode.GetGraphic(20);
		
		return $"data:image/png;base64,{Convert.ToBase64String(graphic)}";
	}
}