using System.Collections.Generic;
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
				{
					instance = new Defaults ( );
					instance.FillDefaults ( );
				}
				return instance;
			}
		}

		private static readonly string DEFAULTSFILE = "Defaults.json";

		private Dictionary<string, List<VariableItem>> defaultVars;

		public Dictionary<string, List<VariableItem>> DefaultVars
		{
			get
			{
				FillDefaults ( );
				return defaultVars;
			}
			set
			{
				defaultVars = value;
				Save ( );
			}
		}

		static Defaults ( )
		{

			if ( !File.Exists ( DEFAULTSFILE ) )
				Save ( );
		}

		public void FillDefaults ( )
		{
			if ( defaultVars != null )
				return;

			defaultVars = new Dictionary<string, List<VariableItem>>
			{
				["Version 0"] = new List<VariableItem> ( ),
				["Version 1"] = new List<VariableItem> ( ),
				["Version 2"] = new List<VariableItem> ( ),
				["Version 3"] = new List<VariableItem> ( ),
				["Version 4"] = new List<VariableItem> ( ),
				["Version 5"] = new List<VariableItem> ( )
			};

			foreach ( var def in EvoBio_Version_0.Variables.Default )
				defaultVars["Version 0"].Add ( new VariableItem ( def.Key, def.Value.description, def.Value.val ) );
			foreach ( var def in EvoBio_Version_1.Variables.Default )
				defaultVars["Version 1"].Add ( new VariableItem ( def.Key, def.Value.description, def.Value.val ) );
			foreach ( var def in EvoBio_Version_2.Variables.Default )
				defaultVars["Version 2"].Add ( new VariableItem ( def.Key, def.Value.description, def.Value.val ) );
			foreach ( var def in EvoBio_Version_3.Variables.Default )
				defaultVars["Version 3"].Add ( new VariableItem ( def.Key, def.Value.description, def.Value.val ) );
			foreach ( var def in EvoBio_Version_4.Variables.Default )
				defaultVars["Version 4"].Add ( new VariableItem ( def.Key, def.Value.description, def.Value.val ) );
			foreach ( var def in EvoBio_Version_5.Variables.Default )
				defaultVars["Version 5"].Add ( new VariableItem ( def.Key, def.Value.description, def.Value.val ) );
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