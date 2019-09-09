using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.CustomArgs;
using Warehouse.Interfaces;

namespace Warehouse.Obsolete.IO
{
	public class StorageViewer
	{
		private static IConsole ConsoleController = new Controllers.ConsoleController();

		public void ShowToOrderpicker(object s, ProductAddedEventArgs poe)
		{
			StringBuilder prodAddInfo = new StringBuilder("==========Warehouse===========");

			 prodAddInfo.Append(string.Format("\nThe following product was added:\n{0},\n{1} times",
				poe.product.Description,
				poe.amount.ToString()));

			prodAddInfo.Append("\n==========================================\n");
			ConsoleController.Print(prodAddInfo.ToString());
		}
	}
}
