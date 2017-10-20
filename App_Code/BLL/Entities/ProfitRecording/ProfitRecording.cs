using System;
using System.Data;
using System.Threading;

namespace CryptoTrader.BLL
{
    [Serializable]
    public class ProfitRecording : BLL.BLLBaseObject
    {

        #region Enums
        public enum enExchange : int
        {
            Bitlish = 0,
            Cex = 1,   
            Bitfinex = 2
        }
        public enum enCurrency : int
        {
            USD = 0,
            EUR = 1,
            GBP = 2,
            RUB = 3
        }


        #endregion


        #region Fields
        private long _id = Constants.NullLong;
        private enExchange _exchange = enExchange.Bitlish;
        private enCurrency _currency = enCurrency.USD;
        private decimal _lunoBid = Constants.NullDecimal;
        private decimal _exchangeAsk = Constants.NullDecimal;
        private decimal _currencyToZARExchangeRate = Constants.NullDecimal;
        private decimal _profitPerc = Constants.NullDecimal;
        private DateTime _timeStamp = Constants.NullDateTime;
        public long Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public enExchange Exchange
        {
            get
            {
                return _exchange;
            }

            set
            {
                _exchange = value;
            }
        }

        public enCurrency Currency
        {
            get
            {
                return _currency;
            }

            set
            {
                _currency = value;
            }
        }

        public decimal LunoBid
        {
            get
            {
                return _lunoBid;
            }

            set
            {
                _lunoBid = value;
            }
        }

        public decimal ExchangeAsk
        {
            get
            {
                return _exchangeAsk;
            }

            set
            {
                _exchangeAsk = value;
            }
        }

        public decimal CurrencyToZARExchangeRate
        {
            get
            {
                return _currencyToZARExchangeRate;
            }

            set
            {
                _currencyToZARExchangeRate = value;
            }
        }

        public decimal ProfitPerc
        {
            get
            {
                return _profitPerc;
            }

            set
            {
                _profitPerc = value;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }

            set
            {
                _timeStamp = value;
            }
        }



        #endregion


        #region Methods
        public override bool MapData(DataRow row)
        {
            Id = GetInt(row, "Id");
            if (GetString(row, "Exchange") != Constants.NullString)
                Exchange = (enExchange)Enum.Parse(typeof(enExchange), GetString(row, "Exchange"), true);
            if (GetString(row, "Currency") != Constants.NullString)
                Currency = (enCurrency)Enum.Parse(typeof(enCurrency), GetString(row, "Currency"), true);

            LunoBid = GetDecimal(row, "LunoBid");
            ExchangeAsk = GetDecimal(row, "ExchangeAsk");
            CurrencyToZARExchangeRate = GetDecimal(row, "CurrencyToZARExchangeRate");
            ProfitPerc = GetDecimal(row, "ProfitPerc");
            TimeStamp = GetDateTime(row, "TimeStamp");

            return base.MapData(row);
        }

        public static ProfitRecording GetById(int id)
        {

            ProfitRecording obj = new ProfitRecording();
            DataSet ds = new DAL.ProfitRecordings().GetById(id);

            if (obj.MapData(ds) == false)
                obj = null;

            return obj;
        }

        public static ProfitRecording GetLatestByExchangeAndCurrnecy(enExchange exchange, enCurrency currency)
        {

            ProfitRecording obj = new ProfitRecording();
            DataSet ds = new DAL.ProfitRecordings().GetLatestByExchangeAndCurrnecy(exchange,currency);

            if (obj.MapData(ds) == false)
                obj = null;

            return obj;
        }

        public void Save()
        {

            System.Data.SqlClient.SqlConnection txnConn = DAL.ProfitRecordings.GetOpenConnection("CryptoTrades");
            IDbTransaction Txn = txnConn.BeginTransaction();

            try
            {
                // save alert row changes.
                DAL.ProfitRecordings profitRecordings = new DAL.ProfitRecordings();
                profitRecordings.Txn = Txn;
                profitRecordings.Save(ref _id, _exchange, _currency, _lunoBid, _exchangeAsk, _currencyToZARExchangeRate, _profitPerc);

                // commit transaction
                Txn.Commit();
            }
            catch (Exception ex)
            {
                Txn.Rollback();

                throw ex;
            }
            finally
            {
                txnConn.Close();
                txnConn.Dispose();
                Txn.Dispose();
            }


        }

        public static void RunRecordingService()
        {

            try
            {
                GenerateProfitRecordings();
            }
            catch (Exception ex)

            { }

            // sleep thread 
            Thread.Sleep(GetIntervalInMilliseconds(1));

            RunRecordingService();

        }
        private static int GetIntervalInMilliseconds(int minutes)
        {
            return minutes * 60 * 1000;
        }


        private static void GenerateProfitRecordings()
		{
            ProfitRecording pf = new ProfitRecording();

            var luno = LunoApi.GetLunoResult(0.00001M);
            decimal bestprice = ((luno.ask.price + luno.bid.price) / 2);

            var bitlishUSD = BitlishApi.GetBestPrices(BitlsihSymbolEnum.BtcUsd, 0.01m);
            var bitlishEUR = BitlishApi.GetBestPrices(BitlsihSymbolEnum.BtcEur, 0.01m);
  
            var cexUSD = CexApi.GetBestPrices(CEXSymbolEnum.Btc_Usd, 0.01m);
            var cexEUR = CexApi.GetBestPrices(CEXSymbolEnum.Btc_Eur, 0.01m);       
            var cexGBP = CexApi.GetBestPrices(CEXSymbolEnum.Btc_Gbp, 0.01m);

            var bitfinexUSD = BitfinexApi.GetBestPrices(BitfinexSymbolEnum.BtcUsd, 0.01m);
           
            decimal usdzar = CurrencyHelper.ConvertToZAR(1, CurrencyHelper.CurrencyEnum.Usd, false);
            decimal eurzar = CurrencyHelper.ConvertToZAR(1, CurrencyHelper.CurrencyEnum.Eur, false);
            decimal rubzar = CurrencyHelper.ConvertToZAR(1, CurrencyHelper.CurrencyEnum.Rub, false);
            decimal gbpzar = CurrencyHelper.ConvertToZAR(1, CurrencyHelper.CurrencyEnum.Gbp, false);


            // Bitlish USD;
            decimal profPerc = 0;
            try
            {
                profPerc = GetProfit(bitlishUSD.ask.price, luno.bid.price, usdzar, 0.4M, 4, 2000);

                pf = new ProfitRecording();
                pf.Exchange = enExchange.Bitlish;
                pf.Currency = enCurrency.USD;
                pf.LunoBid = luno.bid.price;
                pf.ExchangeAsk = bitlishUSD.ask.price;
                pf.CurrencyToZARExchangeRate = usdzar;
                pf.ProfitPerc = profPerc;
                pf.Save();
            }
            catch
            {// duplicate last recording

                ProfitRecording pflast = ProfitRecording.GetLatestByExchangeAndCurrnecy(enExchange.Bitlish, enCurrency.USD);
                pf = new ProfitRecording();
                pf.Exchange = pflast.Exchange;
                pf.Currency = pflast.Currency;
                pf.LunoBid = pflast.LunoBid;
                pf.ExchangeAsk = pflast.ExchangeAsk;
                pf.CurrencyToZARExchangeRate = pflast.CurrencyToZARExchangeRate;
                pf.ProfitPerc = pflast.ProfitPerc;
                pf.Save();
            }

            // Bitlish EUR;
            try
            {
                profPerc = GetProfit(bitlishEUR.ask.price, luno.bid.price, eurzar, 0.4M, 4, 1500);
                pf = new ProfitRecording();
                pf.Exchange = enExchange.Bitlish;
                pf.Currency = enCurrency.EUR;
                pf.LunoBid = luno.bid.price;
                pf.ExchangeAsk = bitlishEUR.ask.price;
                pf.CurrencyToZARExchangeRate = eurzar;
                pf.ProfitPerc = profPerc;
                pf.Save();
            }
            catch
            {// duplicate last recording
                ProfitRecording pflast = ProfitRecording.GetLatestByExchangeAndCurrnecy(enExchange.Bitlish, enCurrency.EUR);
                pf = new ProfitRecording();
                pf.Exchange = pflast.Exchange;
                pf.Currency = pflast.Currency;
                pf.LunoBid = pflast.LunoBid;
                pf.ExchangeAsk = pflast.ExchangeAsk;
                pf.CurrencyToZARExchangeRate = pflast.CurrencyToZARExchangeRate;
                pf.ProfitPerc = pflast.ProfitPerc;
                pf.Save();
            }


            // CEX USD;
            try
            {
                profPerc = GetProfit(cexUSD.ask.price, luno.bid.price, usdzar, 0.4M, 3.5M, 3000);
                pf = new ProfitRecording();
                pf.Exchange = enExchange.Cex;
                pf.Currency = enCurrency.USD;
                pf.LunoBid = luno.bid.price;
                pf.ExchangeAsk = cexUSD.ask.price;
                pf.CurrencyToZARExchangeRate = usdzar;
                pf.ProfitPerc = profPerc;
                pf.Save();
            }
            catch
            {// duplicate last recording
                ProfitRecording pflast = ProfitRecording.GetLatestByExchangeAndCurrnecy(enExchange.Cex, enCurrency.USD);
                pf = new ProfitRecording();
                pf.Exchange = pflast.Exchange;
                pf.Currency = pflast.Currency;
                pf.LunoBid = pflast.LunoBid;
                pf.ExchangeAsk = pflast.ExchangeAsk;
                pf.CurrencyToZARExchangeRate = pflast.CurrencyToZARExchangeRate;
                pf.ProfitPerc = pflast.ProfitPerc;
                pf.Save();
            }
    
            // CEX EUR;
            try
            {
                profPerc = GetProfit(cexEUR.ask.price, luno.bid.price, eurzar, 0.4M, 3.5M, 3000);
                pf = new ProfitRecording();
                pf.Exchange = enExchange.Cex;
                pf.Currency = enCurrency.EUR;
                pf.LunoBid = luno.bid.price;
                pf.ExchangeAsk = cexEUR.ask.price;
                pf.CurrencyToZARExchangeRate = eurzar;
                pf.ProfitPerc = profPerc;
                pf.Save();
            }
            catch
            {// duplicate last recording
                ProfitRecording pflast = ProfitRecording.GetLatestByExchangeAndCurrnecy(enExchange.Cex, enCurrency.EUR);
                pf = new ProfitRecording();
                pf.Exchange = pflast.Exchange;
                pf.Currency = pflast.Currency;
                pf.LunoBid = pflast.LunoBid;
                pf.ExchangeAsk = pflast.ExchangeAsk;
                pf.CurrencyToZARExchangeRate = pflast.CurrencyToZARExchangeRate;
                pf.ProfitPerc = pflast.ProfitPerc;
                pf.Save();
            }
        
            // CEX GBP;
            try
            {
                profPerc = GetProfit(cexGBP.ask.price, luno.bid.price, gbpzar, 0.4M, 3.5M, 2000);
                pf = new ProfitRecording();
                pf.Exchange = enExchange.Cex;
                pf.Currency = enCurrency.GBP;
                pf.LunoBid = luno.bid.price;
                pf.ExchangeAsk = cexGBP.ask.price;
                pf.CurrencyToZARExchangeRate = gbpzar;
                pf.ProfitPerc = profPerc;
                pf.Save();
            }
            catch { // duplicate last recording
                ProfitRecording pflast = ProfitRecording.GetLatestByExchangeAndCurrnecy(enExchange.Cex, enCurrency.GBP);
                pf = new ProfitRecording();
                pf.Exchange = pflast.Exchange;
                pf.Currency = pflast.Currency;
                pf.LunoBid = pflast.LunoBid;
                pf.ExchangeAsk = pflast.ExchangeAsk;
                pf.CurrencyToZARExchangeRate = pflast.CurrencyToZARExchangeRate;
                pf.ProfitPerc = pflast.ProfitPerc;
                pf.Save();
            }

            // Bitfinex USD;
            try
            {
                profPerc = GetProfit(bitfinexUSD.ask.price, luno.bid.price, usdzar, 0.4M, 3.5M, 2000);
                pf = new ProfitRecording();
                pf.Exchange = enExchange.Bitfinex;
                pf.Currency = enCurrency.USD;
                pf.LunoBid = luno.bid.price;
                pf.ExchangeAsk = bitfinexUSD.ask.price;
                pf.CurrencyToZARExchangeRate = usdzar;
                pf.ProfitPerc = profPerc;
                pf.Save();
            }
            catch
            { // duplicate last recording
                ProfitRecording pflast = ProfitRecording.GetLatestByExchangeAndCurrnecy(enExchange.Bitfinex, enCurrency.USD);
                pf = new ProfitRecording();
                pf.Exchange = pflast.Exchange;
                pf.Currency = pflast.Currency;
                pf.LunoBid = pflast.LunoBid;
                pf.ExchangeAsk = pflast.ExchangeAsk;
                pf.CurrencyToZARExchangeRate = pflast.CurrencyToZARExchangeRate;
                pf.ProfitPerc = pflast.ProfitPerc;
                pf.Save();
            }



        }

        private static decimal GetProfit(decimal exchangeAskPrice, decimal lunoBidPrice, decimal exchangerate, decimal spread, decimal feeperc, decimal amount)
        {


            decimal fee = 0;

            decimal zarprice = 0;
            decimal zarfromcc = 0;
            decimal btcweget = 0;
            decimal takerfeeperc = 0.2M;

            decimal btctransferfee = 0.001M;
            decimal tradingfees = 0.0M;
            decimal zararrinving = 0;

            decimal profit = 0;
            decimal profitperc = 0;



            fee = amount * (feeperc / 100);
            zarprice = (exchangerate + spread);
            zarfromcc = zarprice * amount;
            amount = amount - fee;
            btcweget = amount / exchangeAskPrice;
            tradingfees = (btcweget * (takerfeeperc / 100)); // taker fees
            tradingfees += btctransferfee; // transfer fees
            btcweget = btcweget - tradingfees;

            zararrinving = btcweget * lunoBidPrice;
            profit = zararrinving - zarfromcc;
            profitperc = (profit / zarfromcc) * 100;

            return profitperc;
        }

        #endregion
    }
}
