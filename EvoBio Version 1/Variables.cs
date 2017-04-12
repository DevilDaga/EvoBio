using System.Collections.Generic;
using System.ComponentModel;

namespace EvoBio_Version_1
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

		[Description ( "Standard Deviation of rep ability for wild individuals" )]
		public double J { get; set; }

		[Description ( "Standard Deviation of lethal ability for wild individuals" )]
		public double K { get; set; }

		[Description ( "Standard Deviation of rep ability for mutant individuals" )]
		public double L { get; set; }

		[Description ( "Standard Deviation of lethal ability for mutant individuals" )]
		public double M { get; set; }

		[Description ( "Standard Deviation for rep ability for amp individuals" )]
		public double N { get; set; }

		[Description ( "Standard Deviation for lethal ability for amp individuals" )]
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

		public static readonly Dictionary<string, (string description, decimal val)> Default = new Dictionary<string, (string, decimal)>
		{
			["A"] = ("Starting quantity of wild individuals", 10),
			["B"] = ("Starting quantity of mutant individuals", 10),
			["C"] = ("Starting quantity of amp individuals", 10),
			["D"] = ("Starting mean rep ability for wild individuals", 10),
			["E"] = ("Starting mean lethal ability for wild individuals", 10),
			["F"] = ("Starting mean rep ability for mutant individuals", 10),
			["G"] = ("Starting mean lethal ability for mutant individuals", 10),
			["H"] = ("Starting mean rep ability for amp individuals", 10),
			["I"] = ("Starting mean lethal ability for amp individuals", 10),
			["J"] = ("Standard Deviation of rep ability for wild individuals", 1m),
			["K"] = ("Standard Deviation of lethal ability for wild individuals", 1m),
			["L"] = ("Standard Deviation of rep ability for mutant individuals", 1m),
			["M"] = ("Standard Deviation of lethal ability for mutant individuals", 1m),
			["N"] = ("Standard Deviation for rep ability for amp individuals", 1m),
			["O"] = ("Standard Deviation for lethal ability for amp individuals", 1m),
			["P"] = ("Predator mean", 25),
			["Q"] = ("Predator coefficient of variation", 0.1m),
			["R"] = ("Bias in allocation", 0.1m),
			["S"] = ("Implementation Failure", 1),
			["T"] = ("Generations", 100),
			["U"] = ("Iterations", 1000),
			["Y"] = ("Percentage weighting", 1)
		};

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