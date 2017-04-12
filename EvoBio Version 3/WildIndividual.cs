using System;
using System.IO;

namespace EvoBio_Version_3
{
	public class WildIndividual : Individual
	{
		public void Adjust ( )
		{
			if ( rep > 10.5 )
			{
				lethal -= 2.5;
			}
			else if ( rep > 9.5 )
			{
				rep -= .5;
				lethal -= 2;
			}
			else if ( rep > 8.5 )
			{
				rep -= 2;
				lethal -= 1;
			}
			else
			{
				rep -= 4;
			}

			rep = Math.Max ( 0, rep );
			lethal = Math.Max ( 0, lethal );
		}

		public void AdjustOutput ( TextWriter Output = null )
		{
			double decLethal = 0, decRep = 0;
			if ( rep > 10.5 )
			{
				decLethal = 2.5;
				lethal -= 2.5;
				Output?.WriteLine ( $"rep = {originalRep} > 10.5" );
			}
			else if ( rep > 9.5 )
			{
				rep -= .5;
				lethal -= 2;
				decLethal = 2;
				decRep = .5;
				Output?.WriteLine ( $"rep = {originalRep} > 9.5 AND <= 10.5" );
			}
			else if ( rep > 8.5 )
			{
				rep -= 2;
				lethal -= 1;
				decLethal = 1;
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
			lethal = Math.Max ( 0, lethal );

			Output?.WriteLine ( $"     lethal = lethal - {decLethal} = {lethal}, rep = rep - {decRep} = {rep}" );
		}

		public override string ToString ( )
		{
			return "Wild " + base.ToString ( );
		}
	}
}