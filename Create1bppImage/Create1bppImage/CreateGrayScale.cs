using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;

namespace Create1bppImage
{
    public class CreateGrayScale
    {
        public CreateGrayScale()
        {}
        /// <summary>
        /// 指定した画像からグレースケール画像を作成する
        /// </summary>
        /// <param name="img">基の画像</param>
        /// <returns>作成されたグレースケール画像</returns>
        public static Image CreateGrayscaleImage(Image img)
        {
            //グレースケールの描画先となるImageオブジェクトを作成
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            //newImgのGraphicsオブジェクトを取得
            Graphics g = Graphics.FromImage(newImg);

            //ColorMatrixオブジェクトの作成
            //グレースケールに変換するための行列を指定する
            System.DrawingCore.Imaging.ColorMatrix cm =
                  new System.DrawingCore.Imaging.ColorMatrix(
                new float[][]{
            new float[]{0.3086f, 0.3086f, 0.3086f, 0 ,0},
            new float[]{0.6094f, 0.6094f, 0.6094f, 0, 0},
            new float[]{0.0820f, 0.0820f, 0.0820f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
                });
            //ImageAttributesオブジェクトの作成
            System.DrawingCore.Imaging.ImageAttributes ia =
                  new System.DrawingCore.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用してグレースケールを描画
            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            //リソースを解放する
            g.Dispose();

            return newImg;
        }
    }
}
