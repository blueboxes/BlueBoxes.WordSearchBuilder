using BlueBoxes.WordSearchBuilder.Repositories;
using PdfSharpCore.Pdf.IO;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace BlueBoxes.WordSearchBuilder.WebSample;

/// <summary>
/// Renders WebPage as PDF
/// </summary>
public class PdfRenderer
{
    private readonly AppInfo appInfo;

    public PdfRenderer(AppInfo browserInfo)
    {
        appInfo = browserInfo;
    }

    public async Task<IResult> Save(string puzzleId)
    {
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = appInfo.BrowserExecutablePath
        });
        var page = await browser.NewPageAsync();
        await page.GoToAsync($"http://localhost:5017/web/?id={puzzleId}"); //todo: configure domain

        var pdfOptions = new PdfOptions
        {
            Format = PaperFormat.A4,
            DisplayHeaderFooter = false,
            PrintBackground = true,
            HeaderTemplate = "<div style='font-size: 10px; font-family: Arial; color: #666; text-align: center; width: 100%;'>Header</div>",
            FooterTemplate = "<div style='font-size: 10px; font-family: Arial; color: #666; text-align: center; width: 100%;'>Fotter</div>",
            MarginOptions = new MarginOptions
            {
                Top = "1cm",
                Bottom = "1cm",
                Left = "2cm",
                Right = "2cm"
            }
        };
        var stream = await page.PdfStreamAsync(pdfOptions);
        await browser.CloseAsync();

        var repository = new LocalFileRepository();
        var puzzle = await repository.LoadPuzzleAsync(Path.Join(Path.GetTempPath(), $"{puzzleId}.json"));
        var document = PdfReader.Open(stream, PdfDocumentOpenMode.Modify);
        document.Info.Author = "Bluebox Sample";
        document.Info.Creator = "BlueBoxes Sample";
        document.Info.Title = $"{puzzle.Title}";
        document.Info.Keywords = string.Join(";", puzzle.Title);

        var outputStream = new MemoryStream();
        document.Save(outputStream, false);

        return Results.File(outputStream, "application/pdf");
    }

}