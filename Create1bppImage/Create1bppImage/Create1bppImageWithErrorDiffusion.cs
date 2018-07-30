using System.DrawingCore;
using System.DrawingCore.Imaging;

/// <summary>
/// 誤差拡散法（Floyd-Steinbergディザリング）を使用して、
/// 指定された画像から1bppのイメージを作成する
/// </summary>
/// <param name="img">基になる画像</param>
/// <returns>1bppに変換されたイメージ</returns>
public static Bitmap Create1bppImageWithErrorDiffusion(Bitmap img)
{
    //1bppイメージを作成する
    Bitmap newImg = new Bitmap(img.Width, img.Height,
        PixelFormat.Format1bppIndexed);

    //Bitmapをロックする
    BitmapData bmpDate = newImg.LockBits(
        new Rectangle(0, 0, newImg.Width, newImg.Height),
        ImageLockMode.WriteOnly, newImg.PixelFormat);

    //現在の行と次の行の誤差を記憶する配列
    float[][] errors = new float[2][] {
        new float[bmpDate.Width + 1],
        new float[bmpDate.Width + 1]
    };

    //新しい画像のピクセルデータを作成する
    byte[] pixels = new byte[bmpDate.Stride * bmpDate.Height];
    for (int y = 0; y < bmpDate.Height; y++)
    {
        for (int x = 0; x < bmpDate.Width; x++)
        {
            //ピクセルの明るさに、誤差を加える
            float err = img.GetPixel(x, y).GetBrightness() + errors[0][x];
            //明るさが0.5以上の時は白くする
            if (0.5f <= err)
            {
                //ピクセルデータの位置
                int pos = (x >> 3) + bmpDate.Stride * y;
                //白くする
                pixels[pos] |= (byte)(0x80 >> (x & 0x7));
                //誤差を計算（黒くした時の誤差はerr-0なので、そのまま）
                err -= 1f;
            }

            //誤差を振り分ける
            errors[0][x + 1] += err * 7f / 16f;
            if (x > 0)
            {
                errors[1][x - 1] += err * 3f / 16f;
            }
            errors[1][x] += err * 5f / 16f;
            errors[1][x + 1] += err * 1f / 16f;
        }
        //誤差を記憶した配列を入れ替える
        errors[0] = errors[1];
        errors[1] = new float[errors[0].Length];
    }
    //作成したピクセルデータをコピーする
    IntPtr ptr = bmpDate.Scan0;
    System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length);

    //ロックを解除する
    newImg.UnlockBits(bmpDate);

    return newImg;
}