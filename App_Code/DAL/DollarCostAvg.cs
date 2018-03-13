using System;
using System.Data;
using System.Data.SqlClient;


namespace CryptoTrader.DAL
{
    public class DollarCostAvg : DAL.DALBase
    {
        public DataSet GetAll()
        {
            return ExecuteDataSet("CryptoTrades", "SELECT * FROM dollarcostavg WITH (NOLOCK) ORDER BY Id", CommandType.Text);
        }

        public DataSet GetById(int id)
        {
            return ExecuteDataSet("CryptoTrades", "SELECT * FROM dollarcostavg WITH (NOLOCK) WHERE Id = @Id", CommandType.Text,
                CreateParameter("@Id", SqlDbType.Int, id));
        }
        public void Save(ref long id,
                                DateTime theDate,
                                decimal btcUsd,
                                decimal btc,
                                decimal usd
                               )
        {
            System.Text.StringBuilder query = new System.Text.StringBuilder();

            SqlCommand cmd = new SqlCommand();
            query.Append(@"BEGIN
    	                            -- Update existing row if it exists else insert new  row
    	                            IF ((SELECT Count(Id) FROM dollarcostavg  WITH (NOLOCK) WHERE Id = @Id) > 0)
    	                            BEGIN
    
    		                            -- update existing  row
    		                            UPDATE 
    			                            [dollarcostavg]
    		                            SET 
                                            
                                            TheDate = @TheDate,
                                            BTCUSD = @BTCUSD,
                                            BTC = @BTC,
                                            USD = @USD

    		                            WHERE 
    			                            [Id]                    = @Id
    	                            END
    	                            ELSE
    	                            BEGIN
    		                            -- insert new row
    		                            INSERT INTO [dollarcostavg]
    			                            (   
                                                
                                                TheDate,
                                                BTCUSD,
                                                BTC,
                                                USD
                                            )
    		                             VALUES
    			                            (
                                                
                                                @TheDate,
                                                @BTCUSD,
                                                @BTC,
                                                @USD
                                            )

                                            
    
    			                            SELECT @Id = @@Identity FROM [dollarcostavg] WITH (NOLOCK) 

                                            
    
    	                            END
                                       
                                END");

            ExecuteNonQuery("CryptoTrades", out cmd, query.ToString(), CommandType.Text,
                                    CreateParameter("@Id", SqlDbType.BigInt, id, ParameterDirection.InputOutput),
                                    CreateParameter("@TheDate", SqlDbType.DateTime,theDate, ParameterDirection.InputOutput),
                                    CreateParameter("@BTCUSD", SqlDbType.Decimal, btcUsd, ParameterDirection.InputOutput),
                                    CreateParameter("@BTC", SqlDbType.Decimal, btc, ParameterDirection.InputOutput),
                                    CreateParameter("@USD", SqlDbType.Decimal, usd, ParameterDirection.InputOutput)
                            );

            id = int.Parse(cmd.Parameters["@id"].Value.ToString());
        }
    }
}