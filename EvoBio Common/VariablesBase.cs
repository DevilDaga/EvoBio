namespace EvoBio_Common
{
	public abstract class VariablesBase
	{

		public int A { get; set; }

		public int B { get; set; }

		public int C { get; set; }

		public double D { get; set; }

		public double E { get; set; }

		public double F { get; set; }

		public double G { get; set; }

		public double H { get; set; }

		public double I { get; set; }

		public double J { get; set; }

		public double K { get; set; }

		public double L { get; set; }

		public double M { get; set; }

		public double N { get; set; }

		public double O { get; set; }

		public double P { get; set; }

		public double Q { get; set; }

		public double R { get; set; }

		public double S { get; set; }

		public int T { get; set; }

		public int U { get; set; }

		public double Y { get; set; }

		public VariablesBase Clone ( )
		{
			return (VariablesBase) MemberwiseClone ( );
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