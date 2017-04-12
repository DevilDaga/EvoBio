using EvoBio;
using System.Threading.Tasks;

namespace GUI.Model
{
	public class ResultItem
	{
		private Population population;

		public Variables AllVariables { get; set; }

		public VariableItem RangedVariable { get; set; }

		public int TotalIndividuals => WildWins + MutantWins + AmpWins;

		public int WildWins => population?.Highest.Wild ?? 0;

		public int MutantWins => population?.Highest.Mutant ?? 0;

		public int AmpWins => population?.Highest.Amp ?? 0;

		public double WildPercentage => 1.0 * WildWins / TotalIndividuals;

		public double MutantPercentage => 1.0 * MutantWins / TotalIndividuals;

		public double AmpPercentage => 1.0 * AmpWins / TotalIndividuals;

		public string RangedVariableName => RangedVariable.Name;

		public double RangedVariableValue => (double) RangedVariable.Value;

		public ResultItem Run ( )
		{
			population = new Population { V = AllVariables.Clone ( ) };
			population.Iterate ( );

			return this;
		}

		public async Task RunAsync ( )
		{
			population = new Population { V = AllVariables.Clone ( ) };
			//population.Output = new StreamWriter ( "steps.txt" );
			await population.IterateAsync ( );
		}

		public override string ToString ( ) => $"Wild={WildPercentage} Mutant={MutantPercentage} Amp={AmpPercentage}";
	}
}