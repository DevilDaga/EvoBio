using MathNet.Numerics.Distributions;
using System;

namespace EvoBio_Version_4
{
	public static class Utility
	{
		public static Random rand = new Random ( );

		public static double NextDouble => rand.NextDouble ( );

		public static decimal NextDecimal => (decimal) NextDouble;

		//public static double NextGaussian()
		//{
		//	var u1 = rand.NextDouble();
		//	var u2 = rand.NextDouble();

		//	var randStdNormal =
		//		Math.Sqrt(-2.0 * Math.Log(u1)) *
		//		Math.Sin(2.0 * Math.PI * u2);

		//	return randStdNormal;
		//}

		//public static double NextGaussian ( )
		//{
		//	double s, u1, u2;
		//	do
		//	{
		//		u1 = 2.0 * NextDouble - 1.0;
		//		u2 = 2.0 * NextDouble - 1.0;
		//		s = u1 * u1 + u2 * u2;
		//	} while ( s >= 1.0 || s == 0 );

		//	double randStdNormal = u1 * Math.Sqrt ( -2.0 * Math.Log ( s ) / s );

		//	return randStdNormal;
		//}

		public static double NextGaussian ( double mean, double sd )
		{
			var normalDist = new Normal ( mean, sd, rand );
			double randomGaussianValue = normalDist.Sample ( );
			return randomGaussianValue;
		}

		public static double[] NextGaussianSymbols ( double mean, double sd, int n )
		{
			var normalDist = new Normal ( mean, sd, rand );
			double[] samples = new double[n];
			normalDist.Samples ( samples );
			return samples;
		}

		//public static double NextGaussian ( double mean, double sd ) => Math.Max ( 0, mean + sd * NextGaussian ( ) );

		public static double NextGaussian ( double mean, double sd, int precision )
		{
			double randNormal = NextGaussian ( mean, sd );

			return Math.Round ( randNormal, precision );
		}
	}
}