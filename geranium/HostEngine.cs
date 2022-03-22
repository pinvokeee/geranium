using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace geranium
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class Extention
	{
		public WebView2 View { get; set; }

		public ExtensionAssembly asm;

		public Extention(Window parentWindow, WebView2 webView2)
		{
			asm = new ExtensionAssembly(parentWindow, webView2);
			
			webView2.NavigationCompleted += (s, e) =>
			{
				View = webView2;

				View.CoreWebView2.AddHostObjectToScript("Assembly", asm);
				
				View.ExecuteScriptAsync("hosts_loaded()");
			};
		}

		public string ToBase64(string source)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
		}
	}
}
