using System;

namespace GUI.Model
{
	public class DataService : IDataService
	{
		public void GetData ( Action<ResultItem, Exception> callback )
		{
			// Use this to connect to the actual data service

			//var item = new ResultItem ( );
			//callback ( item, null );
		}
	}
}