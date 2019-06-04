using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.Caching;

namespace CryptoTrader
{
    public static class CurrencyHelper
    {

        public enum CurrencyEnum
        {
            Usd = 1,
            Eur = 2,
            Rub = 3,
            Gbp = 4
        }

        private class ExchangeRate
        {
            public string to { get; set; }
            public decimal rate { get; set; }
            public string from { get; set; }

            //{"to": "ZAR", "rate": 9.9872163599999997, "from": "USD"}
        }

        /// <summary>
        /// This will first try and get a cached currency conversion rate, else it will try and get the excange rate from Yahoo, else it will fallback to webservicex.net
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="fromCurrencyCode"></param>
        /// <param name="roundUp"></param>
        /// <returns></returns>
        public static decimal ConvertToZAR(decimal amount, CurrencyEnum fromCurrency)
        {
            string fromCurrencyCode = Enum.GetName(typeof(CurrencyEnum), fromCurrency);

            const string toCurrency = "ZAR";

            
            ObjectCache cache = MemoryCache.Default;
            string strCacheName = string.Format("EXCHANGERATE_{0}", fromCurrencyCode);

            if (cache[strCacheName] == null)
            {
                ExchangeRate exchangeRate;
                try
                {
                    exchangeRate = GetRateExchangeCurrencyConversion(fromCurrencyCode, toCurrency);
                    if (exchangeRate.rate <= 0)
                        throw new Exception("GetRateExchangeCurrencyConversion returned exchange rate as 0");

                }
                catch (Exception)
                {
                    try
                    {
                        // fall back to Yahoo Currency api
                        exchangeRate = GetYahooCurrencyConversion(fromCurrencyCode, toCurrency);
                        if (exchangeRate.rate <= 0)
                            throw new Exception("GetYahooCurrencyConversion returned exchange rate as 0");
                    }
                    catch (Exception)
                    {
                        // fall back to RateExchange service
                        exchangeRate = GetWebserviceXCurrencyConversion(fromCurrencyCode, toCurrency);
                        if (exchangeRate.rate <= 0)
                        {
                            throw new Exception("Critical error - all currency exchange services failed!");
                        }
                    }
                }

                var zarAmound = amount * exchangeRate.rate;

                if (cache != null)
                    cache.Set(strCacheName, zarAmound, DateTime.Now.AddMinutes(5));
            }

            return (decimal)cache[strCacheName];
            
        }

        private static ExchangeRate GetYahooCurrencyConversion(string fromCurrency, string toCurrency)
        {
            var exchangeRate = new ExchangeRate()
            {
                from = fromCurrency,
                to = toCurrency,
                rate = 0
            };

            var apiUrl = string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=sl1d1t1ba&e=.csv", fromCurrency, toCurrency);
            var webRequest = (HttpWebRequest)WebRequest.Create(apiUrl);

            // else call the exchange rate api
            var result = "";
            GetResponse(webRequest, ref result);

            // Example format: "USDZAR=X",10.1562,"11/5/2013","2:22am",10.1541,10.1583
            if (string.IsNullOrWhiteSpace(result))
            {
                return exchangeRate;
            }

            var resultList = result.Split(',').ToList();
            if (resultList.Count <= 0)
            {
                return exchangeRate;
            }

            var strAmount = resultList[1];
            {
               // exchangeRate.rate = Convert.ToDecimal(strAmount);
                exchangeRate.rate = Decimal.Parse(strAmount, NumberStyles.Currency, CultureInfo.InvariantCulture); //yields 15.55
            }

            return exchangeRate;
        }


        private static ExchangeRate GetWebserviceXCurrencyConversion(string fromCurrency, string toCurrency)
        {
            var exchangeRate = new ExchangeRate()
            {
                from = fromCurrency,
                to = toCurrency,
                rate = 0
            };

            var apiUrl = string.Format("http://www.webservicex.net/currencyconvertor.asmx/ConversionRate?FromCurrency={0}&ToCurrency={1}", fromCurrency, toCurrency);
            var webRequest = (HttpWebRequest)WebRequest.Create(apiUrl);

            // else call the exchange rate api
            var result = "";
            GetResponse(webRequest, ref result);

            // Example format: <double xmlns="http://www.webserviceX.NET/">10.1667</double>
            if (string.IsNullOrWhiteSpace(result))
            {
                return exchangeRate;
            }

            var xmlDoc = new XmlDocument();
            {
                xmlDoc.LoadXml(result);
            }

            var node = xmlDoc.SelectSingleNode("/");
            if (node == null)
            {
                return exchangeRate;
            }

            var strAmount = node.InnerText;
            {
                exchangeRate.rate = Decimal.Parse(strAmount, NumberStyles.Currency, CultureInfo.InvariantCulture); //yields 15.55
            }
            return exchangeRate;
        }

        private static ExchangeRate GetRateExchangeCurrencyConversion(string fromCurrency, string toCurrency)
        {
            var apiUrl = string.Format("http://free.currencyconverterapi.com/api/v4/convert?q={0}_{1}&compact=ultra&apiKey=468f01b41e1bd00b7fca", fromCurrency.ToUpper(), toCurrency.ToUpper());
            var webRequest = (HttpWebRequest)WebRequest.Create(apiUrl);

            // else call the exchange rate api
            var result = "";
            GetResponse(webRequest, ref result);

            if (!string.IsNullOrWhiteSpace(result))
            {

                var exchangeRate = new ExchangeRate()
                {
                    from = fromCurrency,
                    to = toCurrency,
                    rate = 0
                };
                
                dynamic jObj = JsonConvert.DeserializeObject(result);
                
                //foreach (var child in jObj.First.Value)
                //{
                    // Console.WriteLine("Item ID: {0}", child.First.id);
                    //Console.WriteLine("Item VAL: {0}", child.First.val);
                    exchangeRate.rate = jObj[fromCurrency.ToUpper() + "_" + toCurrency.ToUpper()];
                //}

                return exchangeRate;
            }
            return null;
        }


        private static void GetResponse(WebRequest objRequest, ref string result)   
        {
            try
            {
                using (var webResponse = (HttpWebResponse)objRequest.GetResponse())
                {
                    // Get the valid response stream
                    using (var stream = webResponse.GetResponseStream())
                    {
                        if (stream == null) throw new NullReferenceException("CurrencyHelper: webResponse.GetResponseStream() returned null...");

                        // Read the resulting stream into a response
                        using (var sr = new StreamReader(stream))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
