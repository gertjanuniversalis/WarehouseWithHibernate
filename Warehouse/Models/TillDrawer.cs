using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.Models
{
	public class TillDrawer
	{
		public ICashSet Contents { get; private set; }

		public TillDrawer()
		{
			Contents = new CashSet();
		}
		public TillDrawer(ICashSet initialSet)
		{
			Contents = initialSet;
		}

		public void PrintContents(object s, EventArgs e)
		{
			Console.WriteLine(string.Format("Tilldrawer contains: {0}\n formatted as:{1}",
				Contents.GetSum().ToString(),
				Contents.ToString()));
		}
	}
}
