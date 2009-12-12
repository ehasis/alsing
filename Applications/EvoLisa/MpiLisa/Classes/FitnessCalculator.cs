using System.Drawing;
using System.Drawing.Imaging;
using GenArt.AST;
using GenArt.Core.Classes;

namespace GenArt.Classes
{
    internal class FitnessCalculator
    {
        private static Bitmap bmp;
        private static Graphics g;
        internal static long GetDrawingFitness(DnaDrawing newDrawing, SourceImage sourceImage, Bitmap bgImage, Bitmap fgImage)
        {
            
            long error = 0;

            if (bmp == null)
            {
                bmp = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);
                g = Graphics.FromImage(bmp);
            }

            if (bgImage != null)
                g.DrawImage(bgImage,0,0);
            else
                g.Clear(Color.Black);


            Renderer.Render(newDrawing, g, 1);

            if (fgImage != null)
                g.DrawImage(fgImage,0,0);

            BitmapData bd = bmp.LockBits(
                new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            unchecked
            {
                unsafe
                {
                    fixed (Pixel* psourcePixels = sourceImage.Pixels)
                    {
                        int partitionEnd = sourceImage.Height*sourceImage.Width;
                        var p1 = (Pixel*) bd.Scan0.ToPointer();
                        Pixel* p2 = psourcePixels;

                        for (int i = 0; i < partitionEnd; i++, p1++, p2++)
                        {
                            int R = p1->R - p2->R;
                            int G = p1->G - p2->G;
                            int B = p1->B - p2->B;
                            error += R * R + G * G + B * B;
                        }
                    }
                }
            }

            bmp.UnlockBits(bd);


            // error += newDrawing.Polygons.Count * 3 ;
            return error;
        }
    }
}