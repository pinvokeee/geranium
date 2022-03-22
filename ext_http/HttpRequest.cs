using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace geranium
{
	public class HttpRequest : HostObject, IHostObject
	{
		public HttpClient Client { get; set; }

		public override string AssemblyName => "Http";

		public HttpRequest(Window w, WebView2 wv) : base(w, wv)
		{
			
			
		}

		public class RequestMessage
		{
			public string method { get; set; } = "GET";
			public IDictionary<string, string> headers { get; set; }
			public string mediaType { get; set; } = "application/x-www-form-urlencoded";
			public string body { get; set; } = null;
		}

		public class ReponseMessage
		{
			public string Content { get; set; }
			public int StatusCode { get; set; }
		}

		public async Task Send(string url, string message, string func_name = "")
		{
			if (Client == null) Client = new HttpClient();

			RequestMessage json = (RequestMessage)JsonSerializer.Deserialize(message, typeof(RequestMessage));

			string method = json.method;

			using (HttpRequestMessage reqm = new HttpRequestMessage(new HttpMethod(json.method), url))
			{
				if (json.headers != null)
				{
					foreach (KeyValuePair<string, string> kv in json.headers)
					{
						reqm.Headers.Add(kv.Key, kv.Value);
					}
				}

				if (json.body != null)
				{
					reqm.Content = new StringContent(json.body, Encoding.UTF8, json.mediaType);
				}

				HttpResponseMessage response = await Client.SendAsync(reqm);

				string result = JsonSerializer.Serialize(
				new ReponseMessage()
				{
					Content = await response.Content.ReadAsStringAsync(),
					StatusCode = (int)response.StatusCode
				},
				typeof(ReponseMessage));

				if (func_name.Length > 0) this.Call(func_name, result);
			}
		}

		public override void Loaded()
		{
			base.Loaded();
		}
	}
}
