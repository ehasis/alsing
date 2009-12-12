using System.Drawing;

namespace GenArt.Core.Classes
{
	// 2008-12-14 DanByström: added class
	public struct Pixel
	{
		public byte B;
		public byte G;
		public byte R;
		public byte A;

		public Pixel( Color c )
		{
			B = c.B;
			G = c.G;
			R = c.R;
			A = c.A;
		}

		public static implicit operator Color( Pixel rhs )
		{
			return Color.FromArgb( rhs.R, rhs.G, rhs.B );
		}

		public static implicit operator Pixel( Color rhs )
		{
			return new Pixel( rhs );
		}

	}

	// 2008-12-14 DanByström:
	//  removed Color[,] Colors
	//  added Pixels[]
	//  added GetPixel( int x, int y );
	//  added SetPixel( int x, int y, Color c )
	public class SourceImage
	{
		public Pixel[] Pixels { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Pixel GetPixel( int x, int y )
		{
			return Pixels[y * Width + x];
		}

		public void SetPixel( int x, int y, Color c )
		{
			Pixels[y * Width + x] = c;
		}

	}

}
