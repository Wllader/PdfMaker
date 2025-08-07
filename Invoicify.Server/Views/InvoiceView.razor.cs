using Data.DbModel;
using Microsoft.AspNetCore.Components;

namespace Invoicify.Server.Views;

public partial class InvoiceView : ComponentBase
{
    [Parameter]
    public Invoice Invoice { get; set; }
    
    public decimal Subtotal => Invoice.TotalPrice;

    // Calculate total (could add tax or other fees if needed)
    public decimal Total => Subtotal;
}
