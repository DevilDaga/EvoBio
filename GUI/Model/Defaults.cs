using System.Collections.ObjectModel;
using System.IO;
using System.Web.Script.Serialization;

namespace GUI.Model
{

	public class Defaults
	{
		private static Defaults instance;

		public static Defaults Instance
		{
			get
			{
				if ( instance == null )
					instance = new Defaults ( );
				return instance;
			}
		}

		private static readonly string DEFAULTSFILE = "Defaults.json";

		private ObservableCollection<VariableItem> defaultVariables = new ObservableCollection<VariableItem>
			{
				new VariableItem ( "A", "Starting quantity of wild individuals", 10 ),
				new VariableItem ( "B", "Starting quantity of mutant individuals", 10 ),
				new VariableItem ( "C", "Starting quantity of amp individuals", 10 ),
				new VariableItem ( "D", "Starting mean rep ability for wild individuals", 10 ),
				new VariableItem ( "E", "Starting mean lethal ability for wild individuals", 10 ),
				new VariableItem ( "F", "Starting mean rep ability for mutant individuals", 10 ),
				new VariableItem ( "G", "Starting mean lethal ability for mutant individuals", 10 ),
				new VariableItem ( "H", "Starting mean rep ability for amp individuals", 10 ),
				new VariableItem ( "I", "Starting mean lethal ability for amp individuals", 10 ),
				new VariableItem ( "J", "Coefficient of variation of rep ability for wild individuals", 0.1m ),
				new VariableItem ( "K", "Coefficient of variation of lethal ability for wild individuals", 0.1m ),
				new VariableItem ( "L", "Coefficient of variation of rep ability for mutant individuals", 0.1m ),
				new VariableItem ( "M", "Coefficient of variation of lethal ability for mutant individuals", 0.1m ),
				new VariableItem ( "N", "Coefficient of variation for rep ability for amp individuals", 0.1m ),
				new VariableItem ( "O", "Coefficient of variation for lethal ability for amp individuals", 0.1m ),
				new VariableItem ( "P", "Predator mean", 25 ),
				new VariableItem ( "Q", "Predator coefficient of variation", 0.1m ),
				new VariableItem ( "R", "Bias in allocation", 0.1m ),
				new VariableItem ( "S", "Implementation Failure", 1 ),
				new VariableItem ( "T", "Generations", 100 ),
				new VariableItem ( "U", "Iterations", 1000 ),
				new VariableItem ( "Y", "Percentage weighting", 1 )
			};

		public ObservableCollection<VariableItem> DefaultVariables
		{
			get => defaultVariables;
			set
			{
				defaultVariables = value;
				Save ( );
			}
		}

		static Defaults ( )
		{
			if ( !File.Exists ( DEFAULTSFILE ) )
				Save ( );
		}

		public static void Save ( )
		{
			if ( instance == null )
				return;
			File.WriteAllText ( DEFAULTSFILE, ( new JavaScriptSerializer ( ) ).Serialize ( instance ) );
		}

		public static void Load ( )
		{
			if ( File.Exists ( DEFAULTSFILE ) )
				instance = ( new JavaScriptSerializer ( ) )
					.Deserialize<Defaults> ( File.ReadAllText ( DEFAULTSFILE ) );
			else
			{
				instance = new Defaults ( );
				Save ( );
			}
		}
	}
}