using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetBarcode;
using Razor.Templating.Core;
using WkHtmlToPdfDotNet;

namespace NetBarcodeTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var barcode = new Barcode("543534"); // default: Code128
            string image = barcode.GetBase64Image();
            
            string content = await RazorTemplateEngine.RenderAsync("~/Views/Index.cshtml", image);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings {Top = 24, Left = 0, Right = 0},
                DocumentTitle = "empty title",
                Out = "./image-base64.pdf"
            };
            var objectSettings = new ObjectSettings
            {
                HtmlContent = content,
            };
            
            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = {objectSettings}
            };
            new SynchronizedConverter(new PdfTools()).Convert(pdf);
            
            Console.WriteLine("Done");
        }
    }
}