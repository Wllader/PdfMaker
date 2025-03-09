using System.ComponentModel.DataAnnotations;
using Data;
using Data.DbModel;
using FileGenerator;
using TestProject.Invoices; //.razor templates MUST be local to the run project!

// var model = Invoice.GetTestInvoice();
//
// await using var generator = new DocumentGenerator<InvoiceView>(model);
// await generator.SaveAsHtmlAsync(@"B:\Users\Wllader\Desktop\invoice.html");
// await generator.SaveAsPdfAsync(@"B:\Users\Wllader\Desktop\invoice.pdf");


var c = new C();
c.s = "12345";

Console.WriteLine(c.s);

class C {
	[MaxLength(3)]
	public string s { get; set; } = "1234";
}