using Microsoft.AspNetCore.Components;

namespace PDFMaker.Invoices;

public partial class InvoiceView : ComponentBase
{
    [Parameter]
    public Invoice Invoice { get; set; }

    // Calculate subtotal by summing item prices
    public decimal Subtotal => Invoice.Items.Sum(item => item.TotalPrice);

    // Calculate total (could add tax or other fees if needed)
    public decimal Total => Subtotal;  // Modify if additional fees are applied
}
