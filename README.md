# BittrexRxSharp
BittrexRxSharp is an Reactive library that was built with C# for the [Bittrex](https://bittrex.com/) API which runs on the .NET platform and uses [Rx.NET](https://github.com/Reactive-Extensions/Rx.NET). Please check out the [TypeScript](https://github.com/harry-sm/BittrexRx) version of this library.

## Installing

Install from [NuGet](https://www.nuget.org/packages/BittrexRx/):

`PM> Install-Package BittrexRx -Version 1.0.0`

Include in your project
```c#
using System;
using System.Linq;
using System.Reactive.Linq;
using BittrexRxSharp;
using BittrexRxSharp.Models;
using BittrexRxSharp.ValueTypes;
using BittrexRxSharp.Helpers.Extensions;
```
## Dependencies
- [Newtonsoft](https://www.nuget.org/packages/Newtonsoft.Json/)
- [System.Reactive](https://www.nuget.org/packages/System.Reactive)
- [SignalR Client](https://www.nuget.org/packages/Microsoft.AspNet.SignalR.Client/)



### API Credentials

Sign into your Bittrex account go to settings then API keys and add new key.

The API key has four access permissions they are:
- **READ INFO** - Grants access to read private trade data such as orders, transaction history, balances, etc...
- **TRADE LIMIT** - Grants access to limit order functions, which includes creating and canceling limit buy and sell orders.
- **TRADE MARKET** - Grants access to other order functions, which includes creating and canceling conditional buy and sell orders.
- **WITHDRAW** -  Grants access to the withdraw function which allows for withdrawals to another address. (This function is not available via the public interface of this library.)

**For first time use please set API permissions to READ INFO only**.

```c#
bittrexRx.ApiCredentials("API_KEY", "API_SECRET");
```
### Example
```c#
using System;
using System.Linq;
using System.Reactive.Linq;
using BittrexRxSharp;
using BittrexRxSharp.Models;
using BittrexRxSharp.ValueTypes;
using BittrexRxSharp.Helpers.Extensions;

var bittrexRx = new BittrexRxSharp();

bittrexRx.ApiCredentials("API_KEY", "API_SECRET");

bittrexRx.GetMarkets()
    .Subscribe(
        data => {
            foreach (var market in data) 
            {
                bittrexRx.GetTicker(market.MarketName).Subscribe(tickData => {
                    Console.WriteLine(tickData);
                });
             }
        },
        err => {
            Console.WriteLine("Error: {0}", err);
        },
        () => {
            Console.WriteLine("Completed");
        });
```
#### Response
```json
{ 
    Bid: 0.00000345, 
    Ask: 0.00000347, 
    Last: 0.00000349
}
```

## Observable Extension

### IntervalTime(int seconds)
The IntervalTime operator returns an observable that emits some sequence of data at specified intervals.

#### Example
```c#
using BittrexRxSharp.Helpers.Extensions;

bittrexRx.GetMarkets()
    IntervalTime(5)
    .Subscribe(
        data => {
            foreach (var market in data)
			  Console.WriteLine(market);
        });
```
The example above fetches market data every 5 seconds.

## Enumerable Extensions

### PrintAll()

Print the contents of a Enumerable to the console.

```c#
using BittrexRxSharp.Helpers.Extensions;

bittrexRx.GetMarkets()
    .Subscribe(
        data => {
            data.printAll();
        });
```

### PrintSome(int amount)

Prints the amount of elements specified to the console. Default: 10

```c#
using BittrexRxSharp.Helpers.Extensions;

bittrexRx.GetMarkets()
    .Subscribe(
        data => {
            data.printSome(5);
        });
```

The example above prints out the first 5 elements.



## Public API Method

### bittrexRx.GetMarkets()
Fetches a snapshot of all markets.

#### Parameters

| Parameter | Type | Example |
| --------- | ---- | ------- |
| none      | -    | -       |

#### Return Type
`IObservable<Market[]>`

#### Example
```c#
bittrexRx.GetMarkets()
    .Subscribe(
        data => {
            for (var market of data)
                Console.WriteLine(market);
        });
```

#### Response
```json
[
    {
      "MarketCurrency": "LTC",
      "MarketCurrencyLong": "Litecoin",
      "BaseCurrency": "BTC",
      "BaseCurrencyLong": "Bitcoin",
      "MinTradeSize": 0.02784181,
      "MarketName": "BTC-LTC",
      "IsActive": true,
      "Created": "2014-02-13T00:00:00",
      "Notice": null,
      "IsSponsored": null,
      "LogoUrl": "https://bittrexblobstorage.blob.core.windows.net/public/6defbc41-582d-47a6-bb2e-d0fa88663524.png" 
    },
    ...
]
```

### bittrexRx.GetCurrencies()
Fetches all the market currencies.

#### Parameters

| Parameter | Type | Example |
| --------- | ---- | ------- |
| none      | -    | -       |

#### Return Type
`IObservable<CryptoCurrency[]>`

#### Example
```c#
bittrexRx.GetCurrencies()
    .Subscribe(
        data => {
          foreach (var market in data)
          	 Console.WriteLine(market);
        });
```

#### Response
```json
[
    {
        Currency: "LTC",
        CurrencyLong: "Litecoin",
        MinConfirmation: 6,
        TxFee: 0.01,
        IsActive: true,
        CoinType: "BITCOIN",
        BaseAddress: "LhyLNfBkoKshT7R8Pce6vkB9T2cP2o84hx",
        Notice: null
    },
...
]
```

### bittrexRx.GetTicker(string market)
Fetches the Tick data which consists of the Bid, Ask and Latest Price the market was traded at.

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-LTC" |

#### Return Type
`IObservable<Ticker>`

#### Example
```c#
bittrexRx.GetTicker("BTC-LTC")
    .Subscribe(
        data => {
            console.WriteLine(data);
        });
```

#### Response
```json
{ 
    Bid: 0.00966006, 
    Ask: 0.00967006, 
    Last: 0.00966006 
}
```

### bittrexRx.GetMarketSummaries()
Fetches the summary of each market available.

#### Parameters

| Parameter | Type | Example |
| --------- | ---- | ------- |
| none      | -    | -       |

#### Return Type
`IObservable<MarketSummary[]>`

#### Example
```c#
bittrexRx.GetMarketSummaries()
    .Subscribe(
        data => {
            foreach (var marketSummary in data)
                Console.WriteLine(marketSummary);
        });
```

#### Response
```json
[
    {
        MarketName: "BTC-LTC",
        High: 0.01023899,
        Low: 0.00966416,
        Volume: 79788.80702209,
        Last: 0.00970283,
        BaseVolume: 791.93512777,
        TimeStamp: "2017-10-26T01:52:30.430",
        Bid: 0.00970283,
        Ask: 0.00970683,
        OpenBuyOrders: 2143,
        OpenSellOrders: 12833,
        PrevDay: 0.01020636,
        Created: "2014-02-13T05:00:00.000"
    },
    ...
]
```

### bittrexRx.GetMarketSummary(string market)
Fetches the summary of a specific market.

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-LTC" |

#### Return Type
`IObservable<MarketSummary[]>`

#### Example
```c#
bittrexRx.GetMarketSummary("BTC-LTC")
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{ 
    MarketName: "BTC-LTC",
    High: 0.01023899,
    Low: 0.00966416,
    Volume: 79788.80702209,
    Last: 0.00970283,
    BaseVolume: 791.93512777,
    TimeStamp: "2017-10-26T01:52:30.430",
    Bid: 0.00970283,
    Ask: 0.00970683,
    OpenBuyOrders: 2143,
    OpenSellOrders: 12833,
    PrevDay: 0.01020636,
    Created: "2014-02-13T05:00:00.000" 
}
```

### bittrexRx.GetOrderBook(string market)
Fetches both buy and sell orders from the order book for a specific market. 

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-LTC" |

#### Return Type
`IObservable<OrderBook>`

#### Example
```c#
bittrexRx.GetOrderBook("BTC-LTC")
    .Subscribe(
        data => {
           Console.WriteLine(data);
        });
```

#### Response
```json
{ 
    buy: [
        { Quantity: 0.1, Rate: 0.07059785 },
        ... 
    ],
    sell: [
        { Quantity: 1.9251093, Rate: 0.07068 },
        ...
    ]
}
```

### bittrexRx.GetOrderBuyBook(string market)
Fetches buy orders from the order book for a specific market.

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-LTC" |

#### Return Type
`IObservable<OrderBookOrderItem[]>`

#### Example
```c#
bittrexRx.GetOrderBuyBook("BTC-LTC")
    .Subscribe(
        data => {
           foreach (var orderItem in data)
                Console.WriteLine(orderItem);
        });
```

#### Response
```json
[
    { Quantity: 0.1, Rate: 0.07059785 },
    ... 
]
```

### bittrexRx.GetOrderSellBook(string market)
Fetches sell orders from the order book for a specific market. 

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-LTC" |

#### Return Type
`IObservable<OrderBookOrderItem[]>`

#### Example
```c#
bittrexRx.GetOrderSellBook("BTC-LTC")
    .Subscribe(
        data => {
           foreach (var orderItem in data)
                Console.WriteLine(orderItem);
        });
```

#### Response
```json
[
    { Quantity: 1.9251093, Rate: 0.07068 },
    ... 
]
```

### bittrexRx.GetMarketHistory(string market)
Fetches the latest transactions for a specific market.

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-LTC" |

#### Return Type
`Observable<Model.MarketHistory[]>`

#### Example
```c#
bittrexRx.GetMarketHistory("BTC-LTC")
    .Subscribe(
        data => {
            foreach (var marketHistory in data)
                Console.WriteLine(marketHistory);
        });
```

#### Response
```json
[
    {
        Id: 85963164,
        TimeStamp: "2017-10-26T02:00:10.273",
        Quantity: 3.29091587,
        Price: 0.00973473,
        Total: 0.03203617,
        FillType: "PARTIAL_FILL",
        OrderType: "BUY" 
    },
    ...
]
```

### bittrexRx.GetCandle(string market, TickIntervalValue tickIntervalType)
Fetches the OHLC (Open, High, Low, Close) of a market for a given time period.

> **Note:**  This method relies on the v2 API of Bittrex. Very little is known about this version of the API and is subjected to change without warning!

#### Parameters

| Parameter        | Type              | Example                                  |
| ---------------- | ----------------- | ---------------------------------------- |
| market           | string            | "USDT-BTC"                               |
| tickIntervalType | TickIntervalValue | TickIntervalValue.oneMin, TickIntervalValue.fiveMin, ... |

#### Return Type
`IObservable<Candle[]>`

#### Example
```c#
bittrexRx.GetCandle("BTC-LTC", TickIntervalType.oneMin)
    .Subscribe(
        data => {
            foreach (var candle in data)
                Console.WriteLine(candle);
        });
```

#### Response
```json
[
    { 
        O: 0.01149845,
        H: 0.0115379,
        L: 0.01149845,
        C: 0.0115379,
        V: 46.98461375,
        T: "2017-10-16T03:56:00.000",
        BV: 0.5419376 
    },
    ...
]
```

## Account API Methods

### bittrexRx.GetBalances()
Fetches all your current currency balances.

#### Parameters

| Parameter | Type | Example |
| --------- | ---- | ------- |
| none      | -    | -       |

#### Return Type
`IObservable<AccountBalance[]>`

#### Example
```c#
bittrexRx.GetBalances()
    .Subscribe(
        data => {
            foreach (var balance in data)
                Console.WriteLine(balance);
        });
```

#### Response
```json
[
    { 
        Currency: "LTC",
        Balance: 0.0,
        Available: 0.0,
        Pending: 0.0,
        CryptoAddress: null,
        Requested: false,
        Uuid: null
    },
    ...
]
```


### bittrexRx.GetBalance(string currency)
Fetches the current balance of a specific currency.

#### Parameters

| Parameter | Type   | Example |
| --------- | ------ | ------- |
| currency  | string | "LTC"   |

#### Return Type
`IObservable<AccountBalance>`

#### Example
```c#
bittrexRx.GetBalance("LTC")
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{ 
    Currency: "LTC",
    Balance: 0.0,
    Available: 0.0,
    Pending: 0.0,
    CryptoAddress: null,
    Requested: false,
    Uuid: null 
}
```

### bittrexRx.GetDepositAddress(string currency)
Fetches the deposit address of a specific currency.

#### Parameters

| Parameter | Type   | Example |
| --------- | ------ | ------- |
| currency  | string | "LTC"   |

#### Return Type
`IObservable<DepositAddress>`

#### Example
```c#
bittrexRx.GetBalance("LTC")
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{ 
    Currency: "LTC",
    Address: ""
}
```

### bittrexRx.GetOrder(string uuid)
Fetches an order by a specific identifier.

#### Return Type
`IObservable<Order>`

#### Parameters

| Parameter | Type   | Example                                |
| --------- | ------ | -------------------------------------- |
| uuid      | string | "2968d0f9-2854-48e5-bbbf-18a2b7451140" |

#### Example
```c#
bittrexRx.GetOrder("dc1a6628-7e12-4817-aa16-b5e9860d116c")
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```js
{ 
    AccountId: null,
    OrderUuid: "dc1a6628-7e12-4817-aa16-b5e9860d116c",
    Exchange: "BTC-XVG",
    Type: "LIMIT_BUY",
    Quantity: 326.22641509,
    QuantityRemaining: 0.0,
    Limit: 0.00000159,
    Reserved: 0.00051869,
    ReserveRemaining: 0.00051869,
    CommissionReserved: 0.00000129,
    CommissionReserveRemaining: 0.0,
    CommissionPaid: 0.00000129,
    Price: 0.00051869,
    PricePerUnit: 0.00000158,
    Opened: "2017-09-27T02:47:50.740",
    Closed: "2017-09-27T03:39:30.280",
    IsOpen: false,
    Sentinel: "Invalid Date",
    CancelInitiated: false,
    ImmediateOrCancel: false,
    IsConditional: false,
    Condition: "NONE",
    ConditionTarget: null
}
```

### bittrexRx.GetOrderHistory()
Fetches the total transaction history.

#### Parameters

| Parameter | Type | Example |
| --------- | ---- | ------- |
| none      | -    | -       |

#### Return Type
`IObservable<OrderHistory[]>`

#### Example
```c#
bittrexRx.GetOrderHistory()
    .Subscribe(
        data => {
        forech(var orderHistoryItem in data)
            Console.WriteLine(orderHistoryItem);
        });
```

#### Response
```json
[
    { 
        OrderUuid: "dc1a6628-7e12-4817-aa16-b5e9860d116c",
        Exchange: "BTC-XVG",
        TimeStamp: "2017-09-27T02:47:50.740",
        OrderType: "LIMIT_BUY",
        Limit: 0.00000159,
        Quantity: 326.22641509,
        QuantityRemaining: 0.0,
        Commission: 0.00000129,
        Price: 0.00051869,
        PricePerUnit: 0.00000158,
        IsConditional: false,
        Condition: "NONE",
        ConditionTarget: null,
        ImmediateOrCancel: false,
        Closed: "2017-09-27T03:39:30.280" 
    },
    ...
]
```


### bittrexRx.GetDepositHistory(string currency)
Fetches the deposit records of the currency specified.

#### Parameters

| Parameter | Type   | Example |
| --------- | ------ | ------- |
| currency  | string | "LTC"   |

#### Return Type
`IObservable<Transaction[]>`

#### Example
```c#
bittrexRx.GetDepositHistory("LTC")
    .Subscribe(
        data => {
        	forech(var transactionHistory in data)
                Console.WriteLine(transactionHistory);
        });
```

#### Response
```json
[
    { 
        Id: 26972433,
        Amount: 0.02455098,
        Currency: "BTC",
        Confirmations: 4,
        LastUpdated: "2017-08-16T22:13:47.783",
        TxId: "8aa448a50b06c0e1436e6e000132d721761e54cac365769ec1136a391df44bfc",
        CryptoAddress: "138TtdZkyMU8GMY8tzpZuc7xsqrb4CwrGE"
    },
    ...
]
```

### bittrexRx.GetWithdrawalHistory(string currency)
Fetches the withdrawal records of the currency specified.

#### Parameters

| Parameter | Type   | Example |
| --------- | ------ | ------- |
| currency  | string | "LTC"   |

#### Return Type
`IObservable<Transaction[]>`

#### Example
```c#
bittrexRx.GetWithdrawalHistory("LTC")
    .Subscribe(
        data => {
            forech(var transactionHistory in data)
                Console.WriteLine(transactionHistory);
        });
```

#### Response
```json
[
    {
        PaymentUuid: "b14f86bb-b15b-4177-9779-5466eb3a0fbc",
        Currency: "BTC",
        Amount: 0.02039674,
        Address: Invalid Date,
        Opened: "2017-08-18T08:40:44.737",
        Authorized: true,
        PendingPayment: false,
        TxCost: 0.001,
        TxId: "38a3147f51b8c4798d1a5b3e2712bd7b7177fa99d6457af45a84e56664b6bbc6",
        Canceled: false,
        InvalidAddress: false 
    },
    ...
]
```

## Market API Methods

### bittrexRx.setBuyOrder(string market, double quantity, double rate)
Place buy limit order for a market pair at a rate and quantity specified.

#### Parameters

| Parameter | Type   | Example    |
| --------- | ------ | ---------- |
| market    | string | "BTC-LTC"  |
| quantity  | double | 0.05849296 |
| rate      | double | 0.00869720 |


#### Return Type
`IObservable<OrderResult>`

#### Example
```c#
bittrexRx.SetBuyOrder("BTC-LTC", 0.05849296, 0.00869720)
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{    
    uuid: "54a1cc8f-10dc-49de-bb52-f5d70b1c84ec"
}
```

### bittrexRx.SetSellOrder(string market, double quantity, double rate)
Place sell limit order for a market pair at a rate and quantity specified.

#### Parameters

| Parameter | Type   | Example    |
| --------- | ------ | ---------- |
| market    | string | "USDT-BTC" |
| quantity  | double | 0.0051     |
| rate      | double | 7000       |

#### Return Type
`IObservable<OrderResult>`

#### Example
```c#
bittrexRx.SetSellOrder("USDT-BTC", 0.0051, 7000)
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{
    uuid: "2968d0f9-2854-48e5-bbbf-18a2b7451140"
}
```

### bittrexRx.GetOpenOrders(string market)
Fetch orders that has not been executed for specific market.

#### Parameters

| Parameter | Type   | Example   |
| --------- | ------ | --------- |
| market    | string | "BTC-GNT" |

#### Return Type
`IObservable<OpenOrder[]>`

#### Example
```c#
bittrexRx.GetOpenOrders("BTC-GNT")
    .Subscribe(
        data => {
          foreach(var openOrder in data)
             Console.WriteLine(openOrder);
        });
```

#### Response
```json
[
    { 
        Uuid: null,
        OrderUuid: "9a6e6f63-de19-475a-ad81-c85129681253",
        Exchange: "BTC-GNT",
        OrderType: "LIMIT_BUY",
        Quantity: 16.14487464,
        QuantityRemaining: 16.14487464,
        Limit: 0.00003151,
        CommissionPaid: 0,
        Price: 0,
        PricePerUnit: null,
        Opened: "2017-10-24T03:50:26.250Z",
        Closed: null,
        CancelInitiated: false,
        ImmediateOrCancel: false,
        IsConditional: false,
        Condition: "NONE",
        ConditionTarget: null 
    },
  ...
]
```

### bittrexRx.CancelOrder(string uuid)
Cancel order returns null.

#### Parameters

| Parameter | Type   | Example                                |
| --------- | ------ | -------------------------------------- |
| uuid      | string | "2968d0f9-2854-48e5-bbbf-18a2b7451140" |

#### Return Type
`IObservable<void>`

#### Example
```c#
bittrexRx.CancelOrder("54a1cc8f-10dc-49de-bb52-f5d70b1c84ec")
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
null
```

### bittrexRx.SetConditionalBuyOrder(string market, MarketOrderValue marketOrderType, double quantity, double rate, TimeInEffectValue timeInEffect, OrderConditionalTypeValue conditionType, double target)
Executes buy orders under the conditions specified.

> **Note:**  This method relies on the v2 API of Bittrex. Very little is known about this version of the API and is subjected to change without warning!

#### Parameter

| Parameter       | Type                      | Example                                  | Description                              |
| --------------- | ------------------------- | ---------------------------------------- | ---------------------------------------- |
| market          | string                    | "BTC-ETH"                                |                                          |
| marketOrderType | MarketOrderValue          | MarketOrderValue.LIMIT                   | LIMIT: The order will be executed at a specific price. |
| quantity        | number                    | 0.01162237                               | -                                        |
| rate            | number                    | 0.04377120                               | -                                        |
| timeInEffect    | TimeInEffectValue         | TimeInEffectValue.IMMEDIATE_OR_CANCEL,<br> TimeInEffectValue.GOOD_TIL_CANCELLED, <br>TimeInEffectValue.FILL_OR_KILL | IMMEDIATE_OR_CANCEL: The order must be executed immediately or else it is canceled. Partial fills are accepted .<br> GOOD_TIL_CANCELLED:,The order is placed until the user cancels it. <br>FILL_OR_KILL: The order must be completed in its entirety. The full quantity of the order at a fixed prices must be executed or canceled. |
| conditionType   | OrderConditionalTypeValue | OrderConditionalTypeValue.NONE, <br>OrderConditionalTypeValue.GREATER_THAN,<br> OrderConditionalTypeValue.LESS_THAN | GREATER_THAN: The order will be executed if the price of the security is greater than the price specified in the target parameter. <br>LESS_THAN: The order will be executed if the price of the security is less than the price specified in the target parameter. |
| target          | number                    | 0.0                                      | -                                        |


#### Return Type
`IObservable<ConditionalOrder>`

#### Example
```c#
bittrexRx.SetConditionalBuyOrder("BTC-ETH", MarketOrderValue.LIMIT, 0.01162237, 0.04377120, TimeInEffectValue.GOOD_TIL_CANCELLED, OrderConditionalTypeValue.NONE, 0.0)
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{   
    OrderId: "ac983afd-6852-478e-8415-d6e30615ea9c",
    MarketName: "BTC-ETH",
    MarketCurrency: "ETH",
    BuyOrSell: "Buy",
    OrderType: "LIMIT",
    Quantity: 0.01162237,
    Rate: 0.0437712
}
```

### bittrexRx.SetConditionalSellOrder(string market, MarketOrderValue marketOrderType, double quantity, double rate, TimeInEffectValue timeInEffect, OrderConditionalTypeValue conditionType, double target)
Executes sell orders under the conditions specified.

> **Note:**  This method relies on the v2 API of Bittrex. Very little is known about this version of the API and is subjected to change without warning!

#### Parameter

| Parameter       | Type                      | Example                                  | Description                              |
| --------------- | ------------------------- | ---------------------------------------- | ---------------------------------------- |
| market          | string                    | "USDT-ETH"                               |                                          |
| marketOrderType | MarketOrderValue          | MarketOrderValue.LIMIT                   | LIMIT: The order will be executed at a specific price. |
| quantity        | number                    | 0.01574783                               | -                                        |
| rate            | number                    | 400                                      | -                                        |
| timeInEffect    | TimeInEffectValue         | TimeInEffectValue.IMMEDIATE_OR_CANCEL,<br> TimeInEffectValue.GOOD_TIL_CANCELLED, <br>TimeInEffectValue.FILL_OR_KILL | IMMEDIATE_OR_CANCEL: The order must be executed immediately or else it is canceled. Partial fills are accepted .<br> GOOD_TIL_CANCELLED:,The order is placed until the user cancels it. <br>FILL_OR_KILL: The order must be completed in its entirety. The full quantity of the order at a fixed prices must be executed or canceled. |
| conditionType   | OrderConditionalTypeValue | OrderConditionalTypeValue.NONE, <br>OrderConditionalTypeValue.GREATER_THAN,<br> OrderConditionalTypeValue.LESS_THAN | GREATER_THAN: The order will be executed if the price of the security is greater than the price specified in the target parameter. <br>LESS_THAN: The order will be executed if the price of the security is less than the price specified in the target parameter. |
| target          | number                    | 0.0                                      | -                                        |


#### Return Type
`IObservable<ConditionalOrder>`

#### Example
```c#
bittrexRx.setConditionalSellOrder("BTC-ETH", MarketOrderValue.LIMIT, 0.01162237, 0.04377120, TimeInEffectValue.GOOD_TIL_CANCELLED,  OrderConditionalTypeValue.NONE, 0.0)
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
{    
    OrderId: "b27a6b86-bae6-4b04-be2d-6726e717e53e",
    MarketName: "USDT-ETH",
    MarketCurrency: "ETH",
    BuyOrSell: "Sell",
    OrderType: "LIMIT",
    Quantity: 0.01574783,
    Rate: 400
} 
```


### customRequest(url: string, queryOptions: Object, useCredentials: Boolean)
This method is not dependent on the API version and allows for the sending of custom requests.

#### Parameters

| Parameter      | Type    | Example                                  | Description                              |
| -------------- | ------- | ---------------------------------------- | ---------------------------------------- |
| url            | string  | https://bittrex.com/api/v1.1/public/getmarketsummary | API endpoint.                            |
| queryOptions   | Object  | { market: "BTC-LTC" }                    | Query string parameters.                 |
| useCredentials | Boolean | false                                    | Specify whether the API credentials should be enabled or not. |

#### Return Type
`IObservable<dynamic>`

#### Example
```c#
bittrexRx.customRequest("https://bittrex.com/api/v1.1/public/getmarketsummary", { market: "BTC-LTC" }, false)
    .Subscribe(
        data => {
            Console.WriteLine(data);
        });
```

#### Response
```json
[ 
    { 
        MarketName: "BTC-LTC",
        High: 0.00908,
        Low: 0.0076,
        Volume: 291758.48361243,
        Last: 0.0084773,
        BaseVolume: 2464.80235543,
        TimeStamp: "2017-11-08T00:32:02.203",
        Bid: 0.00846202,
        Ask: 0.0084773,
        OpenBuyOrders: 2964,
        OpenSellOrders: 13074,
        PrevDay: 0.00775,
        Created: "2014-02-13T00:00:00" 
    } 
]
```
