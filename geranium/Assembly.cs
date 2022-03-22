using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace geranium
{
	public class ExtensionObject
	{
		public string Path { get; set; }
	}

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class ExtensionAssembly : HostObject, IHostObject
	{
		public List<ExtensionObject> Extentsions = new List<ExtensionObject>();

		public ExtensionAssembly(Window w, WebView2 wv) : base(w, wv)
		{
			
		}

		public void LoadFrom(object[] dll_paths)
		{
			foreach (object s in dll_paths)
			{
				LoadFrom(s as string);
			}

			this.ExecuteScript("extension_loaded();");
		}

		public void LoadFrom(string dll_path)
		{
			if (dll_path == null) return;
			if (Extentsions.Any(x => dll_path == x.Path)) return;
			var assembly = Assembly.LoadFile(dll_path);

			IHostObject ext = null;

			foreach (var type in assembly.GetTypes())
			{
				if (type.IsInterface) continue;
				ext = Activator.CreateInstance(type, BindingFlags.CreateInstance, null, new object[] { this.ParentWindow, this.WebView }, null) as HostObject;
				if (ext != null) break;
			}

			Extentsions.Add(new ExtensionObject()
			{
				Path = dll_path
			});

			this.AddHostObject(ext.AssemblyName, ext);

			ext.Loaded();
		}
	}
}
