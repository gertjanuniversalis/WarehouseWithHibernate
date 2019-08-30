using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse
{
	class Program
	{
		private static bool runProcess = true;

		static void Main(string[] _args)
		{
			while(runProcess)
			{
				ITillDrawer tillDrawer = new TillDrawer(DefaultCashSet());

				IConsole console = new IO.ManagementScreen(tillDrawer);

				console.PerformTransaction();
			}
		}

		internal static bool QuitConfirm()
		{
			Console.WriteLine("Really Quit? (Y/N)");

			var answer = Console.ReadKey(false);

			if (answer.Key == ConsoleKey.Y)
			{
				runProcess = false;
				return true;
			}
			else
			{
				runProcess = true;
				return false;
			}
		}

		private static CashSet DefaultCashSet()
		{
			CashSet cashSet = new CashSet();
				cashSet.Add(new CashItem("50 euros", 50m), 0);
				cashSet.Add(new CashItem("20 euros", 20m), 1);
				cashSet.Add(new CashItem("10 euros", 10m), 0);
				cashSet.Add(new CashItem("5 euros", 5m), 1);
				cashSet.Add(new CashItem("2 euros", 2m), 1);
				cashSet.Add(new CashItem("1 euros", 1m), 4);
				cashSet.Add(new CashItem("50 cents", 0.5m), 2);
				cashSet.Add(new CashItem("20 cents", 0.2m), 10);
				cashSet.Add(new CashItem("10 cents", 0.1m), 3);
				cashSet.Add(new CashItem("5 cents", 0.05m), 7);
				cashSet.Add(new CashItem("2 cents", 0.02m), 1);
				cashSet.Add(new CashItem("1 cent", 0.01m), 4);

			return cashSet;			
		}
	}
}
