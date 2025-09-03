using Data.DbModel;
using Microsoft.AspNetCore.Components;

namespace Invoicify.Server.Views;

/// <summary>
/// Blazor component for rendering an invoice view.
/// </summary>
public partial class InvoiceView : ComponentBase
{
    /// <summary>
    /// The invoice to display.
    /// </summary>
    [Parameter]
    public Invoice Invoice { get; set; }
    
    /// <summary>
    /// Gets the subtotal (sum of all invoice items).
    /// </summary>
    public decimal Subtotal => Invoice.TotalPrice;

    /// <summary>
    /// Gets the total amount (can be extended for tax/fees).
    /// </summary>
    public decimal Total => Subtotal;
}
