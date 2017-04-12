using System;

namespace EvoBio
{
	internal class Program
	{
		private static void Main ( string[] args )
		{
			double J_O = .8;
			int individualCount = 10;
			Variables v = new Variables
			{
				A = individualCount,
				B = individualCount,
				C = individualCount,
				D = 10,
				E = 10,
				F = 10,
				G = 10,
				H = 10,
				I = 10,
				J = J_O,
				K = J_O,
				L = J_O,
				M = J_O,
				N = J_O,
				O = J_O,
				P = 25,
				Q = .1,
				R = .1,
				S = 1,
				T = 250,
				U = 1000,
				Y = 1
			};

			Population model = new Population
			{
				V = v
			};
			//model.Output = new StreamWriter ( "output.txt" );

			//Task task = new Task ( model.Iterate );
			//task.Start ( );
			//task.Wait ( );
			for ( double r = .1; r <= 1; r += .1 )
			{
				model.V.R = r;
				model.Iterate ( );

				Console.WriteLine ( $"R={Math.Round ( r, 1 )} {model}" );
			}

			Console.ReadLine ( );
		}
	}
}