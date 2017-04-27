using System.Collections.Generic;
using EvoBio_Common;

namespace EvoBio_Version_6
{
	public class Variables : VariablesBase
	{
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
			["J"] = ("Coefficient of variation of rep ability for wild individuals", 0.1m),
			["K"] = ("Coefficient of variation of lethal ability for wild individuals", 0.1m),
			["L"] = ("Coefficient of variation of rep ability for mutant individuals", 0.1m),
			["M"] = ("Coefficient of variation of lethal ability for mutant individuals", 0.1m),
			["N"] = ("Coefficient of variation for rep ability for amp individuals", 0.1m),
			["O"] = ("Coefficient of variation for lethal ability for amp individuals", 0.1m),
			["P"] = ("Predator mean", 25),
			["Q"] = ("Predator coefficient of variation", 0.1m),
			["R"] = ("Bias in allocation", 0.1m),
			["S"] = ("Implementation Failure", 1),
			["T"] = ("Generations", 100),
			["U"] = ("Iterations", 1000),
			["Y"] = ("Percentage weighting", 1)
		};

		public new Variables Clone ( )
		{
			return (Variables) MemberwiseClone ( );
		}
	}
}