using System;
using System.Linq;
using EvoBio_Common;

namespace EvoBio_Version_1
{
	public class Population : PopulationBase
	{
		protected override void Step3 ( )
		{
			AmpIndividuals = AmpIndividuals.OrderBy ( x => x.fitness ).ToList ( );
			WildIndividuals = WildIndividuals.OrderBy ( x => x.fitness ).ToList ( );

			{
				var cutOff0_20 = (int) ( AmpIndividuals.Count * .2 );
				var cutOff20_50 = (int) ( AmpIndividuals.Count * .5 );
				var cutOff50_80 = (int) ( AmpIndividuals.Count * .8 );

				for ( var i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					if ( i < cutOff0_20 )
						ind.rep = 0;
					else if ( i < cutOff20_50 )
						ind.rep *= .75;
					else if ( i < cutOff50_80 )
						ind.rep *= .8;
				}
			}

			{
				var cutOff0_20 = (int) ( WildIndividuals.Count * .2 );
				var cutOff20_50 = (int) ( WildIndividuals.Count * .5 );
				var cutOff50_80 = (int) ( WildIndividuals.Count * .8 );

				for ( var i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					if ( i < cutOff0_20 )
					{
						ind.rep = 0;
						//ind.lethal = 0;
					}
					else if ( i < cutOff20_50 )
					{
						ind.rep *= .75;
						//ind.lethal = 0;
					}
					else if ( i < cutOff50_80 )
					{
						ind.rep *= .8;
						//ind.lethal = 0;
					}
				}
			}

			foreach ( var ind in AllIndividualsEnumerable )
			{
				ind.rep = Math.Max ( 0, ind.rep );
				ind.lethal = Math.Max ( 0, ind.lethal );
			}
		}

		protected override void Step3Output ( )
		{
			Output?.WriteLine ( "Step 3" );

			AmpIndividuals = AmpIndividuals.OrderBy ( x => x.fitness ).ToList ( );
			WildIndividuals = WildIndividuals.OrderBy ( x => x.fitness ).ToList ( );

			{
				var cutOff0_20 = (int) ( AmpIndividuals.Count * .2 );
				var cutOff20_50 = (int) ( AmpIndividuals.Count * .5 );
				var cutOff50_80 = (int) ( AmpIndividuals.Count * .8 );

				for ( var i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					double oldRep = ind.rep, oldLethal = ind.lethal;

					if ( i < cutOff0_20 )
						ind.rep = 0;
					else if ( i < cutOff20_50 )
						ind.rep *= .75;
					else if ( i < cutOff50_80 )
						ind.rep *= .8;

					Output?.WriteLine ( $"Amp_{i + 1} rep={oldRep} -> {ind.rep} lethal={oldLethal} -> {ind.lethal}" );
				}
			}

			{
				var cutOff0_20 = (int) ( WildIndividuals.Count * .2 );
				var cutOff20_50 = (int) ( WildIndividuals.Count * .5 );
				var cutOff50_80 = (int) ( WildIndividuals.Count * .8 );

				for ( var i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					double oldRep = ind.rep, oldLethal = ind.lethal;

					if ( i < cutOff0_20 )
					{
						ind.rep = 0;
						//ind.lethal = 0;
					}
					else if ( i < cutOff20_50 )
					{
						ind.rep *= .75;
						//ind.lethal = 0;
					}
					else if ( i < cutOff50_80 )
					{
						ind.rep *= .8;
						//ind.lethal = 0;
					}

					Output?.WriteLine ( $"Wild_{i + 1} rep={oldRep} -> {ind.rep} lethal={oldLethal} -> {ind.lethal}" );
				}
			}

			Output?.WriteLine ( "\n\n" );

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #3" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} rep={WildIndividuals[i].rep} lethal={WildIndividuals[i].lethal}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} rep={AmpIndividuals[i].rep} lethal={AmpIndividuals[i].lethal}" );
			//	Output.WriteLine ( );
			//}
		}

		protected override void Step8 ( )
		{
			foreach ( var ind in AllIndividualsEnumerable )
				if ( double.IsNaN ( ind.rep ) )
					isErrored = true;
		}

		protected override void Step8Output ( )
		{
			Output?.WriteLine ( $"Step #8" );

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #8" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} rep={WildIndividuals[i].rep} lethal={WildIndividuals[i].lethal}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} rep={MutantIndividuals[i].rep} lethal={MutantIndividuals[i].lethal}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} rep={AmpIndividuals[i].rep} lethal={AmpIndividuals[i].lethal}" );
			//	Output.WriteLine ( );
			//	Output.WriteLine ( $"average rep = {avgRep}" );
			//	Output.WriteLine ( $"average lethal = {avgLethal}" );
			//	Output.WriteLine ( $"average rep start = {avgStartRep}" );
			//	Output.WriteLine ( $"average lethal start = {avgStartLethal}" );
			//	Output.WriteLine ( );
			//}
		}
	}
}