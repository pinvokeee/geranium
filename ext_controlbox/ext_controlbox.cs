using geranium;
using Microsoft.Web.WebView2.Wpf;
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

namespace ext_controlbox
{
	public class ext_hidecontrolbuttons : HostObject, IHostObject
	{
		[DllImport("user32.dll")]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		const int GWL_STYLE = -16;
		const int WS_SYSMENU = 0x80000;

		public ext_hidecontrolbuttons(Window w, WebView2 wv) : base(w, wv)
		{

		}

		public override string AssemblyName => "ControlBox";

		public void HideAllButtons()
		{
			IntPtr handle = new WindowInteropHelper(this.ParentWindow).Handle;
			int style = GetWindowLong(handle, GWL_STYLE);
			style = style & (~WS_SYSMENU);
			SetWindowLong(handle, GWL_STYLE, style);
		}
	}
}
