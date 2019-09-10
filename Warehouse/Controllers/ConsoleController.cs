using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.CustomArgs;

namespace Warehouse.Controllers
{
	public class ConsoleController
	{
		public void PrintNewItem(object source, CartContentChangedEventArgs e)
		{
			Console.WriteLine(string.Format("\n{0}\n{1}\n{2}\n{3}",
				"=============Customer=================",
				e.NewSumTotal,
				e.Product.Description,
				"==================================="));
		}

		public void Print(object source, ConsolePrintEventArgs cpe)
		{
			Console.WriteLine(cpe.TextToDisplay);
		}
	}
}
