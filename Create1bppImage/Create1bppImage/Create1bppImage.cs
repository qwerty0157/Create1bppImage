using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;

namespace Create1bppImage
{
    public class Create1bppImage
    {
        public Create1bppImage()
        {}
        /// <summary>
        /// 指定された画像から1bppのイメージを作成する
        /// </summary>
        /// <param name="img">基になる画像</param>
        /// <returns>1bppに変換されたイメージ</returns>
        public static Bitmap Create1bppImagefunction(Bitmap img, double brightness)
        {
            //1bppイメージを作成する
            Bitmap newImg = new Bitmap(img.Width, img.Height,
                PixelFormat.Format1bppIndexed);

            //Bitmapをロックする
            BitmapData bmpDate = newImg.LockBits(
                new Rectangle(0, 0, newImg.Width, newImg.Height),
                ImageLockMode.WriteOnly, newImg.PixelFormat);

            //新しい画像のピクセルデータを作成する
            byte[] pixels = new byte[bmpDate.Stride * bmpDate.Height];
            for (int y = 0; y < bmpDate.Height; y++)
            {
                for (int x = 0; x < bmpDate.Width; x++)
                {
                    //明るさが brightness 以上の時は白くする
                    if (brightness <= img.GetPixel(x, y).GetBrightness())
                    {
                        //ピクセルデータの位置
                        int pos = (x >> 3) + bmpDate.Stride * y;
                        //白くする
                        pixels[pos] |= (byte)(0x80 >> (x & 0x7));
                    }
                }
            }
            //作成したピクセルデータをコピーする
            IntPtr ptr = bmpDate.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length);

            ////アンセーフコードを使うと、以下のようにもできる
            //unsafe
            //{
            //    byte* pixelPtr = (byte*)bmpDate.Scan0;
            //    for (int y = 0; y < bmpDate.Height; y++)
            //    {
            //        for (int x = 0; x < bmpDate.Width; x++)
            //        {
            //            //明るさが0.5以上の時は白くする
            //            if (0.5f <= img.GetPixel(x, y).GetBrightness())
            //            {
            //                //ピクセルデータの位置
            //                int pos = (x >> 3) + bmpDate.Stride * y;
            //                //白くする
            //                pixelPtr[pos] |= (byte)(0x80 >> (x & 0x7));
            //            }
            //        }
            //    }
            //}

            //ロックを解除する
            newImg.UnlockBits(bmpDate);

            return newImg;
        }
    }
}
