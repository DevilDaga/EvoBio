using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
namespace Speed_Test
{
	internal class Program
	{
		static int startingQuant = 250;
		static int T = 100;
		static int U = 1000;

		private static TimeSpan TestVersion0 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_0.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 0.1,
				K = 0.1,
				L = 0.1,
				M = 0.1,
				N = 0.1,
				O = 0.1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_0.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );
			
			Console.WriteLine ( $"Version 0 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static TimeSpan TestVersion1 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_1.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 1,
				K = 1,
				L = 1,
				M = 1,
				N = 1,
				O = 1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_1.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );

			Console.WriteLine ( $"Version 1 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static TimeSpan TestVersion2 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_2.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 1,
				K = 1,
				L = 1,
				M = 1,
				N = 1,
				O = 1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_2.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );

			Console.WriteLine ( $"Version 2 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static TimeSpan TestVersion3 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_3.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 1,
				K = 1,
				L = 1,
				M = 1,
				N = 1,
				O = 1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_3.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );

			Console.WriteLine ( $"Version 3 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static TimeSpan TestVersion4 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_4.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 0.1,
				K = 0.1,
				L = 0.1,
				M = 0.1,
				N = 0.1,
				O = 0.1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_4.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );

			Console.WriteLine ( $"Version 4 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static TimeSpan TestVersion5 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_5.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 0.1,
				K = 0.1,
				L = 0.1,
				M = 0.1,
				N = 0.1,
				O = 0.1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_5.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );

			Console.WriteLine ( $"Version 5 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static TimeSpan TestVersion6 ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var v = new EvoBio_Version_6.Variables
			{
				A = startingQuant,
				B = startingQuant,
				C = startingQuant,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = 0.1,
				K = 0.1,
				L = 0.1,
				M = 0.1,
				N = 0.1,
				O = 0.1,
				P = 25,
				Q = 0.1,
				R = 10,
				S = 0.1,
				T = T,
				U = U,
				Y = 1
			};

			var pop = new EvoBio_Version_6.Population { V = v.Clone ( ) };

			pop.Iterate ( null );

			stopwatch.Stop ( );

			Console.WriteLine ( $"Version 6 -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );

			return stopwatch.Elapsed;
		}

		private static void TestAll ( )
		{
			Stopwatch stopwatch = new Stopwatch ( );
			stopwatch.Start ( );

			var results = new Dictionary<string, TimeSpan>
			{
				["Version 0"] = TestVersion0 ( ),
				["Version 1"] = TestVersion1 ( ),
				["Version 2"] = TestVersion2 ( ),
				["Version 3"] = TestVersion3 ( ),
				["Version 4"] = TestVersion4 ( ),
				["Version 5"] = TestVersion5 ( ),
				["Version 6"] = TestVersion6 ( )
			};

			stopwatch.Stop ( );
			Console.WriteLine ( $"Total -> {stopwatch.Elapsed.Hours}:{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds}" );
		}

		private static void Main ( string[] args )
		{
			TestAll ( );
			Console.ReadLine ( );
		}
	}
}