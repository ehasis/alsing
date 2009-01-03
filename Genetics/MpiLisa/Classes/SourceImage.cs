using System.Drawing;

namespace GenArt.Core.Classes
{
	// 2008-12-14 DanByström: added class
	internal struct Pixel
	{
		internal byte B;
		internal byte G;
		internal byte R;
		internal byte A;

		internal Pixel( Color c )
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

        public static implicit operator Pixel(Color rhs)
		{
			return new Pixel( rhs );
		}

	}

	// 2008-12-14 DanByström:
	//  removed Color[,] Colors
	//  added Pixels[]
	//  added GetPixel( int x, int y );
	//  added SetPixel( int x, int y, Color c )
	internal class SourceImage
	{
		internal Pixel[] Pixels { get; set; }
		internal int Width { get; set; }
		internal int Height { get; set; }

		internal Pixel GetPixel( int x, int y )
		{
			return Pixels[y * Width + x];
		}

		internal void SetPixel( int x, int y, Color c )
		{
			Pixels[y * Width + x] = c;
		}

	}

}
