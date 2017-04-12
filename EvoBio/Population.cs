using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EvoBio
{
	public class Population
	{
		#region Globals

		private List<WildIndividual> WildIndividuals = new List<WildIndividual> ( );
		private List<MutantIndividual> MutantIndividuals = new List<MutantIndividual> ( );
		private List<AmpIndividual> AmpIndividuals = new List<AmpIndividual> ( );

		private double PredatorPercent;

		private double WildTempTotal;
		private double MutantTempTotal;
		private double AmpTempTotal;
		private double WildLost;
		private double MutantLost;
		private double AmpLost;

		private double TotalAllocation;

		private bool isErrored;

		private HighestValue highest = new HighestValue ( );

		#endregion Globals

		private IEnumerable<Individual> AllIndividualsEnumerable
		{
			get
			{
				return
					WildIndividuals.Cast<Individual> ( )
					.Concat ( MutantIndividuals.Cast<Individual> ( ) )
					.Concat ( AmpIndividuals.Cast<Individual> ( ) );
			}
		}

		private List<Individual> AllIndividuals => AllIndividualsEnumerable.ToList ( );

		public int TotalIndividuals => V.A + V.B + V.C;

		public Variables V { get; set; }

		public TextWriter Output { get; set; }

		public struct HighestValue
		{
			public int Wild;
			public int Mutant;
			public int Amp;
		}

		public HighestValue Highest => highest;

		private static List<T> CreateStartingDistribution<T>
			(
			double repMean, double repCv,
			double lethalMean, double lethalCv,
			int count
			)
			where T : Individual, new()
		{
			var result = new List<T> ( count );

			var repSd = repMean * repCv;
			var lethalSd = lethalMean * lethalCv;

			var reps = Utility.NextGaussianSymbols ( repMean, repSd, count );
			var lethals = Utility.NextGaussianSymbols ( lethalMean, lethalSd, count );

			for ( var i = 0; i < count; ++i )
			{
				var rep = Math.Max ( 0, reps[i] );
				var lethal = Math.Max ( 0, lethals[i] );
				result.Add ( new T ( )
				{
					rep = rep,
					lethal = lethal,
				}
				);
			}

			return result;
		}

		private void Step1 ( )
		{
			WildIndividuals = CreateStartingDistribution<WildIndividual> ( V.D, V.J, V.E, V.K, V.A );
			MutantIndividuals = CreateStartingDistribution<MutantIndividual> ( V.F, V.L, V.G, V.M, V.B );
			AmpIndividuals = CreateStartingDistribution<AmpIndividual> ( V.H, V.N, V.I, V.O, V.C );
		}

		private void Step1Output ( )
		{
			Output?.WriteLine ( "Step 1" );

			WildIndividuals = CreateStartingDistribution<WildIndividual> ( V.D, V.J, V.E, V.K, V.A );
			MutantIndividuals = CreateStartingDistribution<MutantIndividual> ( V.F, V.L, V.G, V.M, V.B );
			AmpIndividuals = CreateStartingDistribution<AmpIndividual> ( V.H, V.N, V.I, V.O, V.C );

			//var means = new double [ 3, 2 ];
			//means[0, 0] = WildIndividuals.Average ( x => x.rep );
			//means[0, 1] = WildIndividuals.Average ( x => x.lethal );
			//means[1, 0] = MutantIndividuals.Average ( x => x.rep );
			//means[1, 1] = MutantIndividuals.Average ( x => x.lethal );
			//means[2, 0] = AmpIndividuals.Average ( x => x.rep );
			//means[2, 1] = AmpIndividuals.Average ( x => x.lethal );


			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
					Output.WriteLine ( $"Wild_{i + 1} rep={WildIndividuals[i].rep} lethal={WildIndividuals[i].lethal}" );
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
					Output.WriteLine ( $"Mutant_{i + 1} rep={MutantIndividuals[i].rep} lethal={MutantIndividuals[i].lethal}" );
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
					Output.WriteLine ( $"Amp_{i + 1} rep={AmpIndividuals[i].rep} lethal={AmpIndividuals[i].lethal}" );

				Output.WriteLine ( "\n\n" );
			}
		}

		private void Step2 ( )
		{
			PredatorPercent = Utility.NextGaussian ( V.P, V.P * V.Q, 0 ) / 100;
			PredatorPercent = Math.Min ( .99, PredatorPercent );
			PredatorPercent = Math.Max ( 0, PredatorPercent );
			var predatorPercentY = Math.Min ( V.Y * PredatorPercent, 1 );

			foreach ( var individual in AllIndividualsEnumerable )
			{
				individual.originalRep = individual.rep;
				individual.originalLethal = individual.lethal;

				individual.fitness =
					individual.lethal * predatorPercentY +
					individual.rep * ( 1 - predatorPercentY );
			}
		}

		private void Step2Output ( )
		{
			Output?.WriteLine ( $"Step #2" );

			PredatorPercent = Utility.NextGaussian ( V.P, V.P * V.Q, 0 ) / 100;
			PredatorPercent = Math.Min ( .99, PredatorPercent );
			PredatorPercent = Math.Max ( 0, PredatorPercent );
			Output?.WriteLine ( $"Predator Percent={PredatorPercent}" );
			double predatorPercentY = Math.Min ( V.Y * PredatorPercent, 1 );

			foreach ( var individual in AllIndividualsEnumerable )
			{
				individual.originalRep = individual.rep;
				individual.originalLethal = individual.lethal;

				individual.fitness =
					individual.lethal * predatorPercentY +
					individual.rep * ( 1 - predatorPercentY );
			}

			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					Output.WriteLine ( $"Wild_{i + 1} fitness = {ind.fitness} = {ind.lethal} * {predatorPercentY} + {ind.rep} * ( 1 - {predatorPercentY} )" );
				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Mutant_{i + 1} fitness = {ind.fitness} = {ind.lethal} * {predatorPercentY} + {ind.rep} * ( 1 - {predatorPercentY} )" );
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					Output.WriteLine ( $"Amp_{i + 1} fitness = {ind.fitness} = {ind.lethal} * {predatorPercentY} + {ind.rep} * ( 1 - {predatorPercentY} )" );
				}

				Output.WriteLine ( "\n\n" );
			}

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #2" );
			//	Output.WriteLine ( $"Predator Percent={PredatorPercent}" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} fitness={WildIndividuals[i].fitness}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} fitness={MutantIndividuals[i].fitness}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} fitness={AmpIndividuals[i].fitness}" );
			//	Output.WriteLine ( );
			//}
		}

		private void Step3 ( )
		{
			foreach ( var wild in WildIndividuals )
				wild.Adjust ( );
			foreach ( var amp in AmpIndividuals )
				amp.Adjust ( );
		}

		private void Step3Output ( )
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

		private void Step4 ( )
		{
			var predatorPercentY = Math.Min ( V.Y * PredatorPercent, 1 );
			foreach ( var individual in AllIndividualsEnumerable )
			{
				individual.tempFitness =
								individual.lethal * predatorPercentY +
								individual.rep * ( 1 - predatorPercentY );
			}

			var cutOffIndex = (int) Math.Round ( PredatorPercent * TotalIndividuals, 0 );
			cutOffIndex = Math.Min ( cutOffIndex, TotalIndividuals - 1 );
			var sorted = AllIndividualsEnumerable.OrderBy ( x => x.lethal );
			var cutOffLethal = sorted.ElementAt ( cutOffIndex ).lethal;

			foreach ( var ind in AllIndividualsEnumerable )
				if ( ind.lethal < cutOffLethal )
					ind.tempFitness = 0;

			WildTempTotal = WildIndividuals.Sum ( x => x.tempFitness );
			MutantTempTotal = MutantIndividuals.Sum ( x => x.tempFitness );
			AmpTempTotal = AmpIndividuals.Sum ( x => x.tempFitness );
		}

		private void Step4Output ( )
		{
			Output?.WriteLine ( "Step 4" );

			double predatorPercentY = Math.Min ( V.Y * PredatorPercent, 1 );
			foreach ( var individual in AllIndividualsEnumerable )
			{
				individual.tempFitness =
								individual.lethal * predatorPercentY +
								individual.rep * ( 1 - predatorPercentY );
			}

			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					Output.WriteLine ( $"Wild_{i + 1} tempFitness = {ind.tempFitness} = {ind.lethal} * {predatorPercentY} + {ind.rep} * ( 1 - {predatorPercentY} )" );
				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Mutant_{i + 1} tempFitness = {ind.tempFitness} = {ind.lethal} * {predatorPercentY} + {ind.rep} * ( 1 - {predatorPercentY} )" );
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					Output.WriteLine ( $"Amp_{i + 1} tempFitness = {ind.tempFitness} = {ind.lethal} * {predatorPercentY} + {ind.rep} * ( 1 - {predatorPercentY} )" );
				}

				Output.WriteLine ( );
			}

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #4" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} tempFitness={WildIndividuals[i].tempFitness} lethal={WildIndividuals[i].lethal}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} tempFitness={MutantIndividuals[i].tempFitness} lethal={MutantIndividuals[i].lethal}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} tempFitness={AmpIndividuals[i].tempFitness} lethal={AmpIndividuals[i].lethal}" );
			//	Output.WriteLine ( );
			//}

			int cutOffIndex = (int) Math.Round ( PredatorPercent * TotalIndividuals, 0 );
			cutOffIndex = Math.Min ( cutOffIndex, TotalIndividuals - 1 );
			Output?.WriteLine ( $"cutOffIndex = {cutOffIndex} = {PredatorPercent} * {TotalIndividuals}" );
			var sorted = AllIndividualsEnumerable.OrderBy ( x => x.lethal );
			var cutOffLethal = sorted.ElementAt ( cutOffIndex ).lethal;
			Output?.WriteLine ( $"cutOffLethal = {cutOffLethal}" );

			foreach ( var ind in AllIndividualsEnumerable )
				if ( ind.lethal < cutOffLethal )
					ind.tempFitness = 0;

			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					if ( ind.lethal < cutOffLethal )
						Output.WriteLine ( $"Wild_{i + 1} {ind.lethal} < {cutOffLethal} : tempFitness = 0" );
					else
						Output.WriteLine ( $"Wild_{i + 1} {ind.lethal} >= {cutOffLethal} : tempFitness = {ind.tempFitness}" );

				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					if ( ind.lethal < cutOffLethal )
						Output.WriteLine ( $"Mutant_{i + 1} {ind.lethal} < {cutOffLethal} : tempFitness = 0" );
					else
						Output.WriteLine ( $"Mutant_{i + 1} {ind.lethal} >= {cutOffLethal} : tempFitness = {ind.tempFitness}" );
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					if ( ind.lethal < cutOffLethal )
						Output.WriteLine ( $"Amp_{i + 1} {ind.lethal} < {cutOffLethal} : tempFitness = 0" );
					else
						Output.WriteLine ( $"Amp_{i + 1} {ind.lethal} >= {cutOffLethal} : tempFitness = {ind.tempFitness}" );
				}

				Output.WriteLine ( );
			}

			WildTempTotal = WildIndividuals.Sum ( x => x.tempFitness );
			MutantTempTotal = MutantIndividuals.Sum ( x => x.tempFitness );
			AmpTempTotal = AmpIndividuals.Sum ( x => x.tempFitness );

			Output?.WriteLine ( $"WildTempTotal = {WildTempTotal}" );
			Output?.WriteLine ( $"MutantTempTotal = {MutantTempTotal}" );
			Output?.WriteLine ( $"AmpTempTotal = {AmpTempTotal}" );

			Output?.WriteLine ( "\n\n" );

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( );
			//	Output.WriteLine ( $"cut off lethal = {cutOffLethal}" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} tempFitness={WildIndividuals[i].tempFitness} lethal={WildIndividuals[i].lethal}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} tempFitness={MutantIndividuals[i].tempFitness} lethal={MutantIndividuals[i].lethal}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} tempFitness={AmpIndividuals[i].tempFitness} lethal={AmpIndividuals[i].lethal}" );
			//	Output.WriteLine ( );
			//	Output.WriteLine ( $"wild tempFitness sum = {WildTempTotal}" );
			//	Output.WriteLine ( $"mutant tempFitness sum = {MutantTempTotal}" );
			//	Output.WriteLine ( $"amp tempFitness sum = {AmpTempTotal}" );
			//	Output.WriteLine ( );
			//}
		}

		private void Step5 ( )
		{
			WildLost = WildIndividuals.Sum ( x => x.Lost );
			MutantLost = MutantIndividuals.Sum ( x => x.Lost );
			AmpLost = AmpIndividuals.Sum ( x => x.Lost );
		}

		private void Step5Output ( )
		{
			Output?.WriteLine ( "Step 5" );

			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					Output.WriteLine ( $"Wild_{i + 1} Lost = {ind.Lost} = {ind.fitness} - {ind.tempFitness}" );

				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Mutant_{i + 1} Lost = {ind.Lost} = {ind.fitness} - {ind.tempFitness}" );
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					Output.WriteLine ( $"Amp_{i + 1} Lost = {ind.Lost} = {ind.fitness} - {ind.tempFitness}" );
				}

				Output.WriteLine ( );
			}

			WildLost = WildIndividuals.Sum ( x => x.Lost );
			MutantLost = MutantIndividuals.Sum ( x => x.Lost );
			AmpLost = AmpIndividuals.Sum ( x => x.Lost );

			Output?.WriteLine ( $"WildLost = {WildLost}" );
			Output?.WriteLine ( $"MutantLost = {MutantLost}" );
			Output?.WriteLine ( $"AmpLost = {AmpLost}" );

			Output?.WriteLine ( "\n\n" );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #5" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} lost={WildIndividuals[i].Lost}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} lost={MutantIndividuals[i].Lost}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} lost={AmpIndividuals[i].Lost}" );
			//	Output.WriteLine ( );
			//	Output.WriteLine ( $"wild lost sum = {WildLost}" );
			//	Output.WriteLine ( $"mutant lost sum = {MutantLost}" );
			//	Output.WriteLine ( $"amp lost sum = {AmpLost}" );
			//	Output.WriteLine ( );
			//}
		}

		private void Step6 ( )
		{
			var rPlus1 = V.R + 1;
			var denom = WildTempTotal + MutantTempTotal + AmpTempTotal;

			if ( double.IsNaN ( denom ) )
			{
				isErrored = true;
				return;
			}

			TotalAllocation = 0;

			foreach ( var ind in AllIndividualsEnumerable )
			{
				var wildMultiplier = ( ind is WildIndividual ) ? rPlus1 : 1;
				var mutantMultiplier = ( ind is MutantIndividual ) ? rPlus1 : 1;
				var ampMultiplier = ( ind is AmpIndividual ) ? rPlus1 : 1;

				ind.allocation = 1;
				ind.allocation += wildMultiplier * WildLost * V.S / ( V.R * WildTempTotal + denom );
				ind.allocation += mutantMultiplier * MutantLost * V.S / ( V.R * MutantTempTotal + denom );
				ind.allocation += ampMultiplier * AmpLost * V.S / ( V.R * AmpTempTotal + denom );
				ind.allocation *= ind.tempFitness;

				TotalAllocation += ind.allocation;
			}
		}

		private void Step6Output ( )
		{
			Output?.WriteLine ( "Step 6" );

			var rPlus1 = V.R + 1;
			var denom = WildTempTotal + MutantTempTotal + AmpTempTotal;

			if ( double.IsNaN ( denom ) )
			{
				isErrored = true;
				return;
			}

			TotalAllocation = 0;

			foreach ( var ind in AllIndividualsEnumerable )
			{
				double wildMultiplier = ( ind is WildIndividual ) ? rPlus1 : 1;
				double mutantMultiplier = ( ind is MutantIndividual ) ? rPlus1 : 1;
				double ampMultiplier = ( ind is AmpIndividual ) ? rPlus1 : 1;

				ind.allocation = 1;
				ind.allocation += wildMultiplier * WildLost * V.S / ( V.R * WildTempTotal + denom );
				ind.allocation += mutantMultiplier * MutantLost * V.S / ( V.R * MutantTempTotal + denom );
				ind.allocation += ampMultiplier * AmpLost * V.S / ( V.R * AmpTempTotal + denom );
				ind.allocation *= ind.tempFitness;

				TotalAllocation += ind.allocation;
			}

			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					Output.WriteLine ( $"Wild_{i + 1} allocation = " );
					Output.WriteLine ( $"( 1 +" );
					Output.WriteLine ( $"{rPlus1} * {WildLost} * {V.S} / ( {V.R} * {WildTempTotal} + {denom} ) +" );
					Output.WriteLine ( $"{MutantLost} * {V.S} / ( {V.R} * {MutantTempTotal} + {denom} ) +" );
					Output.WriteLine ( $"{AmpLost} * {V.S} / ( {V.R} * {AmpTempTotal} + {denom} ) +" );
					Output.WriteLine ( $") * {ind.tempFitness} = {ind.allocation}" );
				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Mutant_{i + 1} allocation = " );
					Output.WriteLine ( $"( 1 +" );
					Output.WriteLine ( $"{WildLost} * {V.S} / ( {V.R} * {WildTempTotal} + {denom} ) +" );
					Output.WriteLine ( $"{rPlus1} * {MutantLost} * {V.S} / ( {V.R} * {MutantTempTotal} + {denom} ) +" );
					Output.WriteLine ( $"{AmpLost} * {V.S} / ( {V.R} * {AmpTempTotal} + {denom} ) +" );
					Output.WriteLine ( $") * {ind.tempFitness} = {ind.allocation}" );
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Amp_{i + 1} allocation = " );
					Output.WriteLine ( $"( 1 +" );
					Output.WriteLine ( $"{WildLost} * {V.S} / ( {V.R} * {WildTempTotal} + {denom} ) +" );
					Output.WriteLine ( $"{MutantLost} * {V.S} / ( {V.R} * {MutantTempTotal} + {denom} ) +" );
					Output.WriteLine ( $"{rPlus1} * {AmpLost} * {V.S} / ( {V.R} * {AmpTempTotal} + {denom} ) +" );
					Output.WriteLine ( $") * {ind.tempFitness} = {ind.allocation}" );
				}

				Output.WriteLine ( "\n\n" );
			}

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #6" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} allocation={WildIndividuals[i].allocation}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} allocation={MutantIndividuals[i].allocation}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} allocation={AmpIndividuals[i].allocation}" );
			//	Output.WriteLine ( );
			//	Output.WriteLine ( $"total allocation = {TotalAllocation}" );
			//	Output.WriteLine ( );
			//}
		}

		private List<Individual> ChooseParents ( )
		{
			var chosen = new List<Individual> ( );
			var all = AllIndividuals;

			for ( var i = 0; i != TotalIndividuals; ++i )
			{
				double cumulative = 0;
				var cutOff = Utility.NextDouble * TotalAllocation;

				foreach ( var ind in all )
				{
					cumulative += ind.allocation;

					if ( !( cutOff < cumulative ) )
						continue;

					Individual offspring;
					switch ( ind )
					{
						case WildIndividual _:
							offspring = new WildIndividual
							{
								lethal = Math.Max ( 0, Utility.NextGaussian ( ind.originalLethal, ind.originalLethal * V.K ) ),
								rep = Math.Max ( 0, Utility.NextGaussian ( ind.originalRep, ind.originalRep * V.J ) )
							};
							chosen.Add ( offspring );
							break;

						case MutantIndividual _:
							offspring = new MutantIndividual
							{
								lethal = Math.Max ( 0, Utility.NextGaussian ( ind.originalLethal, ind.originalLethal * V.M ) ),
								rep = Math.Max ( 0, Utility.NextGaussian ( ind.originalRep, ind.originalRep * V.L ) )
							};
							chosen.Add ( offspring );
							break;

						case AmpIndividual _:
							offspring = new AmpIndividual
							{
								lethal = Math.Max ( 0, Utility.NextGaussian ( ind.originalLethal, ind.originalLethal * V.O ) ),
								rep = Math.Max ( 0, Utility.NextGaussian ( ind.originalRep, ind.originalRep * V.N ) )
							};
							chosen.Add ( offspring );
							break;
					}
					break;
				}
			}
			if ( chosen.Sum ( x => x.rep ) == 0 )
				isErrored = true;

			return chosen;
		}

		private List<Individual> ChooseParentsOutput ( )
		{
			var chosen = new List<Individual> ( );
			var all = AllIndividuals;

			double cumulative = 0;
			if ( Output != null )
			{
				for ( int i = 0; i != WildIndividuals.Count; ++i )
				{
					var ind = WildIndividuals[i];
					Output.WriteLine ( $"Wild_{i + 1} cumulative = {cumulative + ind.allocation} = {cumulative} + {ind.allocation}" );
					cumulative += ind.allocation;
				}
				for ( int i = 0; i != MutantIndividuals.Count; ++i )
				{
					var ind = MutantIndividuals[i];
					Output.WriteLine ( $"Mutant_{i + 1} cumulative = {cumulative + ind.allocation} = {cumulative} + {ind.allocation}" );
					cumulative += ind.allocation;
				}
				for ( int i = 0; i != AmpIndividuals.Count; ++i )
				{
					var ind = AmpIndividuals[i];
					Output.WriteLine ( $"Amp_{i + 1} cumulative = {cumulative + ind.allocation} = {cumulative} + {ind.allocation}" );
					cumulative += ind.allocation;
				}

				Output.WriteLine ( $"TotalAllocation = {TotalAllocation}" );
			}

			for ( int i = 0; i != TotalIndividuals; ++i )
			{
				cumulative = 0;
				double cutOff = Utility.NextDouble * TotalAllocation;
				Output?.WriteLine ( $"Parent_{i + 1} cutOff = {cutOff}" );

				foreach ( var ind in all )
				{
					cumulative += ind.allocation;

					if ( cutOff < cumulative )
					{
						Individual offspring;
						switch ( ind )
						{
							case WildIndividual w:
								offspring = new WildIndividual
								{
									lethal = Math.Max ( 0, Utility.NextGaussian ( ind.originalLethal, ind.originalLethal * V.K ) ),
									rep = Math.Max ( 0, Utility.NextGaussian ( ind.originalRep, ind.originalRep * V.J ) )
								};
								chosen.Add ( offspring );
								Output?.WriteLine ( $"    Wild originalLethal = {ind.originalLethal} originalRep = {ind.originalRep}" );
								Output?.WriteLine ( $"    Offspring lethal = {offspring.lethal} rep = {offspring.rep}" );
								break;

							case MutantIndividual m:
								offspring = new MutantIndividual
								{
									lethal = Math.Max ( 0, Utility.NextGaussian ( ind.originalLethal, ind.originalLethal * V.M ) ),
									rep = Math.Max ( 0, Utility.NextGaussian ( ind.originalRep, ind.originalRep * V.L ) )
								};
								chosen.Add ( offspring );
								Output?.WriteLine ( $"    Mutant originalLethal = {ind.originalLethal} originalRep = {ind.originalRep}" );
								Output?.WriteLine ( $"    Offspring lethal = {offspring.lethal} rep = {offspring.rep}" );
								break;

							case AmpIndividual a:
								offspring = new AmpIndividual
								{
									lethal = Math.Max ( 0, Utility.NextGaussian ( ind.originalLethal, ind.originalLethal * V.O ) ),
									rep = Math.Max ( 0, Utility.NextGaussian ( ind.originalRep, ind.originalRep * V.N ) )
								};
								chosen.Add ( offspring );
								Output?.WriteLine ( $"    Amp originalLethal = {ind.originalLethal} originalRep = {ind.originalRep}" );
								Output?.WriteLine ( $"    Offspring lethal = {offspring.lethal} rep = {offspring.rep}" );
								break;

							default:
								break;
						}

						//if ( double.IsNaN ( chosen.Last ( ).rep ) )
						//	Debug.Assert ( false );
						break;
					}
				}
			}
			if ( chosen.Sum ( x => x.rep ) == 0 )
				isErrored = true;

			return chosen;
		}

		private void Step7 ( )
		{
			var chosen = ChooseParents ( );
			WildIndividuals = chosen.OfType<WildIndividual> ( ).ToList ( );
			MutantIndividuals = chosen.OfType<MutantIndividual> ( ).ToList ( );
			AmpIndividuals = chosen.OfType<AmpIndividual> ( ).ToList ( );
		}

		private void Step7Output ( )
		{
			Output?.WriteLine ( "Step 7" );

			//if ( Output != null )
			//{
			//	Output.WriteLine ( $"Step #7" );
			//	for ( int i = 0; i != WildIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Wild_{i + 1} probability={WildIndividuals[i].allocation / TotalAllocation}" );
			//	for ( int i = 0; i != MutantIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Mutant_{i + 1} probability={MutantIndividuals[i].allocation / TotalAllocation}" );
			//	for ( int i = 0; i != AmpIndividuals.Count; ++i )
			//		Output.WriteLine ( $"Amp_{i + 1} probability={AmpIndividuals[i].allocation / TotalAllocation}" );
			//	Output.WriteLine ( );
			//}

			//var original = AllIndividuals;
			var chosen = ChooseParentsOutput ( );
			WildIndividuals = chosen.OfType<WildIndividual> ( ).ToList ( );
			MutantIndividuals = chosen.OfType<MutantIndividual> ( ).ToList ( );
			AmpIndividuals = chosen.OfType<AmpIndividual> ( ).ToList ( );

			//if ( AllIndividualsEnumerable.Sum ( x => x.rep ) == 0 )
			//	Debug.Assert ( false );
			//if ( AllIndividualsEnumerable.Sum ( x => x.lethal ) == 0 )
			//	Debug.Assert ( false );

			if ( Output != null )
			{
				Output.WriteLine ( );
				Output.WriteLine ( $"wild chosen = {WildIndividuals.Count}" );
				Output.WriteLine ( $"mutant chosen = {MutantIndividuals.Count}" );
				Output.WriteLine ( $"amp chosen = {AmpIndividuals.Count}" );
				Output.WriteLine ( );
				Output.WriteLine ( "\n\n" );
			}
		}

		private void Step8 ( )
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

		private void Step8Output ( )
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

		private void SimulateGeneration ( )
		{
			//if ( V.A == 0 || V.B == 0 || V.C == 0 )
			//	Debug.Assert ( false );
			Step2 ( );
			Step3 ( );
			Step4 ( );
			Step5 ( );
			Step6 ( );
			if ( isErrored )
				return;
			Step7 ( );
			Step8 ( );
		}

		private void SimulateGenerationOutput ( )
		{
			//if ( V.A == 0 || V.B == 0 || V.C == 0 )
			//	Debug.Assert ( false );
			Step2Output ( );
			Step3Output ( );
			Step4Output ( );
			Step5Output ( );
			Step6Output ( );
			if ( isErrored )
				return;
			Step7Output ( );
			Step8Output ( );
		}

		public void Iterate ( IProgress<decimal> progress = null )
		{
			var backup = V.Clone ( );
			highest = new HighestValue ( );
			for ( var iteration = 0; iteration < V.U; iteration++ )
			{
				progress?.Report ( 1.0m * iteration / V.U );
				Step1 ( );
				isErrored = false;
				for ( var i = 0; i != V.T && !isErrored; ++i )
				{
					SimulateGeneration ( );

					V.A = WildIndividuals.Count;
					V.B = MutantIndividuals.Count;
					V.C = AmpIndividuals.Count;

					if ( new[] { V.A, V.B, V.C }.Contains ( TotalIndividuals ) )
						break;
				}

				if ( V.A > V.B && V.A > V.C )
					++highest.Wild;
				else if ( V.B > V.C && V.B > V.A )
					++highest.Mutant;
				else if ( V.C > V.B && V.C > V.A )
					++highest.Amp;

				V = backup.Clone ( );
			}
		}

		public void IterateOutput ( )
		{
			var backup = V.Clone ( );
			highest = new HighestValue ( );
			Output?.WriteLine ( $"{V}\n\n" );
			for ( int iteration = 0; iteration < V.U; iteration++ )
			{
				Step1 ( );
				isErrored = false;
				for ( int i = 0; i != V.T && !isErrored; ++i )
				{
					if ( Output != null )
					{
						Output.WriteLine ( $"Generation: {i + 1}" );
						Output.WriteLine ( );
					}

					SimulateGenerationOutput ( );

					if ( Output != null )
						Output.Flush ( );

					V.A = WildIndividuals.Count;
					V.B = MutantIndividuals.Count;
					V.C = AmpIndividuals.Count;

					if ( new[] { V.A, V.B, V.C }.Contains ( TotalIndividuals ) )
						break;
				}

				if ( V.A > V.B && V.A > V.C )
					++highest.Wild;
				else if ( V.B > V.C && V.B > V.A )
					++highest.Mutant;
				else if ( V.C > V.B && V.C > V.A )
					++highest.Amp;

				V = backup.Clone ( );

				//Console.WriteLine($"Iteration={iteration} {this}");
			}
		}

		public async Task IterateAsync ( IProgress<decimal> progress )
		{
			await Task.Run ( ( ) => Iterate ( progress ) );
		}

		public async Task IterateOutputAsync ( )
		{
			await Task.Run ( ( ) => IterateOutput ( ) );
		}

		public override string ToString ( )
		{
			return $"Wild={Highest.Wild} Mutant={Highest.Mutant} Amp={Highest.Amp}";
		}
	}
}