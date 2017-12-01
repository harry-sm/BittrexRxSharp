using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BittrexRxSharp.Models;
using BittrexRxSharp.ValueTypes;
using BittrexRxSharp.Helpers;

namespace BittrexRxSharp
{
    public class BittrexRxSharpClient
    {
        private readonly HttpClient _httpClient;
        private string apiKey;
        private string apiSecret;
        private string baseUrl = "https://bittrex.com/api/";

        public BittrexRxSharpClient()
        {
            _httpClient = new HttpClient();
        }

        public void ApiCredentials(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        //Public API
        public IObservable<Market[]> GetMarkets()
        {
            return this.publicApiRequest<Market[]>("/public/getmarkets");
        }
        public IObservable<CryptoCurrency[]> GetCurrencies()
        {
            return this.publicApiRequest<CryptoCurrency[]>("/public/getcurrencies");
        }
        public IObservable<Ticker> GetTicker(string market)
        {
            return this.publicApiRequest<Ticker>("/public/getticker", new { market });
        }
        public IObservable<MarketSummary[]> GetMarketSummaries()
        {
            return this.publicApiRequest<MarketSummary[]>("/public/getmarketsummaries");
        }
        public IObservable<MarketSummary> GetMarketSummary(string market)
        {
            return this.publicApiRequest<MarketSummary[]>("/public/getmarketsummary", new { market })
                .Select(arr => arr.FirstOrDefault());
        }
        public IObservable<OrderBook> GetOrderBook(string market)
        {
            return this.publicApiRequest<OrderBook>("/public/getorderbook",
                new { market, type = BookOrderTypeValue.both.ToString() });
        }
        public IObservable<OrderBookOrderItem> GetBuyOrderBook(string market)
        {
            return this.publicApiRequest<OrderBookOrderItem>("/public/getorderbook",
                new { market, type = BookOrderTypeValue.buy.ToString() });
        }
        public IObservable<OrderBookOrderItem> GetSellOrderBook(string market)
        {
            return this.publicApiRequest<OrderBookOrderItem>("/public/getorderbook",
                new { market, type = BookOrderTypeValue.sell.ToString() });
        }
        public IObservable<MarketHistory[]> GetMarketHistory(string market)
        {
            return this.publicApiRequest<MarketHistory[]>("/public/getmarkethistory", new { market });
        }

        // V2
        public IObservable<Candle[]> GetCandles(string market, TickIntervalValue tickInterval)
        {
            // {
            //     marketName: string, //'USDT-BTC'
            //     tickInterval: string; // 'fiveMin' // intervals are keywords
            //     _: Date; // ((new Date()).getTime()/1000)-(300*5) // start timestamp
            // }
            return this.publicApiRequest<Candle[]>("/pub/market/GetTicks",
                new { marketName = market, tickInterval = tickInterval.ToString() }, 2);
        }

        // Account API 

        public IObservable<AccountBalance[]> GetBalances()
        {
            return this.privateApiRequest<AccountBalance[]>("/account/getbalances");
        }
        public IObservable<AccountBalance> GetBalance(string currency)
        {
            return this.privateApiRequest<AccountBalance>("/account/getbalance", new { currency });
        }
        public IObservable<DepositAddress> GetDepositAddress(string currency)
        {
            return this.privateApiRequest<DepositAddress>("/account/getdepositaddress", new { currency });
        }
        public IObservable<Order> GetOrder(string uuid)
        {
            return this.privateApiRequest<Order>("/account/getorder", new { uuid });
        }
        public IObservable<OrderHistory[]> GetOrderHistory()
        {
            return this.privateApiRequest<OrderHistory[]>("/account/getorderhistory");
        }
        public IObservable<Transaction[]> GetWithdrawalHistory(string currency)
        {
            return this.privateApiRequest<Transaction[]>("/account/getwithdrawalhistory", new { currency });
        }
        public IObservable<Transaction[]> GetDepositHistory(string currency)
        {
            return this.privateApiRequest<Transaction[]>("/account/getdeposithistory", new { currency });
        }
        private IObservable<dynamic> Withdraw(string currency, double quantity, string address, string paymentid = null)
        {
            return this.privateApiRequest<dynamic>("/account/withdraw", new { currency, quantity, address, paymentid });
        }

        // Market API

        public IObservable<OrderResult> SetBuyOrder(string market, double quantity, double rate)
        {
            return this.privateApiRequest<OrderResult>("/market/buylimit", new { market, quantity, rate });
        }
        public IObservable<OrderResult> SetSellOrder(string market, double quantity, double rate)
        {
            return this.privateApiRequest<OrderResult>("/market/selllimit", new { market, quantity, rate });
        }
        public IObservable<dynamic> CancelOrder(string uuid)
        {
            return this.privateApiRequest<dynamic>("/market/cancel", new { uuid });
        }
        public IObservable<OpenOrder[]> GetOpenOrders(string market)
        {
            return this.privateApiRequest<OpenOrder[]>("/market/getopenorders", new { market });
        }

        //V2
        public IObservable<ConditionalOrder> SetConditionalBuyOrder(
            string market,
            MarketOrderValue marketOrderType,
            double quantity,
            double rate,
            TimeInEffectValue timeInEffect,
            OrderConditionalTypeValue conditionType,
            double target)
        {
            return this.privateApiRequest<ConditionalOrder>("/key/market/TradeBuy",
                new
                {
                    MarketName = market,
                    orderType = marketOrderType.ToString(),
                    quantity,
                    rate,
                    timeInEffect = timeInEffect.ToString(),
                    conditionType = conditionType.ToString(),
                    target
                }, 2);
        }

        public IObservable<ConditionalOrder> SetTradeSellOrder(
            string market,
            MarketOrderValue marketOrderType,
            double quantity,
            double rate,
            TimeInEffectValue timeInEffect,
            OrderConditionalTypeValue conditionType,
            double target)
        {
            return this.privateApiRequest<ConditionalOrder>("/key/market/TradeSell",
                new
                {
                    MarketName = market,
                    orderType = marketOrderType.ToString(),
                    quantity,
                    rate,
                    timeInEffect = timeInEffect.ToString(),
                    conditionType = conditionType.ToString(),
                    target
                }, 2);
        }

        private async Task<string> AsyncRequest(string url, Boolean useCredentials)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            if (useCredentials)
            {
                var apiSign = GenerateApiSign(url);
                request.Headers.Add("apisign", apiSign);
            }
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        private IObservable<T> DispatchRequest<T>(string url, dynamic queryObject = null, Boolean useCredentials = false)
        {
            if (useCredentials)
            {
                dynamic apiCredientials = new { apikey = this.apiKey, nonce = this.GenerateNonce() };
                queryObject = (queryObject == null) ? apiCredientials : Utilities.MergeObjects(apiCredientials, queryObject);
            }
            url = url + Utilities.GenerateQueryString(queryObject);
            return Observable.Create<T>(subj =>
            {
                try
                {
                    var responseContent = this.AsyncRequest(url, useCredentials);
                    var parse = JsonConvert.DeserializeObject<ApiResponse<T>>(responseContent.Result);

                    if (parse.Success == true)
                        subj.OnNext(parse.Result);
                    else
                    {
                        subj.OnError(new Exception(parse.Message));
                    }
                }
                catch (Exception e) { Console.WriteLine("Error: " + e); }
                return Disposable.Empty;
            });
        }
        private IObservable<T> publicApiRequest<T>(string path, dynamic queryObject = null, int apiVersion = 1)
        {
            string url = this.baseUrl + this.GetApiVersion(apiVersion) + path;
            return this.DispatchRequest<T>(url, queryObject, false);
        }
        private IObservable<T> privateApiRequest<T>(string path, dynamic queryObject = null, int apiVersion = 1)
        {
            string url = this.baseUrl + this.GetApiVersion(apiVersion) + path;
            return this.DispatchRequest<T>(url, queryObject, true);
        }

        private string GetApiVersion(int ver)
        {
            return (ver == 1) ? "v1.1" : "v2.0";
        }
        private String GenerateApiSign(String url)
        {
            var secretBytes = Encoding.ASCII.GetBytes(this.apiSecret);

            using (var hmacsha512 = new HMACSHA512(secretBytes))
            {
                var hashBytes = hmacsha512.ComputeHash(Encoding.ASCII.GetBytes(url));

                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
        private String GenerateNonce()
        {
            long nonce = (long)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
            return nonce.ToString();
        }
    }
}
