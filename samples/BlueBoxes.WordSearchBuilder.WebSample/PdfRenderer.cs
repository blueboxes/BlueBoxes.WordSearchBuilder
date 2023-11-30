using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Xml.Linq;

namespace BlueBoxes.WordSearchBuilder.WebSample;

/// <summary>
/// Renders WebPage as PDF
/// </summary>
public class PdfRenderer
{
    private readonly AppInfo appInfo;

    public PdfRenderer(AppInfo browserInfo)
    {
        this.appInfo = browserInfo;
    }

    public async Task<IResult> Save(string puzzleSetId, Pages.WebModel.Display view)
    {
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = appInfo.BrowserExecutablePath
        });
        var page = await browser.NewPageAsync();
        await page.GoToAsync($"https://localhost:7028/web/?id={puzzleSetId}&view={view}"); //todo: configure domain

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

        var repository = new WordSearchEngine.WordSearchRepository();
        var puzzleSet = await repository.LoadSetAsync(puzzleSetId);
        var document = PdfReader.Open(stream, PdfDocumentOpenMode.Modify);
        document.Info.Author = "Bluebox Sample";
        document.Info.Creator = "BlueBoxes Sample";
        document.Info.Title = $"{puzzleSet.Title}";
        document.Info.Keywords = string.Join(";", puzzleSet.Puzzles.Select(a => a.Title));

        var outputStream = new MemoryStream();
        document.Save(outputStream, false);

        return Results.File(outputStream, "application/pdf");
    }

}