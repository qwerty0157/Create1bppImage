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
            createGrayScale();
            //create1bppImage();
            //create1bppImageByErrorDiffusion();
        }

        public static void createGrayScale()
        {
            var image = (Bitmap)Image.FromFile("../../../../input/circle.png");
            var bmpImage = CreateGrayScale.CreateGrayscaleImage(image);
            bmpImage.Save("../../../../input/grayscale.png");
        }

        public static void create1bppImage()
        {
            var image = (Bitmap)Image.FromFile("../../../../input/circle.png");
            var brightness = 0.75;
            var bmpImage = Create1bppImage.Create1bppImagefunction(image, brightness); 
            bmpImage.Save("../../../../input/1bpp_wtnbyou.png");
        }

        public static void create1bppImageByErrorDiffusion()
        {
            var dir = Directory.GetCurrentDirectory();
            Console.WriteLine(dir);
            var image = (Bitmap)Image.FromFile("../../../../input/wtnbyou.png");
            var bmpImage = Create1bppImageWithErrorDiffusion.Create1bppImageWithErrorDiffusionfunction(image);
            bmpImage.Save("../../../../input/1bpp_wtnbyouByErrorDiffusion.png");
        }
    }
}
