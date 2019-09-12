using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Controllers;
using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse
{
	class Program
	{
		private static bool runProcess = true;
		private static ProductController ProductController;
		private static ConsoleController ConsoleController;

#pragma warning disable IDE0060 // Remove unused parameter
		static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
		{
			ProductController = new ProductController();
			ConsoleController = new ConsoleController();

			//Start the employee screen
			var employeeScreen = new IO.EmployeeConsole();
			var orderPrinter = new IO.OrderPrinter();

			//Setup till-specific instances
			var tillDrawer = new TillDrawer(DefaultCashSet());
			var inputHandler = new EventHandlers.InputHandler();
			var paymentcontroller = new PaymentController(tillDrawer);		
			var cart = new ShoppingCart(ProductController, paymentcontroller);
			var orderControler = new OrderController();

			//Subscribe to events
				//Listeners to EmployeeScreen
				employeeScreen.InputRequested += inputHandler.GetAndHandleInput;
				
				//Listeners to inputhandler
				inputHandler.BarcodeScanned += cart.EditCart;
				inputHandler.ProgramClosing += HandleEmployeeTillClosing;
				inputHandler.CartContentRequested += cart.DisplayCartContent;
				inputHandler.PaymentStarted += cart.StartPayment;
				inputHandler.InstructionsRequested += employeeScreen.PrintInstructions;
				inputHandler.TillContentRequested += paymentcontroller.TillDrawer.PrintContents;
				inputHandler.OrderRequested += orderControler.DisplayOrder;
				inputHandler.CatalogueRequested += ProductController.PrintCatalogue;

				//Listeners to ShoppingCart
				cart.CartContentChanged += ConsoleController.PrintNewItem;
				cart.ChangeRequested += paymentcontroller.DetermineChange;
				cart.PaymentCompleted += orderPrinter.PrintOrder;
				cart.PaymentCompleted += cart.ResetContent;

				//Listeners to paymentcontroller
				paymentcontroller.ChangeFound += ConsoleController.Print;
				paymentcontroller.PaymentPossible += cart.FinalizeTransaction;

			//Start the process
			employeeScreen.PrintInstructions();
			while(runProcess)
			{
				employeeScreen.GetInput();
			}
		}

		public static void HandleEmployeeTillClosing(object s, EventArgs e)
		{
			runProcess = false;
		}

		private static CashSet DefaultCashSet()
		{
			CashSet cashSet = new CashSet();
				cashSet.Add(new CashItem("50 euros", 50m),	 0);
				cashSet.Add(new CashItem("20 euros", 20m),	 1);
				cashSet.Add(new CashItem("10 euros", 10m),	 0);
				cashSet.Add(new CashItem("5 euros",	 5m),    1);
				cashSet.Add(new CashItem("2 euros",  2m),	 1);
				cashSet.Add(new CashItem("1 euros",  1m),	 4);
				cashSet.Add(new CashItem("50 cents", 0.5m),  2);
				cashSet.Add(new CashItem("20 cents", 0.2m),  10);
				cashSet.Add(new CashItem("10 cents", 0.1m),  3);
				cashSet.Add(new CashItem("5 cents",  0.05m), 7);
				cashSet.Add(new CashItem("2 cents",  0.02m), 1);
				cashSet.Add(new CashItem("1 cent",	 0.01m), 4);

			return cashSet;			
		}
	}
}
