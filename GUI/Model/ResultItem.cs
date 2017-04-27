using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvoBio_Common;

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

		private void FillValues ( VariablesBase v, Dictionary<string, VariableItem> variableMap )
		{
			v.A = (int) variableMap["A"].Value;
			v.B = (int) variableMap["B"].Value;
			v.C = (int) variableMap["C"].Value;
			v.D = (double) variableMap["D"].Value;
			v.E = (double) variableMap["E"].Value;
			v.F = (double) variableMap["F"].Value;
			v.G = (double) variableMap["G"].Value;
			v.H = (double) variableMap["H"].Value;
			v.I = (double) variableMap["I"].Value;
			v.J = (double) variableMap["J"].Value;
			v.K = (double) variableMap["K"].Value;
			v.L = (double) variableMap["L"].Value;
			v.M = (double) variableMap["M"].Value;
			v.N = (double) variableMap["N"].Value;
			v.O = (double) variableMap["O"].Value;
			v.P = (double) variableMap["P"].Value;
			v.Q = (double) variableMap["Q"].Value;
			v.R = (double) variableMap["R"].Value;
			v.S = (double) variableMap["S"].Value;
			v.T = (int) variableMap["T"].Value;
			v.U = (int) variableMap["U"].Value;
			v.Y = (double) variableMap["Y"].Value;
		}

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
				var v = new EvoBio_Version_0.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_0.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 1" )
			{
				var v = new EvoBio_Version_1.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_1.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 2" )
			{
				var v = new EvoBio_Version_2.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_2.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 3" )
			{
				var v = new EvoBio_Version_3.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_3.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 4" )
			{
				var v = new EvoBio_Version_4.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_4.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 5" )
			{
				var v = new EvoBio_Version_5.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_5.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
			else if ( version == "Version 6" )
			{
				var v = new EvoBio_Version_6.Variables ( );
				FillValues ( v, variableMap );
				var population = new EvoBio_Version_6.Population { V = v.Clone ( ) };
				await population.IterateAsync ( progress );
				WildWins = population.Highest.Wild;
				MutantWins = population.Highest.Mutant;
				AmpWins = population.Highest.Amp;
			}
		}

		public override string ToString ( ) => $"Wild={WildPercentage} Mutant={MutantPercentage} Amp={AmpPercentage}";
	}
}