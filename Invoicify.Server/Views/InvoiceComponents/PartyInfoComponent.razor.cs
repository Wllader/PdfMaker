using Data;
using Data.DbModel;
using Microsoft.AspNetCore.Components;

namespace Invoicify.Server.Views.InvoiceComponents;

/// <summary>
/// Blazor component for displaying party information (seller, customer, etc.) in an invoice.
/// </summary>
public partial class PartyInfoComponent : ComponentBase {
	/// <summary>
	/// Title for the party section (e.g. Seller, Customer).
	/// </summary>
	[Parameter]
	public string Title { get; set; }
	
	/// <summary>
	/// Party information to display.
	/// </summary>
	[Parameter]
	public PartyInfo Info { get; set; }
	
	/// <summary>
	/// Alignment of the party info block (left or right).
	/// </summary>
	[Parameter]
	public eAlignment Alignment { get; set; } = eAlignment.Left;
}

public enum eAlignment {
	Left,
	Right
}
