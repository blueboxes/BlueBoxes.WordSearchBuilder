using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

//Pdf renderer
var bfOptions = new BrowserFetcherOptions();
if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    bfOptions.Path = Path.GetTempPath();
}
var bf = new BrowserFetcher(bfOptions);
bf.DownloadAsync(BrowserFetcher.DefaultChromiumRevision).Wait();
var info = new AppInfo
{
    BrowserExecutablePath = bf.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision)
};
builder.Services.AddSingleton(info);
builder.Services.AddSingleton(new PdfRenderer(info));

var app = builder.Build();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.MapGet("/save/pdf/{id}/{view}", async ([FromServices] PdfRenderer renderer, string id, Display view) => await renderer?.Save(id, view));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.Run();
