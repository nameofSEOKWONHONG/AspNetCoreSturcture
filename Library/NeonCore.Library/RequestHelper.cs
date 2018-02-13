using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NeonCore.Library
{
    public class RequestHelper
    {
        string _baseUrl;
        Dictionary<string, string> _headers;

        public RequestHelper(string baseUrl, Dictionary<string, string> headers = null)
        {
            _baseUrl = baseUrl;

            if (headers != null)
            {
                _headers = headers;
            }
        }

        public async Task<T> Request<T>(REQUEST_TYPE type, string method, string getParam = null, string postParam = null, string header = null)
        {
            switch(type)
            {
                case REQUEST_TYPE.GET:
                    return await RequestGet<T>(method, getParam);
                case REQUEST_TYPE.POST:
                    return await RequestPost<T>(method, postParam);
                case REQUEST_TYPE.PUT:
                    return await RequestPut<T>(method, postParam);
                case REQUEST_TYPE.DELETE:
                    return await RequestDelete<T>(method, getParam);
                default:
                    throw new NotImplementedException("not impl yet.");
            }
        }

        private async Task<T> RequestGet<T>(string method, string getParam)
        {
            T entity = default(T);

            using(var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_baseUrl);

                    if (_headers != null)
                    {
                        client.DefaultRequestHeaders.Clear();

                        foreach(var kvp in _headers)
                        {
                            client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                        }
                    }

                    var getUrl = method + "?" + getParam;
                    var response = await client.GetAsync(getUrl);

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var responseText = await
                            response.Content.ReadAsStringAsync();

                        entity = JsonConvert.DeserializeObject<T>(responseText);
                    }
                }
                catch(Exception e)
                {
                    
                }
            }

            return entity;
        }

        private async Task<T> RequestPost<T>(string method, string postParam)
        {
            T entity = default(T);
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                if (_headers != null)
                {
                    client.DefaultRequestHeaders.Clear();

                    foreach (var kvp in _headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                var contents = new StringContent(postParam, Encoding.UTF8, "application/json");                

                var response = await client.PostAsync(method, contents);

                response.EnsureSuccessStatusCode();

                if(response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();

                    entity = JsonConvert.DeserializeObject<T>(responseText);
                }
            }

            return entity;
        }

        private async Task<T> RequestPut<T>(string method, string postParam)
        {
            T entity = default(T);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                if (_headers != null)
                {
                    client.DefaultRequestHeaders.Clear();

                    foreach (var kvp in _headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                var contents = new StringContent(postParam, Encoding.UTF8, "application/json");

                var response = await client.PutAsync(method, contents);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();

                    entity = JsonConvert.DeserializeObject<T>(responseText);
                }
            }

            return entity;
        }

        private async Task<T> RequestDelete<T>(string method, string getParam)
        {
            T entity = default(T);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                if (_headers != null)
                {
                    client.DefaultRequestHeaders.Clear();

                    foreach (var kvp in _headers)
                    {
                        client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                    }
                }

                var response = await client.DeleteAsync(method + "?" + getParam);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();

                    entity = JsonConvert.DeserializeObject<T>(responseText);
                }
            }

            return entity;
        }
    }

    public enum REQUEST_TYPE
    {
        GET,
        POST,
        PUT,
        DELETE
    }


}
