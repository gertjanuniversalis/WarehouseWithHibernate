using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Interfaces;

namespace Warehouse
{
	class Program
	{
		private static bool runProcess = true;

		static void Main(string[] args)
		{

			while(runProcess)
			{

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
	}
}
