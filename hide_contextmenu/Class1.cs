using geranium;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hide_contextmenu
{
	public class HideContext : HostObject, IHostObject
	{
		public HideContext(WebView2 wv, Action<string, string> callback) : base(wv, callback)
		{
		}

		public override string AssemblyName => "h";

		public override void Loaded()
		{
			base.Loaded();
		}

		public async Task a()
		{
			this.ExecuteScript("alert(\"aaaaa\");");
			WebView.IsEnabled = false;
			Console.WriteLine("a");
		}

	}
}
