using Data;
using Data.DbModel;
using Microsoft.AspNetCore.Components;

namespace Invoicify.Client.Invoices.InvoiceComponents;

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