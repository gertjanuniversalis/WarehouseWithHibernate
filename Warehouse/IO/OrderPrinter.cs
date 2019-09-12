using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.CustomArgs;

namespace Warehouse.IO
{
	public class OrderPrinter
	{
		internal void PrintOrder(object sender, CartPayedForEventArgs cpfe)
		{
			var payedCart = cpfe.ProcessedCart;

			string topBorder = "\n";
			topBorder = topBorder.PadRight(20, '=');

			string header = "\n|\tNew order:";
			string order = string.Join("\n| ", payedCart.Select(p => p.Value + " times " + p.Key.Description+" |"));

			Console.WriteLine(topBorder+header+order+topBorder);
		}
	}
}
