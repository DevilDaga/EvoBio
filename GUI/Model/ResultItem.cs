using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Model
{
	public class ResultItem
	{

		public int TotalIndividuals => WildWins + MutantWins + AmpWins;

		public int WildWins;

		public int MutantWins;

		public int AmpWins;

		public double WildPercentage => 1.0 * WildWins / TotalIndividuals;

		public double MutantPercentage => 1.0 * MutantWins / TotalIndividuals;

		public double AmpPercentage => 1.0 * AmpWins / TotalIndividuals;

		public string RangedVariableName;

		public double RangedVariableValue;

		public async Task RunAsync (
			Dictionary<string, VariableItem> variableMap,
			string rangedVariableName,
			string version,
			IProgress<decimal> progress
			)
		{
			RangedVariableName = rangedVariableName;
			RangedVariableValue = (double) variableMap[rangedVariableName].Value;

			if ( version == "Version 0" )
			{
				var v = new EvoBio_Version_0.Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var population = new EvoBio_Version_0.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 1" )
			{
				var v = new EvoBio_Version_1.Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var population = new EvoBio_Version_1.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 2" )
			{
				var v = new EvoBio_Version_2.Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var population = new EvoBio_Version_2.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 3" )
			{
				var v = new EvoBio_Version_3.Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var population = new EvoBio_Version_3.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 4" )
			{
				var v = new EvoBio_Version_4.Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var population = new EvoBio_Version_4.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 5" )
			{
				var v = new EvoBio_Version_5.Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var population = new EvoBio_Version_5.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
		}

		//public ResultItem Run ( IProgress<decimal> progress )
		//{
		//	population = new Population { V = AllVariables.Clone ( ) };
		//	population.Iterate ( progress );

		//	return this;
		//}

		//public async Task RunAsync ( IProgress<decimal> progress, string version = "Version 0" )
		//{
		//	population = new Population { V = AllVariables.Clone ( ) };
		//	//population.Output = new StreamWriter ( "steps.txt" );
		//	await population.IterateAsync ( progress );
		//}

		public override string ToString ( ) => $"Wild={WildPercentage} Mutant={MutantPercentage} Amp={AmpPercentage}";
	}
}