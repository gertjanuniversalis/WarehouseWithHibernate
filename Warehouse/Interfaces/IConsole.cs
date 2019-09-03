using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	interface IConsole
	{
		void Print(string textToPrint);

		string GetStringInput(string textToShow);

		ConsoleKey GetSingleKey(string textToShow, bool hideInput = false);
	}
}
