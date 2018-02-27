using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Coinigy.API
{
    public partial class CoinigyApi
    {
        //https://lisk.io/documentation?i=lisk-docs/APIReference
        //Author: Allen Byron Penner
        //TODO: fix the way errors are handled in http methods

        public enum MarketDataType { history, asks, bids, orders, all, COUNT }

        public CoinigyApi(string api_key, string api_secret) : this(api_key, api_secret, DefaultBaseUrl, DefaultUserAgent) { }
        public CoinigyApi(string api_key, string api_secret, string serverBaseUrl, string userAgent)
        {
            User_Agent = userAgent;
            Server_Url = serverBaseUrl;
            _key = api_key;
            Api_Secret = api_secret;
        }

        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class BaseResponse
        {
            public string err_msg;
            public string err_num;
        }

        /// <summary>
        /// Accounts at Coinigy
        /// </summary>
        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class Accounts : BaseResponse
        {
            public List<ExchangeAccount> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class ExchangeAccount
            {
                public string auth_active;
                public string auth_id;
                public string auth_key;
                public string auth_nickname;
                public string auth_optional1;
                public string auth_secret;
                public string auth_trade;
                public string auth_updated;
                public string exch_id;
                public string exch_name;
                public string exch_trade_enabled;
            }
        }

        /// <summary>
        /// Accounts
        /// </summary>
        public Accounts Accts => GetAccounts();
        private static readonly string _accounts = "accounts";
        public Accounts GetAccounts() => Request<Accounts>(_accounts);
        public Task<Accounts> GetAsync() => RequestAsync<Accounts>(_accounts);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class Activity : BaseResponse
        {
            public List<ActivityNotification> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class ActivityNotification
            {
                public string notification_style;
                public string notification_time_added;
                public string notification_title_vars;
                public string notification_type_message;
                public string notification_type_title;
                public string notification_vars;
            }
        }

        /// <summary>
        /// Activity
        /// </summary>
        public Activity AcctActivity => GetActivity();
        private static readonly string _activity = "activity";
        public Activity GetActivity() => Request<Activity>(_activity);
        public Task<Activity> GetActivityAsync() => RequestAsync<Activity>(_activity);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class Alerts : BaseResponse
        {
            public AlertData data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class AlertData
            {
                public List<AlertHistory> alert_history;
                public List<OpenAlert> open_alerts;

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class AlertHistory
                {
                    public string alert_history_id;
                    public string alert_note;
                    public string alert_price;
                    public string display_name;
                    public string exch_name;
                    public string mkt_name;
                    public string @operator;
                    public string operator_text;
                    public string price;
                    public string timestamp;
                }

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class OpenAlert
                {
                    public string alert_added;
                    public string alert_id;
                    public string alert_note;
                    public string display_name;
                    public string exch_code;
                    public string exch_name;
                    public string mkt_name;
                    public string @operator;
                    public string operator_text;
                    public string price;
                }
            }
        }

        public Alerts PriceAlerts => GetAlerts();
        private static readonly string _alerts = "alerts";
        public Alerts GetAlerts() => Request<Alerts>(_alerts);
        public Task<Alerts> GetAlertsAsync() => RequestAsync<Alerts>(_alerts);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class Balance
        {
            public string balance_amount_avail;
            public string balance_amount_held;
            public string balance_amount_total;
            public string balance_curr_code;
            public string btc_balance;
            public string last_price;
        }

        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class Balances
        {
            public List<Balance> data;
            public List<object> notifications;
        }

        public Balances Bals => GetBalances();
        private static readonly string _balances = "balances";
        public Balances GetBalances() => GetBalances(false);
        public Balances GetBalances(bool show_nils) => GetBalances(show_nils, "");
        public Balances GetBalances(bool show_nils, string auth_ids)
            => Request<Balances>(_balances, new KeyValuePair<string, string>[] { KVP("show_nils", show_nils), KVP("auth_ids", auth_ids) });
        public Task<Balances> GetBalancesAsync() => GetBalancesAsync(false);
        public Task<Balances> GetBalancesAsync(bool show_nils) => GetBalancesAsync(show_nils, "");
        public Task<Balances> GetBalancesAsync(bool show_nils, string auth_ids)
            => RequestAsync<Balances>(_balances, new KeyValuePair<string, string>[] { KVP("show_nils", show_nils), KVP("auth_ids", auth_ids) });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class BalanceHistory : BaseResponse
        {
            public BalanceHistoryList data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class BalanceHistoryList
            {
                public List<BalanceHistory> balance_history;

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class BalanceHistory
                {
                    public string auth_id;
                    public string balance_amount_avail;
                    public string balance_amount_held;
                    public string balance_amount_total;
                    public string balance_curr_code;
                    public string balance_date;
                    public string btc_value;
                    public string timestamp;
                }
            }
        }

        public BalanceHistory BalHist => GetBalanceHistory();
        private static readonly string _balanceHistory = "balanceHistory";
        public BalanceHistory GetBalanceHistory() => GetBalanceHistory(null);
        public BalanceHistory GetBalanceHistory(string date) => Request<BalanceHistory>(_balanceHistory, KVP("date", GetDate(date)));
        public Task<BalanceHistory> GetBalanceHistoryAsync() => GetBalanceHistoryAsync(null);
        public Task<BalanceHistory> GetBalanceHistoryAsync(string date) => RequestAsync<BalanceHistory>(_balanceHistory, KVP("date", GetDate(date)));


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class UserWatchList : BaseResponse
        {
            public List<WatchedItem> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class WatchedItem
            {
                public string btc_volume;
                public string current_volume;
                public string exch_code;
                public string exch_name;
                public string exchmkt_id;
                public string fiat_market;
                public string high_trade;
                public string last_price;
                public string low_trade;
                public string mkt_name;
                public string prev_price;
                public string primary_currency_name;
                public string secondary_currency_name;
                public string server_time;
            }
        }

        public UserWatchList WatchList => GetUserWatchList();
        private static readonly string _userWatchList = "userWatchList";
        public UserWatchList GetUserWatchList() => Request<UserWatchList>(_userWatchList);
        public Task<UserWatchList> GetUserWatchListAsync() => RequestAsync<UserWatchList>(_userWatchList);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class OrdersResponse : BaseResponse
        {
            public Orders data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class Orders
            {
                public List<OpenOrder> open_orders;
                public List<OrderHistory> order_history;

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class OpenOrder
                {
                    public string auth_id;
                    public string auth_nickname;
                    public string display_name;
                    public string exch_code;
                    public string exch_name;
                    public string foreign_order_id;
                    public string limit_price;
                    public string mkt_name;
                    public string @operator;
                    public string order_id;
                    public string order_price_type;
                    public string order_status;
                    public string order_time;
                    public string order_type;
                    public string price_type_id;
                    public string quantity;
                    public string quantity_remaining;
                    public string stop_price;
                }

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class OrderHistory
                {
                    public string auth_id;
                    public string auth_nickname;
                    public string display_name;
                    public string exch_code;
                    public string exch_id;
                    public string exch_name;
                    public string executed_price;
                    public string last_updated;
                    public string limit_price;
                    public string mkt_name;
                    public string order_id;
                    public string order_price_type;
                    public string order_status;
                    public string order_time;
                    public string order_type;
                    public string quantity;
                    public string quantity_remaining;
                    public string unixtime;
                }
            }
        }

        public OrdersResponse Orders => GetOrders();
        private static readonly string _orders = "orders";
        public OrdersResponse GetOrders() => Request<OrdersResponse>(_orders);
        public Task<OrdersResponse> GetOrdersAsync() => RequestAsync<OrdersResponse>(_orders);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class NewsFeed : BaseResponse
        {
            public List<NewsItem> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class NewsItem
            {
                public string date_field;
                public string feed_description;
                public string feed_enabled;
                public string feed_id;
                public string feed_image;
                public string feed_name;
                public string feed_url;
                public string id;
                public string pubDate;
                public string published_date;
                public string timestamp;
                public string title;
                public string title_field;
                public string url;
                public string url_field;
            }
        }

        public NewsFeed News => GetNewsFeed();
        private static readonly string _newsFeed = "newsFeed";
        public NewsFeed GetNewsFeed() => Request<NewsFeed>(_newsFeed);
        public Task<NewsFeed> GetNewsFeedAsync() => RequestAsync<NewsFeed>(_newsFeed);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class OrderTypesResponse : BaseResponse
        {
            public OrderTypes data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class OrderTypes
            {
                public List<OrderType> order_types;
                public List<PriceType> price_types;

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class OrderType
                {
                    public string name;
                    public string order;
                    public string order_type_id;
                }

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class PriceType
                {
                    public string name;
                    public string order;
                    public string price_type_id;
                }
            }
        }

        public OrderTypesResponse OrderTypes => GetOrderTypes();
        private static readonly string _orderTypes = "orderTypes";
        public OrderTypesResponse GetOrderTypes() => Request<OrderTypesResponse>(_orderTypes);
        public Task<OrderTypesResponse> GetOrderTypesAsync() => RequestAsync<OrderTypesResponse>(_orderTypes);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class UserData : BaseResponse
        {
            public UserInfo data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class UserInfo
            {
                public string active;
                public string chat_enabled;
                public string chat_nick;
                public string city;
                public string company;
                public string country;
                public string created_on;
                public string custom_ticker;
                public string email;
                public string first_name;
                public string last_active;
                public string last_login;
                public string last_name;
                public string newsletter;
                public string phone;
                public string pref_alert_email;
                public string pref_alert_mobile;
                public string pref_alert_sms;
                public bool pref_app_device_id;
                public string pref_balance_email;
                public string pref_referral_code;
                public string pref_subscription_expires;
                public string pref_trade_email;
                public string pref_trade_mobile;
                public string pref_trade_sms;
                public string referral_balance;
                public string state;
                public string street1;
                public string street2;
                public string subscription_status;
                public string ticker_enabled;
                public string ticker_indicator_time_type;
                public string two_factor;
                public string zip;
            }
        }

        public UserData UserInfo => GetUserInfo();
        private static readonly string _userInfo = "userInfo";
        public UserData GetUserInfo() => Request<UserData>(_userInfo);
        public Task<UserData> GetAsync(CoinigyApi api) => api.RequestAsync<UserData>(_userInfo);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class Notification
        {
            public string notification_id;
            public string notification_pinned;
            public string notification_sound;
            public string notification_sound_id;
            public string notification_style;
            public string notification_title_vars;
            public string notification_type_message;
            public string notification_type_title;
            public string notification_vars;
        }

        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class UpdateUserResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;

        }

        private static readonly string _updateUser = "updateUser";
        public UpdateUserResponse UpdateUser(string first_name, string last_name, string company, string phone,
            string street1, string street2, string city, string state, string zip, string country)
            => Request<UpdateUserResponse>(_updateUser, new KeyValuePair<string, string>[] {
                KVP("first_name", first_name),
                KVP("last_name", last_name),
                KVP("company", company),
                KVP("phone", phone),
                KVP("street1", street1),
                KVP("street2", street2),
                KVP("city", city),
                KVP("state", state),
                KVP("zip", zip),
                KVP("country", country)
            });
        public Task<UpdateUserResponse> UpdateUserAsync(string first_name, string last_name, string company,
            string phone, string street1, string street2, string city, string state, string zip, string country)
            => RequestAsync<UpdateUserResponse>(_updateUser, new KeyValuePair<string, string>[] {
                KVP("first_name", first_name),
                KVP("last_name", last_name),
                KVP("company", company),
                KVP("phone", phone),
                KVP("street1", street1),
                KVP("street2", street2),
                KVP("city", city),
                KVP("state", state),
                KVP("zip", zip),
                KVP("country", country)
            });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class UpdatePrefsResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _updatePrefs = "updatePrefs";
        public UpdatePrefsResponse UpdatePrefs(bool alert_email, bool alert_sms, bool trade_email, bool trade_sms, bool balance_email)
            => Request<UpdatePrefsResponse>(_updatePrefs, new KeyValuePair<string, string>[] {
                KVP("alert_email", alert_email),
                KVP("alert_sms", alert_sms),
                KVP("trade_email", trade_email),
                KVP("trade_sms", trade_sms),
                KVP("balance_email", balance_email)
            });
        public Task<UpdatePrefsResponse> UpdatePrefsAsync(bool alert_email, bool alert_sms, bool trade_email, bool trade_sms, bool balance_email)
            => RequestAsync<UpdatePrefsResponse>(_updatePrefs, new KeyValuePair<string, string>[] {
                KVP("alert_email", alert_email),
                KVP("alert_sms", alert_sms),
                KVP("trade_email", trade_email),
                KVP("trade_sms", trade_sms),
                KVP("balance_email", balance_email)
            });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class UpdateTickersResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _updateTickers = "updateTickers";
        public UpdateTickersResponse UpdateTickers(string exch_mkt_ids) => Request<UpdateTickersResponse>(_updateTickers, KVP("exch_mkt_ids", exch_mkt_ids));
        public Task<UpdateTickersResponse> UpdateTickersAsync(string exch_mkt_ids) => RequestAsync<UpdateTickersResponse>(_updateTickers, KVP("exch_mkt_ids", exch_mkt_ids));


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class RefreshBalanceResponse : BaseResponse
        {
            public List<Balance> data;
            public List<object> notifications;
        }

        private static readonly string _refreshBalance = "refreshBalance";
        public RefreshBalanceResponse RefreshBalance(string auth_id) => Request<RefreshBalanceResponse>(_refreshBalance, KVP("auth_id", auth_id));
        public Task<RefreshBalanceResponse> RefreshBalanceAsync(string auth_id) => RequestAsync<RefreshBalanceResponse>(_refreshBalance, KVP("auth_id", auth_id));


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class AddAlertResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _addAlert = "addAlert";
        public AddAlertResponse AddAlert(string exch_code, string market_name, string alert_price) => AddAlert(exch_code, market_name, alert_price, "");
        public AddAlertResponse AddAlert(string exch_code, string market_name, string alert_price, string alert_note) => Request<AddAlertResponse>(_addAlert,
            new KeyValuePair<string, string>[] {
                KVP("exch_code", exch_code),
                KVP("market_name", market_name),
                KVP("alert_price", alert_price),
                KVP("alert_note", alert_note)
            });
        public Task<AddAlertResponse> AddAlertAsync(string exch_code, string market_name, string alert_price) => AddAlertAsync(exch_code, market_name, alert_price, "");
        public Task<AddAlertResponse> AddAlertAsync(string exch_code, string market_name, string alert_price, string alert_note) => RequestAsync<AddAlertResponse>(_addAlert,
            new KeyValuePair<string, string>[] {
                KVP("exch_code", exch_code),
                KVP("market_name", market_name),
                KVP("alert_price", alert_price),
                KVP("alert_note", alert_note)
            });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class DeleteAlertResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _deleteAlert = "deleteAlert";
        public DeleteAlertResponse DeleteAlert(string alert_id) => Request<DeleteAlertResponse>(_deleteAlert, KVP("alert_id", alert_id));
        public Task<DeleteAlertResponse> DeleteAlertAsync(string alert_id) => RequestAsync<DeleteAlertResponse>(_deleteAlert, KVP("alert_id", alert_id));


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class AddApiKeyResponse : BaseResponse
        {
            public long data;
            public List<Notification> notifications;
        }

        private static readonly string _addApiKey = "addApiKey";
        public AddApiKeyResponse AddApiKey(string api_key, string api_secret, string api_exch_id, string api_nickname) => Request<AddApiKeyResponse>(_addApiKey,
            new KeyValuePair<string, string>[] {
                KVP("api_key", api_key),
                KVP("api_secret", api_secret),
                KVP("api_exch_id", api_exch_id),
                KVP("api_nickname", api_nickname)
            });
        public Task<AddApiKeyResponse> AddApiKeyAsync(string api_key, string api_secret, string api_exch_id, string api_nickname) => RequestAsync<AddApiKeyResponse>(_addApiKey,
            new KeyValuePair<string, string>[] {
                KVP("api_key", api_key),
                KVP("api_secret", api_secret),
                KVP("api_exch_id", api_exch_id),
                KVP("api_nickname", api_nickname)
            });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class DeleteApiKeyResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _deleteApiKey = "deleteApiKey";
        public DeleteApiKeyResponse DeleteApiKey(string auth_id) => Request<DeleteApiKeyResponse>(_deleteApiKey, KVP("auth_id", auth_id));
        public Task<DeleteApiKeyResponse> DeleteApiKeyAsync(string auth_id) => RequestAsync<DeleteApiKeyResponse>(_deleteApiKey, KVP("auth_id", auth_id));


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class ActivateApiKey : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _enApiKey = "activateApiKey";
        public ActivateApiKey EnableApiKey(string auth_id) => EnableApiKey(auth_id, true);
        public ActivateApiKey EnableApiKey(string auth_id, bool auth_active) => Request<ActivateApiKey>(_enApiKey,
            new KeyValuePair<string, string>[] { KVP("auth_id", auth_id), KVP("auth_active", auth_id) });
        public Task<ActivateApiKey> ActivateApiKeyAsync(string auth_id) => ActivateApiKeyAsync(auth_id, true);
        public Task<ActivateApiKey> ActivateApiKeyAsync(string auth_id, bool auth_active) => RequestAsync<ActivateApiKey>(_enApiKey,
            new KeyValuePair<string, string>[] { KVP("auth_id", auth_id), KVP("auth_active", auth_id) });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class ActivateTradingKeyResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _activateTradingKey = "activateTradingKey";
        public ActivateTradingKeyResponse ActivateTradingKey(string auth_id) => ActivateTradingKey(auth_id, true);
        public ActivateTradingKeyResponse ActivateTradingKey(string auth_id, bool auth_trade) => Request<ActivateTradingKeyResponse>(_activateTradingKey,
            new KeyValuePair<string, string>[] { KVP("auth_id", auth_id), KVP("auth_trade", Convert.ToInt32(auth_id)) });
        public Task<ActivateTradingKeyResponse> ActivateTradingKeyAsync(string auth_id) => ActivateTradingKeyAsync(auth_id, true);
        public Task<ActivateTradingKeyResponse> ActivateTradingKeyAsync(string auth_id, bool auth_trade) => RequestAsync<ActivateTradingKeyResponse>(_activateTradingKey,
            new KeyValuePair<string, string>[] { KVP("auth_id", auth_id), KVP("auth_trade", Convert.ToInt32(auth_id)) });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class AddOrderResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _addOrder = "addOrder";
        public AddOrderResponse AddOrder(int auth_id, int exch_id, int mkt_id, int order_type_id, int price_type_id, decimal limit_price, decimal order_quantity)
            => Request<AddOrderResponse>(_addOrder, new KeyValuePair<string, string>[] {
                KVP("auth_id", auth_id),
                KVP("exch_id", exch_id),
                KVP("mkt_id", mkt_id),
                KVP("order_type_id", order_type_id),
                KVP("price_type_id", price_type_id),
                KVP("limit_price", limit_price),
                KVP("order_quantity", order_quantity)
            });
        public Task<AddOrderResponse> AddOrderAsync(int auth_id, int exch_id, int mkt_id, int order_type_id, int price_type_id, decimal limit_price, decimal order_quantity)
            => RequestAsync<AddOrderResponse>(_addOrder, new KeyValuePair<string, string>[] {
                KVP("auth_id", auth_id),
                KVP("exch_id", exch_id),
                KVP("mkt_id", mkt_id),
                KVP("order_type_id", order_type_id),
                KVP("price_type_id", price_type_id),
                KVP("limit_price", limit_price),
                KVP("order_quantity", order_quantity)
            });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class CancelOrderResponse : BaseResponse
        {
            public object data;
            public List<Notification> notifications;
        }

        private static readonly string _cancelOrder = "cancelOrder";
        public CancelOrderResponse CancelOrder(int internal_order_id) => Request<CancelOrderResponse>(_cancelOrder,
            new KeyValuePair<string, string>[] { KVP("internal_order_id", internal_order_id) });
        public Task<CancelOrderResponse> CancelOrderAsync(int internal_order_id) => RequestAsync<CancelOrderResponse>(_cancelOrder,
            new KeyValuePair<string, string>[] { KVP("internal_order_id", internal_order_id) });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class ExchangesResponse : BaseResponse
        {
            public List<Exchange> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class Exchange
            {
                public string exch_balance_enabled;
                public string exch_code;
                public string exch_fee;
                public string exch_id;
                public string exch_name;
                public string exch_trade_enabled;
                public string exch_url;
            }
        }

        public ExchangesResponse Exchanges => GetExchanges();
        private static readonly string _exchanges = "exchanges";
        public ExchangesResponse GetExchanges() => Request<ExchangesResponse>(_exchanges);
        public Task<ExchangesResponse> GetExchangesAsync() => RequestAsync<ExchangesResponse>(_exchanges);


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class MarketsResponse : BaseResponse
        {
            public List<Market> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class Market
            {
                public string exch_code;
                public string exch_id;
                public string exch_name;
                public string exchmkt_id;
                public string mkt_id;
                public string mkt_name;
            }
        }

        private static readonly string _markets = "markets";
        public MarketsResponse Markets(string exchange_code) => Request<MarketsResponse>(_markets, KVP("exchange_code", exchange_code));
        public Task<MarketsResponse> MarketsAsync(string exchange_code) => RequestAsync<MarketsResponse>(_markets, KVP("exchange_code", exchange_code));


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class DataResponse : BaseResponse
        {
            public Data data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class Data
            {
                public List<DataAsk> asks;
                public List<DataBid> bids;
                public string exch_code;
                public List<DataHistory> history;
                public string primary_curr_code;
                public string secondary_curr_code;
                public string type;

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class DataAsk
                {
                    public string price;
                    public string quantity;
                    public string total;
                }

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class DataBid
                {
                    public string price;
                    public string quantity;
                    public string total;
                }

                [JsonObject(MemberSerialization = MemberSerialization.Fields)]
                public class DataHistory
                {
                    public string price;
                    public string quantity;
                    public string time_local;
                    public string type;
                }
            }
        }

        private static readonly string _data = "data";
        public DataResponse Data(string exchange_code, string exchange_market, MarketDataType type) => Request<DataResponse>(_data,
            new KeyValuePair<string, string>[] {
                KVP("exchange_code", exchange_code),
                KVP("exchange_market", exchange_market),
                KVP("type", Enum.GetName(typeof(MarketDataType), type))
            });
        public Task<DataResponse> DataAsync(string exchange_code, string exchange_market, MarketDataType type) => RequestAsync<DataResponse>(_data,
            new KeyValuePair<string, string>[] {
                KVP("exchange_code", exchange_code),
                KVP("exchange_market", exchange_market),
                KVP("type", Enum.GetName(typeof(MarketDataType), type))
            });


        [JsonObject(MemberSerialization = MemberSerialization.Fields)]
        public class TickerResponse : BaseResponse
        {
            public List<Ticker> data;
            public List<object> notifications;

            [JsonObject(MemberSerialization = MemberSerialization.Fields)]
            public class Ticker
            {
                public string ask;
                public string bid;
                public string current_volume;
                public string exchange;
                public string high_trade;
                public string last_trade;
                public string low_trade;
                public string market;
                public string timestamp;
            }
        }

        private static readonly string _ticker = "ticker";
        public TickerResponse Ticker(string exchange_code, string exchange_market) => Request<TickerResponse>(_ticker,
            new [] { KVP("exchange_code", exchange_code), KVP("exchange_market", exchange_market) });
        public Task<TickerResponse> TickerAsync(string exchange_code, string exchange_market) => RequestAsync<TickerResponse>(_ticker,
            new [] { KVP("exchange_code", exchange_code), KVP("exchange_market", exchange_market) });


        #region Private members


        private string _key;
        private string Api_Secret { get; set; }


        #region _httpPostRequest{,Async}()

        private string _httpPostRequest(string url, string ua, IEnumerable<KeyValuePair<string, string>> data)
        {
            using (var client = _prepareClient(ua, _key, Api_Secret))
            {
                var r = client.PostAsync(Server_Url + url, new FormUrlEncodedContent(data));
                r.Wait();
                var response = r.Result;
                return response.IsSuccessStatusCode ? response.Content.ReadAsStringAsync().Result :
                    "ERROR:" + response.StatusCode + " " + response.ReasonPhrase + " | " + response.RequestMessage;
            }
        }

        private async Task<string> _httpPostRequestAsync(string url, string ua, IEnumerable<KeyValuePair<string, string>> data)
        {
            using (var client = _prepareClient(ua, _key, Api_Secret))
                return await _processResponseAsync(await client.PostAsync(Server_Url + url, new FormUrlEncodedContent(data)));
        }

        #endregion _httpPostRequest{,Async}()


        #region _processResponseAsync()

        private static async Task<string> _processResponseAsync(HttpResponseMessage m) => m.IsSuccessStatusCode ?
            await m.Content.ReadAsStringAsync() : "ERROR:" + m.StatusCode + " " + m.ReasonPhrase + " | " + m.RequestMessage;

        #endregion _processResponseAsync()


        #region _prepareClient()

        private static HttpClient _prepareClient(string userAgent, string k, string s)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            client.DefaultRequestHeaders.Add("X-API-KEY", k);
            client.DefaultRequestHeaders.Add("X-API-SECRET", s);
            return client;
        }

        #endregion _prepareClient()


        #region Private constants

        #endregion Private constants


        #endregion Private members

    }
}