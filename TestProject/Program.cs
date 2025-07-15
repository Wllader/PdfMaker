using Data.DbModel;
using FileGenerator;
using TestProject.Invoices; //.razor templates MUST be local to the run project!

var model = InvoiceExtensions.GetTestInvoice_LazyCode();

await using var generator = new DocumentGenerator<InvoiceView>(model);
await generator.SaveAsHtmlAsync(@"B:\Users\Wllader\Desktop\invoice.html");
await generator.SaveAsPdfAsync(@"B:\Users\Wllader\Desktop\invoice.pdf");



