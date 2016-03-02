using System;

namespace SewerGui.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("DINGEN EN DINGEN DOEN DINGEN");
            callback(item, null);
        }
    }
}