using Data;
using FileGenerator;

var model = Invoice.GetTestInvoice();

await using var generator = new DocumentGenerator(model);
await generator.SaveAsHtmlAsync(@"B:\Users\Wllader\Desktop\invoice.html");
await generator.SaveAsPdfAsync(@"B:\Users\Wllader\Desktop\invoice.pdf");

