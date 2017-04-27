namespace EvoBio_Common
{
	public class MutantIndividual : Individual
	{
		public override string ToString ( )
		{
			return "Mutant " + base.ToString ( );
		}
	}
}