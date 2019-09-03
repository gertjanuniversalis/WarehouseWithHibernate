using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Controllers
{
	public static class ConsoleController
	{
		/// <summary>
		/// Prints the parameter to the console
		/// </summary>
		/// <param name="textToPrint">The text to print</param>
		public static void Print(string textToPrint)
		{
			Console.WriteLine(textToPrint);
		}

		public static string GetStringInput(string textToShow)
		{
			Print(textToShow);
			return Console.ReadLine();
		}

		public static ConsoleKey GetSingleKey(string textToShow, bool hideInput = false)
		{
			Print(textToShow);
			return Console.ReadKey(hideInput).Key;
		}
	}
}
