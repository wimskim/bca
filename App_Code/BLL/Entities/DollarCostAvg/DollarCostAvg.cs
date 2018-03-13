using System;
using System.Data;
using System.Threading;

namespace CryptoTrader.BLL
{
    [Serializable]
    public class DollarCostAvg : BLL.BLLBaseObject
    {
        
        #region Fields
        private long _id = Constants.NullLong;
        private DateTime _theDate = Constants.NullDateTime;
        private decimal _btcUsd = Constants.NullDecimal;
        private decimal _btc = Constants.NullDecimal;
        private decimal _usd = Constants.NullDecimal;
        
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

        public DateTime TheDate
        {
            get
            {
                return _theDate;
            }

            set
            {
                _theDate = value;
            }
        }

        public decimal BtcUsd
        {
            get
            {
                return _btcUsd;
            }

            set
            {
                _btcUsd = value;
            }
        }

        public decimal Btc
        {
            get
            {
                return _btc;
            }

            set
            {
                _btc = value;
            }
        }

        public decimal Usd
        {
            get
            {
                return _usd;
            }

            set
            {
                _usd = value;
            }
        }


        #endregion


        #region Methods
        public override bool MapData(DataRow row)
        {
            Id = GetInt(row, "Id");
            TheDate = GetDateTime(row, "TheDate");
            BtcUsd = GetDecimal(row, "BtcUsd");
            Btc = GetDecimal(row, "Btc");
            Usd = GetDecimal(row, "Usd");

            return base.MapData(row);
        }

        public static DollarCostAvg GetById(int id)
        {

            DollarCostAvg obj = new DollarCostAvg();
            DataSet ds = new DAL.DollarCostAvg().GetById(id);

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
                DAL.DollarCostAvg dollarcostavg  = new DAL.DollarCostAvg();
                dollarcostavg.Txn = Txn;
                dollarcostavg.Save(ref _id, _theDate, _btcUsd, _btc, _usd);

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

        public static void CalculateDollarCostAvg()
        {

            DollarCostAvgCollection all = DollarCostAvgCollection.GetAll();

            decimal btc = 587M;
            decimal usd = 0M;

       

            foreach (DollarCostAvg avg in all)
            {
                decimal diff = (btc * avg.BtcUsd) - usd;

                avg.Btc = btc;
                avg.Usd = usd;

                if (diff >= 0)
                {
                    if (diff < 0) diff = diff * -1;
                    avg.Btc = avg.Btc - ((diff / 2) / avg.BtcUsd);
                    avg.Usd = avg.Usd + ((diff / 2));
                }
                else
                {
                    if (diff < 0) diff = diff * -1;
                    avg.Btc = avg.Btc + ((diff / 2) / avg.BtcUsd);
                    avg.Usd = avg.Usd - ((diff / 2));
                }

                avg.Save();

                btc = avg.Btc;
                usd = avg.Usd;

            }




        }

        #endregion
    }
}
