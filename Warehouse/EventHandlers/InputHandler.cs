using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.CustomArgs;

namespace Warehouse.EventHandlers
{
	class InputHandler
	{		
		public event EventHandler<BarcodeScannedEventArgs> BarcodeScanned;
		public event EventHandler<EventArgs> CartContentRequested;
		public event EventHandler<EventArgs> CatalogueRequested;
		public event EventHandler<EventArgs> InstructionsRequested;
		public event EventHandler<OrderEventArgs> OrderRequested;
		public event EventHandler<PaymentEventArgs> PaymentStarted;
		public event EventHandler<EventArgs> ProgramClosing;
		public event EventHandler<EventArgs> TillContentRequested;


		public void GetAndHandleInput(object s, ConsolePrintEventArgs cpe)
		{
			Console.WriteLine(cpe.textToDisplay);

			string input = Console.ReadLine();

			if (input.Length == 0) return;

			switch (input.ToUpper().Substring(0,1))
			{
				case "A":
					RaiseAddProduct(input.Substring(1));
					break;
				case "B":
					RaiseStartPayment(input.Substring(1));
					break;
				case "C":
					RaiseShowTillDrawer();
					break;
				case "I":
					RaiseInstructionsRequested();
					break;
				case "O":
					RaiseShowOldOrder(input.Substring(1));
					break;
				case "P":
					RaiseShowCatalogue();
					break;
				case "Q":
					RaiseCloseProgram();
					break;
				case "R":
					RaiseRemoveProduct(input.Substring(1));
					break;
				case "T":
					RaiseShowCartContent();
					break;
				default:
					RaiseAddProduct(input);
					break;
			}
		}
		
		private void RaiseAddProduct(string barCode)
		{
			BarcodeScanned?.Invoke(this, new BarcodeScannedEventArgs(barCode));
		}

		private void RaiseRemoveProduct(string barCode)
		{
			BarcodeScanned?.Invoke(this, new BarcodeScannedEventArgs(barCode, false));
		}

		private void RaiseShowCartContent()
		{
			CartContentRequested?.Invoke(this, EventArgs.Empty);
		}

		private void RaiseCloseProgram()
		{
			ProgramClosing?.Invoke(this, EventArgs.Empty);
		}

		private void RaiseShowCatalogue()
		{
			CatalogueRequested?.Invoke(this, EventArgs.Empty);
		}

		private void RaiseShowOldOrder(string orderID)
		{
			OrderRequested?.Invoke(this, new OrderEventArgs(orderID));
		}

		private void RaiseShowTillDrawer()
		{
			TillContentRequested?.Invoke(this, EventArgs.Empty);
		}

		private void RaiseStartPayment(string cashGivenStr)
		{
			PaymentStarted?.Invoke(this, new PaymentEventArgs(cashGivenStr));
		}

		private void RaiseInstructionsRequested()
		{
			InstructionsRequested?.Invoke(this, EventArgs.Empty);
		}
	}
}
