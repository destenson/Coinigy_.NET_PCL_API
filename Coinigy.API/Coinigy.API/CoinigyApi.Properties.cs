
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Coinigy.API
{
    public partial class CoinigyApi
    {
        #region Public Properties (User_Agent, Server_Url)

        public string User_Agent { get; set; } = DefaultUserAgent;
        public string Server_Url { get; set; } = DefaultBaseUrl;

        #endregion Public Properties (User_Agent, Server_Url)


        #region Internal properties and methods

        static internal string DefaultBaseUrl { get; } = "https://api.coinigy.com/api/v1/";
        static internal string DefaultUserAgent { get; } = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        static internal KeyValuePair<T1, T2> KVP<T1, T2>(T1 a, T2 b) => new KeyValuePair<T1, T2>(a, b);
        static internal KeyValuePair<string, string> KVP<T>(string a, T b) => KVP(a, b.ToString());
        static internal KeyValuePair<string, string> KVP(string a, bool b) => KVP(a, Convert.ToInt32(b));

        static internal string GetDate(string date) => string.IsNullOrEmpty(date) ? DateTime.UtcNow.ToString("yyyy-MM-dd") : date;

        static internal readonly IEnumerable<KeyValuePair<string, string>> E = new KeyValuePair<string, string>[0];

        internal T Request<T>(string method) => Request<T>(method, null);
        internal T Request<T>(string method, KeyValuePair<string, string> p) => Request<T>(method, new KeyValuePair<string, string>[]{p});
        internal T Request<T>(string method, IEnumerable<KeyValuePair<string, string>> list)
            => JsonConvert.DeserializeObject<T>(_httpPostRequest(method, User_Agent, list ?? E));

        internal Task<T> RequestAsync<T>(string method) => RequestAsync<T>(method, null);
        internal Task<T> RequestAsync<T>(string method, KeyValuePair<string, string> p) => RequestAsync<T>(method, new[] { p });
        internal async Task<T> RequestAsync<T>(string method, IEnumerable<KeyValuePair<string, string>> list)
            => JsonConvert.DeserializeObject<T>(await _httpPostRequestAsync(method, User_Agent, list ?? E));

        internal KeyValuePair<string, string> KS => new KeyValuePair<string, string>(_key, Api_Secret);

        #endregion Internal properties and methods
    }
}
