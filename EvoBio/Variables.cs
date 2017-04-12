using System.ComponentModel;

namespace EvoBio
{
	public class Variables
	{
		[Description ( "Starting quantity of wild individuals" )]
		public int A { get; set; }

		[Description ( "Starting quantity of mutant individuals" )]
		public int B { get; set; }

		[Description ( "Starting quantity of amp individuals" )]
		public int C { get; set; }

		[Description ( "Starting mean rep ability for wild individuals" )]
		public double D { get; set; }

		[Description ( "Starting mean lethal ability for wild individuals" )]
		public double E { get; set; }

		[Description ( "Starting mean rep ability for mutant individuals" )]
		public double F { get; set; }

		[Description ( "Starting mean lethal ability for mutant individuals" )]
		public double G { get; set; }

		[Description ( "Starting mean rep ability for amp individuals" )]
		public double H { get; set; }

		[Description ( "Starting mean lethal ability for amp individuals" )]
		public double I { get; set; }

		[Description ( "Coefficient of variation of rep ability for wild individuals" )]
		public double J { get; set; }

		[Description ( "Coefficient of variation of lethal ability for wild individuals" )]
		public double K { get; set; }

		[Description ( "Coefficient of variation of rep ability for mutant individuals" )]
		public double L { get; set; }

		[Description ( "Coefficient of variation of lethal ability for mutant individuals" )]
		public double M { get; set; }

		[Description ( "Coefficient of variation for rep ability for amp individuals" )]
		public double N { get; set; }

		[Description ( "Coefficient of variation for lethal ability for amp individuals" )]
		public double O { get; set; }

		[Description ( "Predator mean" )]
		public double P { get; set; }

		[Description ( "Predator coefficient of variation" )]
		public double Q { get; set; }

		[Description ( "Bias in allocation" )]
		public double R { get; set; }

		[Description ( "Implementation Failure" )]
		public double S { get; set; }

		[Description ( "Generations" )]
		public int T { get; set; }

		[Description ( "Iterations" )]
		public int U { get; set; }

		[Description ( "Percentage weighting" )]
		public double Y { get; set; }

		public Variables Clone ( )
		{
			return (Variables) MemberwiseClone ( );
		}

		public override string ToString ( )
		{
			return
				$"A={A}, " +
				$"B={B}, " +
				$"C={C}" + "\n" +
				$"D={D}, " +
				$"E={E}, " +
				$"F={F}, " +
				$"G={G}, " +
				$"H={H}, " +
				$"I={I}" + "\n" +
				$"J={J}, " +
				$"K={K}, " +
				$"L={L}, " +
				$"M={M}, " +
				$"N={N}, " +
				$"O={O}" + "\n" +
				$"P={P}, " +
				$"Q={Q}, " +
				$"R={R}, " +
				$"S={S}" + "\n" +
				$"T={T}, " +
				$"U={U}, " +
				$"Y={Y}";
		}
	}
}