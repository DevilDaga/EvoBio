namespace GUI.Model
{
	public class VariableItem
	{
		private string name;
		private decimal val;
		private string description;

		public string Name { get => this.name; set => this.name = value; }
		public decimal Value { get => this.val; set => this.val = value; }
		public string Description { get => this.description; set => this.description = value; }

		public VariableItem ( )
		{

		}

		public VariableItem ( string name, string description = "", decimal val = 0 )
		{
			Name = name;
			Value = val;
			Description = description;
		}

		public override string ToString ( ) => $"{Name} = {Value}";
	}
}