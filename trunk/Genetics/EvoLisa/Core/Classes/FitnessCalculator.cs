using System.Drawing;
using System.Drawing.Imaging;
using GenArt.AST;
using GenArt.Core.Classes;

namespace GenArt.Classes
{
    public class FitnessCalculator
    {
        public static double GetDrawingFitness(DnaDrawing newDrawing, SourceImage sourceImage,int partitionY,int partitionHeight)
        {
            double error = 0;

            using (var bmp = new Bitmap(sourceImage.Width, sourceImage.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SetClip(new Rectangle(0,partitionY,sourceImage.Width,partitionHeight));
                Renderer.Render(newDrawing, g, 1);
                g.ResetClip();

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
                            int partitionOffset = partitionY * sourceImage.Width;
                            int partitionEnd = partitionOffset + partitionHeight * sourceImage.Width;
                            var p1 = (Pixel*)bd.Scan0.ToPointer() + partitionOffset;
                            Pixel* p2 = psourcePixels + partitionOffset;
                            for (int i = partitionOffset; i < partitionEnd; i++, p1++, p2++)
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
            }

            // error += newDrawing.Polygons.Count * 3 ;
            return error;
        }


        // 2008-12-14 DanByström: method optimized for speed
        public static double GetDrawingFitness(DnaDrawing newDrawing, SourceImage sourceImage)
        {
            double error = 0;

            using (var bmp = new Bitmap(sourceImage.Width, sourceImage.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Renderer.Render(newDrawing, g, 1);

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
                            var p1 = (Pixel*) bd.Scan0.ToPointer();
                            Pixel* p2 = psourcePixels;
                            for (int i = sourceImage.Pixels.Length; i > 0; i--, p1++, p2++)
                            {
                                int R = p1->R - p2->R;
                                int G = p1->G - p2->G;
                                int B = p1->B - p2->B;
                                error += R*R + G*G + B*B;
                            }
                        }
                    }
                }

                bmp.UnlockBits(bd);
            }

           // error += newDrawing.Polygons.Count * 3 ;
            return error;
        }
    }
}