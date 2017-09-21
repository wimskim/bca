using System;
using System.Data;

namespace CryptoTrader.BLL
{
    public class ProfitRecordingCollection : BLLBaseCollection<ProfitRecording>
    {

        public static ProfitRecordingCollection GetByExchangeAndCurrecy(BLL.ProfitRecording.enExchange exchange, BLL.ProfitRecording.enCurrency currency,long periods)
        {
            ProfitRecordingCollection obj = new ProfitRecordingCollection();
            DataSet ds = new DAL.ProfitRecordings().GetByExchangeAndCurrecy(exchange, currency, periods);
            obj.MapObjects(ds);
            return obj;
        }


    }
}
