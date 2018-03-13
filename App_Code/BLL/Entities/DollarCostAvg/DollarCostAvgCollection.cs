using System;
using System.Data;

namespace CryptoTrader.BLL
{
    public class DollarCostAvgCollection : BLLBaseCollection<DollarCostAvg>
    {

        public static DollarCostAvgCollection GetAll()
        {
            DollarCostAvgCollection obj = new DollarCostAvgCollection();
            DataSet ds = new DAL.DollarCostAvg().GetAll();
            obj.MapObjects(ds);
            return obj;
        }


    }
}
