using System;
using System.Data;
using System.Data.SqlClient;

namespace CryptoTrader.DAL
{
    class ProfitRecordings : DAL.DALBase
    {
        //public Alerts()
        //{
        //    ConnectionStringName = "ThinkingTraveller";
        //}

        public DataSet GetAll()
        {
            return ExecuteDataSet("CryptoTrades", "SELECT * FROM ProfitRecordings WITH (NOLOCK) ORDER BY Id", CommandType.Text);
        }

        public DataSet GetById(int id)
        {
            return ExecuteDataSet("CryptoTrades", "SELECT * FROM ProfitRecordings WITH (NOLOCK) WHERE Id = @Id", CommandType.Text,
                CreateParameter("@Id",SqlDbType.Int,id));
        }
        public DataSet GetByExchangeAndCurrecy(BLL.ProfitRecording.enExchange exchange, BLL.ProfitRecording.enCurrency currency,DateTime dtfrom, DateTime dtTo)
        {
            return ExecuteDataSet(@"CryptoTrades", "SELECT * FROM ProfitRecordings WITH (NOLOCK) WHERE (exchange = '" + exchange.ToString() + "' or exchange = '" + ((int)exchange).ToString() + @"')
                                                            AND (Currency = '" + currency.ToString() + "' or Currency = '" + ((int)currency).ToString() + @"')
                                                            AND TimeStamp Between @dtFrom AND @dtTo 
                                                            order by TimeStamp desc", CommandType.Text, 
                                                            CreateParameter("@dtFrom", SqlDbType.DateTime, dtfrom, ParameterDirection.InputOutput),
                                                            CreateParameter("@dtTo", SqlDbType.DateTime, dtTo, ParameterDirection.InputOutput)
                                    );
        }
        public DataSet GetLatestByExchangeAndCurrnecy(BLL.ProfitRecording.enExchange exchange, BLL.ProfitRecording.enCurrency currency)
        {
            return ExecuteDataSet(@"CryptoTrades", "SELECT top 1 * FROM ProfitRecordings WITH (NOLOCK) WHERE (exchange = '" + exchange.ToString() + "' or exchange = '" + ((int)exchange).ToString() + @"')
                                                            AND ( Currency = '" + currency.ToString() + "' or Currency = '" + ((int)currency).ToString() + @"') 
                                            order by TimeStamp desc", CommandType.Text);
        }


        public void Save(ref long id,
                                BLL.ProfitRecording.enExchange exchange,
                                BLL.ProfitRecording.enCurrency currency,
                                decimal lunoBid,
                                decimal exchangeAsk,
                                decimal currencyToZARExchangeRate,
                                decimal profitPerc
                                )
        {
            System.Text.StringBuilder query = new System.Text.StringBuilder();

            SqlCommand cmd = new SqlCommand();
            query.Append(@"BEGIN
    	                            -- Update existing row if it exists else insert new  row
    	                            IF ((SELECT Count(Id) FROM ProfitRecordings  WITH (NOLOCK) WHERE Id = @Id) > 0)
    	                            BEGIN
    
    		                            -- update existing  row
    		                            UPDATE 
    			                            [ProfitRecordings]
    		                            SET 
                                            
                                            Exchange = @Exchange,
                                            Currency = @Currency,
                                            LunoBid = @LunoBid,
                                            ExchangeAsk = @ExchangeAsk,
                                            CurrencyToZARExchangeRate = @CurrencyToZARExchangeRate,
                                            ProfitPerc = @ProfitPerc

    		                            WHERE 
    			                            [Id]                    = @Id
    	                            END
    	                            ELSE
    	                            BEGIN
    		                            -- insert new row
    		                            INSERT INTO [ProfitRecordings]
    			                            (   
                                                
                                                Exchange,
                                                Currency,
                                                LunoBid,
                                                ExchangeAsk,
                                                CurrencyToZARExchangeRate,
                                                ProfitPerc,
                                                TimeStamp
                                            )
    		                             VALUES
    			                            (
                                                
                                                @Exchange,
                                                @Currency,
                                                @LunoBid,
                                                @ExchangeAsk,
                                                @CurrencyToZARExchangeRate,
                                                @ProfitPerc,
                                                getDate()

                                            )

                                            
    
    			                            SELECT @Id = @@Identity FROM [ProfitRecordings] WITH (NOLOCK) 

                                            
    
    	                            END
                                       
                                END");

            ExecuteNonQuery("CryptoTrades", out cmd, query.ToString(), CommandType.Text,
                                    CreateParameter("@Id", SqlDbType.BigInt, id, ParameterDirection.InputOutput),
                                    CreateParameter("@Exchange", SqlDbType.VarChar, exchange.ToString(), ParameterDirection.InputOutput),
                                    CreateParameter("@Currency", SqlDbType.VarChar, currency.ToString(), ParameterDirection.InputOutput),
                                    
                                    
                                    CreateParameter("@LunoBid", SqlDbType.Decimal, lunoBid, ParameterDirection.InputOutput),
                                    CreateParameter("@ExchangeAsk", SqlDbType.Decimal, exchangeAsk, ParameterDirection.InputOutput),
                                    CreateParameter("@CurrencyToZARExchangeRate", SqlDbType.Decimal, currencyToZARExchangeRate, ParameterDirection.InputOutput),
                                    CreateParameter("@ProfitPerc", SqlDbType.Decimal, profitPerc, ParameterDirection.InputOutput)
                            );

            id = int.Parse(cmd.Parameters["@id"].Value.ToString());
        }


    }
}
