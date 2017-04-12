namespace EvoBio
{
	public class MutantIndividual : Individual
	{
		public override string ToString ( )
		{
			return "Mutant " + base.ToString ( );
		}
	}
}