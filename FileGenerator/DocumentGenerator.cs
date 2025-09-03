using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace FileGenerator;

/// <summary>
/// DocumentGenerator is a generic class that renders a Blazor component to HTML or PDF.
/// </summary>
/// <typeparam name="TComponent">The type of the Blazor component to render.</typeparam>
public class DocumentGenerator<TComponent> : IAsyncDisposable where TComponent : IComponent {
    private readonly HtmlRenderer _htmlRenderer;
    // public object? _model { get; set; }
    private readonly object _model;
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    /// <summary>
    /// Asynchronously gets the rendered HTML string for the component and model.
    /// </summary>
    /// <returns>HTML string</returns>
    public async Task<string> GetHtmlAsync() => await GenerateHtmlStringAsync();

    /// <summary>
    /// Asynchronously gets the PDF bytes for the rendered component and model.
    /// </summary>
    /// <returns>PDF file as byte array</returns>
    public async Task<byte[]> GetPdfAsync() => await GeneratePdfAsync();

    /// <summary>
    /// Initializes the DocumentGenerator with required services.
    /// </summary>
    public DocumentGenerator() {
        IServiceCollection services = new ServiceCollection();
        services.AddLogging();
        services.AddScoped<HtmlRenderer>();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        _htmlRenderer = serviceProvider.GetRequiredService<HtmlRenderer>();
    }

    /// <summary>
    /// Initializes the DocumentGenerator with a model.
    /// </summary>
    /// <param name="model">Model to be rendered</param>
    public DocumentGenerator(object model) : this() {
        _model = model;
    }

    /// <summary>
    /// Gets a Playwright browser instance (headless Chromium).
    /// </summary>
    /// <returns>IBrowser instance</returns>
    private async Task<IBrowser> GetBrowserAsync() {
        if (_browser != null) return _browser;
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        return _browser;
    }

    /// <summary>
    /// Renders the component to an HTML string using the model.
    /// </summary>
    /// <returns>HTML string</returns>
    private async Task<string> GenerateHtmlStringAsync() {
        return await _htmlRenderer.Dispatcher.InvokeAsync(async () => {
            var dictionary = new Dictionary<string, object?> { { "Invoice", _model } };
            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await _htmlRenderer.RenderComponentAsync<TComponent>(parameters);
            string result = output.ToHtmlString();

            return result;
        });
    }

    /// <summary>
    /// Renders the component to a PDF file using Playwright.
    /// </summary>
    /// <param name="options">PDF options (format, path, etc.)</param>
    /// <returns>PDF file as byte array</returns>
    private async Task<byte[]> GeneratePdfAsync(PagePdfOptions? options = null) {
        var browser = await GetBrowserAsync();
        var page = await browser.NewPageAsync();
        await page.SetContentAsync(await GetHtmlAsync());
        var pdf = await page.PdfAsync(options ?? new PagePdfOptions {Format = "A4"});
        await page.CloseAsync();

        return pdf;
    }

    /// <summary>
    /// Saves the rendered HTML to a file.
    /// </summary>
    /// <param name="path">File path</param>
    public async Task SaveAsHtmlAsync(string path) {
        string html = await GetHtmlAsync();
        
        await File.WriteAllTextAsync(path, await GetHtmlAsync());
    }

    /// <summary>
    /// Saves the rendered PDF to a file.
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="options">PDF options (optional)</param>
    public async Task SaveAsPdfAsync(string path, PagePdfOptions? options = null) {
        await GeneratePdfAsync(options ?? new PagePdfOptions {Path = path, Format = "A4"});
    }

    /// <summary>
    /// Disposes browser, Playwright, and renderer resources asynchronously.
    /// </summary>
    public async ValueTask DisposeAsync() {
        if (_browser != null)
            await _browser.DisposeAsync();
        _playwright?.Dispose();
        await _htmlRenderer.DisposeAsync();
    }
}
