using EvoBio;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GUI.Model;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace GUI.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// See http://www.mvvmlight.net
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		#region Members

		private ObservableCollection<VariableItem> variableCollection;
		private Dictionary<string, VariableItem> variableMap;
		private VariableItem selectedVariableItem;
		private string rangedVariableName;
		private decimal rangeStartValue;
		private decimal rangeEndValue;
		private decimal rangeStepValue;
		private decimal diValue;
		private decimal joValue;
		private ObservableCollection<ResultItem> results;
		private ObservableCollection<string> xAxisValues;
		private Func<decimal, string> yFormatter;
		private SeriesCollection seriesValues;
		private bool isDiGrouped;
		private bool isJoGrouped;
		private decimal progressIterations;

		#endregion Members

		#region Properties

		public ObservableCollection<VariableItem> VariableCollection
		{
			get => this.variableCollection;
			set => Set ( nameof ( VariableCollection ), ref variableCollection, value );
		}

		public VariableItem SelectedVariableItem
		{
			get => this.selectedVariableItem;
			set => Set ( nameof ( SelectedVariableItem ), ref selectedVariableItem, value );
		}

		public string RangedVariableName
		{
			get => this.rangedVariableName;
			set => Set ( nameof ( RangedVariableName ), ref rangedVariableName, value );
		}

		public decimal RangeStartValue
		{
			get => this.rangeStartValue;
			set => Set ( nameof ( RangeStartValue ), ref rangeStartValue, value );
		}

		public decimal RangeEndValue
		{
			get => this.rangeEndValue;
			set => Set ( nameof ( RangeEndValue ), ref rangeEndValue, value );
		}

		public decimal RangeStepValue
		{
			get => this.rangeStepValue;
			set => Set ( nameof ( RangeStepValue ), ref rangeStepValue, value );
		}

		public RelayCommand InputDiCommand { get; set; }

		public RelayCommand InputJoCommand { get; set; }

		public RelayCommand SimulateCommand { get; set; }

		public decimal DiValue
		{
			get => this.diValue;
			set => Set ( nameof ( DiValue ), ref diValue, value );
		}

		public decimal JoValue
		{
			get => this.joValue;
			set => Set ( nameof ( JoValue ), ref joValue, value );
		}

		public ObservableCollection<string> XAxisValues
		{
			get => xAxisValues;
			set => Set ( nameof ( XAxisValues ), ref xAxisValues, value );
		}

		public Func<decimal, string> YFormatter
		{
			get => yFormatter;
			set => Set ( nameof ( YFormatter ), ref yFormatter, value );
		}

		public SeriesCollection SeriesValues
		{
			get => seriesValues;
			set => Set ( nameof ( SeriesValues ), ref seriesValues, value );
		}

		public ObservableCollection<ResultItem> Results
		{
			get => this.results;
			set => Set ( nameof ( Results ), ref results, value );
		}

		public bool IsDiGrouped
		{
			get => this.isDiGrouped;
			set
			{
				Set ( nameof ( IsDiGrouped ), ref isDiGrouped, value );
				if ( value )
					IsJoGrouped = false;
			}
		}

		public bool IsJoGrouped
		{
			get => this.isJoGrouped;
			set
			{
				Set ( nameof ( IsJoGrouped ), ref isJoGrouped, value );
				if ( value )
					IsDiGrouped = false;
			}
		}

		public decimal ProgressIterations
		{
			get => this.progressIterations;
			set => Set ( nameof ( ProgressIterations ), ref progressIterations, value );
		}

		#endregion Properties

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel ( IDataService dataService )
		{
			Defaults.Load ( );
			VariableCollection = new ObservableCollection<VariableItem> ( Defaults.Instance.DefaultVariables.ToList ( ) );

			VariableCollectionToMap ( );

			InputDiCommand = new RelayCommand ( InputDi, CanInputDi );
			InputJoCommand = new RelayCommand ( InputJo, CanInputJo );
			SimulateCommand = new RelayCommand ( Simulate, CanSimulate );

			Results = new ObservableCollection<ResultItem> ( );
			XAxisValues = new ObservableCollection<string> ( );
			YFormatter = value => $"{value}";
			SeriesValues = new SeriesCollection
			{
				new LineSeries
				{
					Title = "Wild",
					Values = new ChartValues<double> ( ),
					PointGeometry = null,
					PointGeometrySize = 15,
					Fill = Brushes.Transparent
				},
				new LineSeries
				{
					Title = "Mutant",
					Values = new ChartValues<double> ( ),
					PointGeometry = null,
					PointGeometrySize = 15,
					Fill = Brushes.Transparent
				},
				new LineSeries
				{
					Title = "Amp",
					Values = new ChartValues<double> ( ),
					PointGeometry = null,
					PointGeometrySize = 15,
					Fill = Brushes.Transparent
				}
			};
		}

		private void VariableCollectionToMap ( )
		{
			variableMap = new Dictionary<string, VariableItem> ( );
			foreach ( var v in VariableCollection )
				variableMap[v.Name] = v;
		}

		private bool CanInputDi ( ) => true;

		private void InputDi ( )
		{
			variableMap["D"].Value = DiValue;
			variableMap["E"].Value = DiValue;
			variableMap["F"].Value = DiValue;
			variableMap["G"].Value = DiValue;
			variableMap["H"].Value = DiValue;
			variableMap["I"].Value = DiValue;

			RaisePropertyChanged ( nameof ( VariableCollection ) );
		}

		private bool CanInputJo ( ) => true;

		private void InputJo ( )
		{
			variableMap["J"].Value = JoValue;
			variableMap["K"].Value = JoValue;
			variableMap["L"].Value = JoValue;
			variableMap["M"].Value = JoValue;
			variableMap["N"].Value = JoValue;
			variableMap["O"].Value = JoValue;

			RaisePropertyChanged ( nameof ( VariableCollection ) );
		}

		private bool CanSimulate ( ) => true;

		private async void Simulate ( )
		{
			RangedVariableName = selectedVariableItem.Name;
			XAxisValues.Clear ( );
			Results.Clear ( );
			foreach ( var series in SeriesValues )
				series.Values.Clear ( );

			if ( RangeStartValue == 0m && RangeEndValue == 0m && RangeStepValue == 0m )
			{
				RangeStartValue = variableMap[RangedVariableName].Value;
				RangeEndValue = variableMap[RangedVariableName].Value;
				RangeStepValue = 1m;
			}

			var variablesDi = variableCollection.Where ( x => new string[] { "D", "E", "F", "G", "H", "I" }.Contains ( x.Name ) );
			var variablesJo = variableCollection.Where ( x => new string[] { "J", "K", "L", "M", "N", "O" }.Contains ( x.Name ) );

			var progress = new Progress<decimal> ( x =>
			{
				ProgressIterations = 100m * x;
			} );

			for ( var curValue = rangeStartValue; curValue <= rangeEndValue; curValue += RangeStepValue )
			{
				if ( IsDiGrouped )
					variablesDi.ForEach ( x => x.Value = curValue );
				else if ( IsJoGrouped )
					variablesJo.ForEach ( x => x.Value = curValue );
				else
					variableCollection.First ( x => x.Name == RangedVariableName ).Value = curValue;
				VariableCollectionToMap ( );
				var v = new Variables
				{
					A = (int) variableMap["A"].Value,
					B = (int) variableMap["B"].Value,
					C = (int) variableMap["C"].Value,
					D = (double) variableMap["D"].Value,
					E = (double) variableMap["E"].Value,
					F = (double) variableMap["F"].Value,
					G = (double) variableMap["G"].Value,
					H = (double) variableMap["H"].Value,
					I = (double) variableMap["I"].Value,
					J = (double) variableMap["J"].Value,
					K = (double) variableMap["K"].Value,
					L = (double) variableMap["L"].Value,
					M = (double) variableMap["M"].Value,
					N = (double) variableMap["N"].Value,
					O = (double) variableMap["O"].Value,
					P = (double) variableMap["P"].Value,
					Q = (double) variableMap["Q"].Value,
					R = (double) variableMap["R"].Value,
					S = (double) variableMap["S"].Value,
					T = (int) variableMap["T"].Value,
					U = (int) variableMap["U"].Value,
					Y = (double) variableMap["Y"].Value
				};
				var result = new ResultItem
				{
					AllVariables = v,
					RangedVariable = variableMap[RangedVariableName]
				};
				await result.RunAsync ( progress );
				Results.Add ( result );
				XAxisValues.Add ( $"{result.RangedVariableValue}" );
				SeriesValues[0].Values.Add ( result.WildPercentage );
				SeriesValues[1].Values.Add ( result.MutantPercentage );
				SeriesValues[2].Values.Add ( result.AmpPercentage );

				ProgressIterations = 0;
			}

			RangeStartValue = 0;
			RangeEndValue = 0;
			RangeStepValue = 0;
		}
	}
}