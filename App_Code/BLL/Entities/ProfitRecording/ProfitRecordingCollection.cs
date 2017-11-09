using System;
using System.Data;

namespace CryptoTrader.BLL
{
    public class ProfitRecordingCollection : BLLBaseCollection<ProfitRecording>
    {

        public static ProfitRecordingCollection GetByExchangeAndCurrecyBetween(BLL.ProfitRecording.enExchange exchange, BLL.ProfitRecording.enCurrency currency,DateTime dtfrom,DateTime dtTo,int avgPerMinutes,string arbSymbol)
        {
            ProfitRecordingCollection obj = new ProfitRecordingCollection();
            DataSet ds = new DAL.ProfitRecordings().GetByExchangeAndCurrecy(exchange, currency, dtfrom, dtTo, avgPerMinutes,arbSymbol);
            obj.MapObjects(ds);
            return obj;
        }


    }
}
