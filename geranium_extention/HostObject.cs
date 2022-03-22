using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace geranium
{
	public interface IHostObject
	{
		//void Load();
		void Loaded();

		string ToBase64(string source);

		string AssemblyName { get; }
	}

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class HostObject : IHostObject
	{
		public Window ParentWindow { get; set; }
		public WebView2 WebView { get; set; }

		public Action<string, string> Execute;

		public virtual string AssemblyName
		{
			get => this.GetType().Name;
		}

		public string ToBase64(string source)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
		}

		public HostObject(Window w, WebView2 wv)
		{
			ParentWindow = w;
			WebView = wv;
		}

		public virtual void Loaded()
		{
			this.ExecuteScript($"console.log(\"LoadedExtension - { this.AssemblyName }\")");
		}

		public async void Call(string function_name, string result)
		{
			string js = $"{ function_name }(\"{ ToBase64(result) }\");";
			await((dynamic)(this.WebView)).ExecuteScriptAsync(js);
		}

		public async void ExecuteScript(string script)
		{
			await this.WebView.ExecuteScriptAsync(script);
		}

		public void AddHostObject(string name, object instance)
		{
			this.WebView.CoreWebView2.AddHostObjectToScript(name, instance);
		}
	}
}
