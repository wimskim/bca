using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace CryptoTrader
{

    public enum LunoSymbolEnum
    {
        XBTZAR = 1,
        ETHZAR = 2,
        
    }


    public static class LunoApi
    {
        public static BestLunohBidAsk GetLunoResult(decimal minVolume, LunoSymbolEnum symbol)
        {
            if (symbol == LunoSymbolEnum.XBTZAR)
            {
                var result = new LunoResult();

                var client = new RestClient("https://api.mybitx.com/api/1/orderbook?pair=" + symbol.ToString());
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.Content.Length < 10)
                {
                    return GetLunoResult(minVolume, symbol);
                }

                var lunoResponse = JsonConvert.DeserializeObject<LunoResponse>(response.Content);



                try
                {
                    lunoResponse = JsonConvert.DeserializeObject<LunoResponse>(response.Content);

                    BestLunohBidAsk bestprices = new BestLunohBidAsk();

                    int i = 0;
                    do
                    {
                        bestprices.ask.price = lunoResponse.asks[i].price;
                        bestprices.ask.volume += lunoResponse.asks[i].volume;
                        i++;
                    } while (bestprices.ask.volume < minVolume);

                    i = 0;
                    do
                    {
                        bestprices.bid.price = lunoResponse.bids[i].price;
                        bestprices.bid.volume += lunoResponse.bids[i].volume;
                        i++;
                    }
                    while (bestprices.bid.volume < minVolume);

                    return bestprices;
                }
                catch
                {
                    return new BestLunohBidAsk();
                }
            }
            else
            {
                BestLunohBidAsk bestprices = new BestLunohBidAsk();
                bestprices.ask.price = 4959M;
                bestprices.ask.volume = 4959M;

                bestprices.bid.price = 4959M;
                bestprices.ask.volume = 4959M;

                return bestprices;
            }
        }
    }

    public class LunoResult
    {
        public LunoItem BestBid { get; set; }
        public LunoItem BestAsk { get; set; }
    }


    public class LunoResponse
    {
        public List<LunoItem> asks { get; set; }
        public List<LunoItem> bids { get; set; }
    }
    public class BestLunohBidAsk
    {
        public BestLunohBidAsk()
        {
            bid = new LunoItem();
            ask = new LunoItem();
        }
        public LunoItem bid { get; set; }
        public LunoItem ask { get; set; }
    }

    public class LunoItem
    {
        public decimal price { get; set; }
        public decimal volume { get; set; }
    }
}
