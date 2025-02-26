using Data;
using Microsoft.AspNetCore.Components;

namespace FileGenerator.Invoices.InvoiceComponents;

public enum eAlignment {
	Left,
	Right
}
public partial class PartyInfoComponent : ComponentBase {
	[Parameter]
	public string Title { get; set; } // Seller, Client, Customer, etc.
	
	[Parameter]
	public PartyInfo Info { get; set; } // SellerInfo, CustomerInfo, etc.
	
	[Parameter]
	public eAlignment Alignment { get; set; } = eAlignment.Left;
}