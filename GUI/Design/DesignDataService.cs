using System;
using GUI.Model;

namespace GUI.Design
{
	public class DesignDataService : IDataService
	{
		public void GetData ( Action<ResultItem, Exception> callback )
		{
			// Use this to create design time data

			//var item = new ResultItem ( );
			//callback ( item, null );
		}
	}
}