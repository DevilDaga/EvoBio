using System;
using System.Linq;
using EvoBio_Common;

namespace EvoBio_Version_6
{
	public class Population : PopulationBase
	{
		protected override void Step3 ( )
		{
			//foreach ( var wild in WildIndividuals )
			//	wild.Adjust ( );

			AmpIndividuals = AmpIndividuals.OrderBy ( x => x.fitness ).ToList ( );
			WildIndividuals = WildIndividuals.OrderBy ( x => x.fitness ).ToList ( );

			{
				var cutOff0_20 = AllIndividualsEnumerable.OrderBy ( x => x.fitness ).ElementAt ( (int) ( TotalIndividuals * .2 ) ).fitness;
				var cutOff20_50 = AllIndividualsEnumerable.OrderBy ( x => x.fitness ).ElementAt ( (int) ( TotalIndividuals * .5 ) ).fitness;
				var cutOff50_80 = AllIndividualsEnumerable.OrderBy ( x => x.fitness ).ElementAt ( (int) ( TotalIndividuals * .8 ) ).fitness;

				foreach ( var ind in AmpIndividuals )
				{
					if ( ind.fitness < cutOff0_20 )
						ind.rep = 0;
					else if ( ind.fitness < cutOff20_50 )
						ind.rep *= .75;
					else if ( ind.fitness < cutOff50_80 )
						ind.rep *= .8;
				}
			}

			{
				var cutOff0_20 = AllIndividualsEnumerable.OrderBy ( x => x.fitness ).ElementAt ( (int) ( TotalIndividuals * .2 ) ).fitness;
				var cutOff20_50 = AllIndividualsEnumerable.OrderBy ( x => x.fitness ).ElementAt ( (int) ( TotalIndividuals * .5 ) ).fitness;
				var cutOff50_80 = AllIndividualsEnumerable.OrderBy ( x => x.fitness ).ElementAt ( (int) ( TotalIndividuals * .8 ) ).fitness;

				foreach ( var ind in WildIndividuals )
				{
					if ( ind.fitness < cutOff0_20 )
					{
						ind.rep = 0;
						//ind.lethal = 0;
					}
					else if ( ind.fitness < cutOff20_50 )
					{
						ind.rep *= .75;
						//ind.lethal = 0;
					}
					else if ( ind.fitness < cutOff50_80 )
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

			int i = 1;
			foreach ( var wild in WildIndividuals )
			{
				Output?.Write ( $"Wild_{i++} " );
				wild.AdjustOutput ( Output );
			}
			i = 1;
			foreach ( var amp in AmpIndividuals )
			{
				Output?.Write ( $"Amp_{i++} " );
				amp.AdjustOutput ( Output );
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
			var avgRep = AllIndividualsEnumerable.Average ( x => x.rep );
			var avgLethal = AllIndividualsEnumerable.Average ( x => x.lethal );
			var avgStartRep = ( V.A * V.D + V.B * V.F + V.C * V.H ) / TotalIndividuals;
			var avgStartLethal = ( V.A * V.E + V.B * V.G + V.C * V.I ) / TotalIndividuals;

			foreach ( var ind in AllIndividualsEnumerable )
			{
				ind.rep *= avgStartRep / avgRep;
				ind.lethal *= avgStartLethal / avgLethal;

				if ( double.IsNaN ( ind.rep ) )
					isErrored = true;
			}
		}

		protected override void Step8Output ( )
		{
			Output?.WriteLine ( $"Step #8" );

			double avgRep = AllIndividualsEnumerable.Average ( x => x.rep );
			double avgLethal = AllIndividualsEnumerable.Average ( x => x.lethal );
			double avgStartRep = ( V.A * V.D + V.B * V.F + V.C * V.H ) / TotalIndividuals;
			double avgStartLethal = ( V.A * V.E + V.B * V.G + V.C * V.I ) / TotalIndividuals;

			foreach ( var ind in AllIndividualsEnumerable )
			{
				ind.rep *= avgStartRep / avgRep;
				ind.lethal *= avgStartLethal / avgLethal;

				if ( double.IsNaN ( ind.rep ) )
					isErrored = true;
			}

			if ( Output != null )
			{
				Output.WriteLine ( $"average rep = {avgRep}" );
				Output.WriteLine ( $"average lethal = {avgLethal}" );
				Output.WriteLine ( $"average rep start = {avgStartRep}" );
				Output.WriteLine ( $"average lethal start = {avgStartLethal}" );
				Output.WriteLine ( );
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					Output.WriteLine ( $"Wild_{i + 1} rep={ind.rep} lethal={ind.lethal}" );
				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Mutant_{i + 1} rep={ind.rep} lethal={ind.lethal}" );
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					Output.WriteLine ( $"Amp_{i + 1} rep={ind.rep} lethal={ind.lethal}" );
				}
				Output.WriteLine ( "\n\n" );
			}

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