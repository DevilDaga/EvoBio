using System;
using System.IO;

namespace EvoBio_Version_2
{
	public class AmpIndividual : Individual
	{
		public void Adjust ( )
		{
			if ( rep > 10.5 )
			{
				rep -= 0;
			}
			else if ( rep > 9.5 )
			{
				rep -= .5;
			}
			else if ( rep > 8.5 )
			{
				rep -= 2;
			}
			else
			{
				rep -= 4;
			}

			rep = Math.Max ( 0, rep );
		}

		public void AdjustOutput ( TextWriter Output = null )
		{
			double decRep = 0;
			if ( rep > 10.5 )
			{
				rep -= 0;
				decRep = 0;
				Output?.WriteLine ( $"rep = {originalRep} > 10.5" );
			}
			else if ( rep > 9.5 )
			{
				rep -= .5;
				decRep = .5;
				Output?.WriteLine ( $"rep = {originalRep} > 9.5 AND <= 10.5" );
			}
			else if ( rep > 8.5 )
			{
				rep -= 2;
				decRep = 2;
				Output?.WriteLine ( $"rep = {originalRep} > 8.5 AND <= 9.5" );
			}
			else
			{
				rep -= 4;
				decRep = 4;
				Output?.WriteLine ( $"rep = {originalRep} <= 8.5" );
			}

			rep = Math.Max ( 0, rep );

			Output?.WriteLine ( $"     rep = rep - {decRep} = {rep}" );
		}

		public override string ToString ( )
		{
			return "Amp " + base.ToString ( );
		}
	}
}