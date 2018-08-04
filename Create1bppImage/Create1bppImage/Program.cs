using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;

namespace Create1bppImage
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();
            Console.WriteLine(dir);
            var image = (Bitmap)Image.FromFile("../../../../input/wtnbyou.png");
            var bmpImage = Create1bppImageWithErrorDiffusion.Create1bppImageWithErrorDiffusionfunction(image);
            bmpImage.Save("../../../../input/1bpp_wtnbyou.png");
        }
    }
}
