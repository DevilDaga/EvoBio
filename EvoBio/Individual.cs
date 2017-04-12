namespace EvoBio_Version_0
{
	public abstract class Individual
	{
		public double rep, lethal, fitness, tempFitness, allocation;
		public double originalRep, originalLethal;

		public double Lost => fitness - tempFitness;

		public override string ToString ( )
		{
			return $"temp={tempFitness} rep={rep} lethal={lethal} fitness={fitness} allocation={allocation}";
		}
	}
}