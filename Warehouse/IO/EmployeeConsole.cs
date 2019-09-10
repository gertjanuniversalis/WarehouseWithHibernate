using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.CustomArgs;
using Warehouse.Interfaces;

namespace Warehouse.IO
{
	class EmployeeConsole
	{		
		public delegate void InputRequestedEventHandler(object source, ConsolePrintEventArgs cpe);
		public event InputRequestedEventHandler InputRequested;

		private string Instructions => GetInstructions();

		internal void GetInput()
		{
			InputRequested?.Invoke(this, new ConsolePrintEventArgs("input code"));
		}

		public void PrintInstructions()
		{
			PrintInstructions(null, EventArgs.Empty);
		}
		public void PrintInstructions(object s, EventArgs e)
		{
			Console.WriteLine(Instructions);
		}




		private string GetInstructions()
		{
			return
				"XXX\t -> Add 1 item with code XXX to the transaction\n" +
				"XXX Y\t -> Adds Y of item with code XXX to the transaction\n" +
				"AXXX\t -> Add 1 item with code XXX to the transaction\n" +
				"AXXX Y\t -> Adds Y of item with code XXX to the transaction\n" +
				"RXXX\t -> Remove 1 item with code XXX from the transaction\n" +
				"RXXX Y\t -> Remove Y items with code XXX from the transaction\n" +
				"-----------------\n" +
				"T\t -> Print the current transaction\n" +
				"BYYY\t -> process payment of YYY\n" +
				"P\t -> Print all available items\n" +
				"C\t -> Show the current contents of the cash drawer\n" +
				"Q\t -> Close the register\n" +
				"I\t -> Shows the instructions\n" +
				"OXXX\t -> Displays the contents of Order XXX\n" +
				"\n";
		}
	}
}
