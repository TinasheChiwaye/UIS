using BarcodeLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Funeral.Services.Barcode
{
    public  class BarcodeGenerator
    {
        public static string GenerateBarcode(string text)
        {
            using (var ms = new MemoryStream())
            {
                BarcodeLib.Barcode barcodLib = new BarcodeLib.Barcode();

                int imageWidth = 250;
                int imageHeight = 110;
                Color foreColor = Color.Black;
                Color backColor = Color.Transparent;

                Image barcodeImage = barcodLib.Encode(TYPE.CODE11, text, foreColor, backColor, imageWidth, imageHeight);

                barcodeImage.Save(ms, ImageFormat.Jpeg);

                return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray()); 
            }
        }
    }
}