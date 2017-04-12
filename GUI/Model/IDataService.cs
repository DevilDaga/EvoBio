using System;

namespace GUI.Model
{
	public interface IDataService
	{
		void GetData ( Action<ResultItem, Exception> callback );
	}
}