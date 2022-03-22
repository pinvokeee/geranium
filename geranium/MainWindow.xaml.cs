using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace geranium
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CoreWebView2EnvironmentOptions op = new CoreWebView2EnvironmentOptions();
			//CoreWebView2Environment ee = await CoreWebView2Environment.CreateAsync(@"\Microsoft.WebView2.FixedVersionRuntime.99.0.1150.36.x64", null, op);
			CoreWebView2Environment ee = await CoreWebView2Environment.CreateAsync(null, null, op);
			
			await View.EnsureCoreWebView2Async(ee);
			
			Extention v = new Extention(this, View);

			View.CoreWebView2.Navigate("file:///C:/Users/nanas/Downloads/%E6%96%B0%E3%81%97%E3%81%84%E3%83%95%E3%82%A9%E3%83%AB%E3%83%80%E3%83%BC%20(3)/aa.html");
			//View.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;


		}
	}
}
