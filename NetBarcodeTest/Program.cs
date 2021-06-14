using System.IO;
using NetBarcode;

namespace NetBarcodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var barcode = new Barcode("543534"); // default: Code128
            string image = barcode.GetBase64Image();
            File.WriteAllText("./image-base64.txt", image);
        }
    }
}