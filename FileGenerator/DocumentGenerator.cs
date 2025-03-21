﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace FileGenerator;

public class DocumentGenerator<TComponent> : IAsyncDisposable where TComponent : IComponent {
    private readonly HtmlRenderer _htmlRenderer;
    private readonly object _model;
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public async Task<string> GetHtmlAsync() => await GenerateHtmlStringAsync();

    public DocumentGenerator(object model) {
        _model = model;

        IServiceCollection services = new ServiceCollection();
        services.AddLogging();
        services.AddScoped<HtmlRenderer>();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        _htmlRenderer = serviceProvider.GetRequiredService<HtmlRenderer>();
    }

    private async Task<IBrowser> GetBrowserAsync() {
        if (_browser != null) return _browser;
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        return _browser;
    }

    private async Task<string> GenerateHtmlStringAsync() {
        return await _htmlRenderer.Dispatcher.InvokeAsync(async () => {
            var dictionary = new Dictionary<string, object?> { { "Invoice", _model } };
            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await _htmlRenderer.RenderComponentAsync<TComponent>(parameters);
            string result = output.ToHtmlString();

            Console.WriteLine($"Rendered HTML (Length: {result.Length}): {result}");
            return result;
        });
    }

    public async Task SaveAsHtmlAsync(string path) {
        string html = await GetHtmlAsync();
        Console.WriteLine(html);
        
        await File.WriteAllTextAsync(path, await GetHtmlAsync());
    }

    public async Task SaveAsPdfAsync(string path, PagePdfOptions? options = null) {
        var browser = await GetBrowserAsync();
        var page = await browser.NewPageAsync();
        await page.SetContentAsync(await GetHtmlAsync());
        await page.PdfAsync( options ?? new PagePdfOptions { Path = path, Format = "A4" });
        await page.CloseAsync();
    }

    public async ValueTask DisposeAsync() {
        if (_browser != null)
            await _browser.DisposeAsync();
        _playwright?.Dispose();
        await _htmlRenderer.DisposeAsync();
    }
}
