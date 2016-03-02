using System;
using SewerGui.Model;

namespace SewerGui.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("DESIGN DINGEN DINGEN DESIGN");
            callback(item, null);
        }
    }
}