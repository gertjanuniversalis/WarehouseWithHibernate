using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;
using Warehouse.Models;
using Warehouse.Exceptions;
using Warehouse.Controllers;

namespace Warehouse.IO
{
	class ManagementScreen
	{
		private readonly ITillDrawer tillDrawer;

		private IShoppingCart cart;

		private bool transactionRuns;

		private int userID;

		public ManagementScreen(ITillDrawer tillDrawer)
		{
			this.tillDrawer = tillDrawer;
		}

		/// <summary>
		/// Runs a single purchase transaction
		/// </summary>
		public void PerformTransaction()
		{
			transactionRuns = true;

			cart = new ShoppingCart();

			SetUserID();

			ShowInstructions();

			while(transactionRuns)
			{
				try
				{
					string userInput = ConsoleController.GetStringInput("Input code");


					switch (userInput.ToUpper().Substring(0,1))
					{
						case "A":
							AddToCart(userInput.Substring(1));
							break;
						case "B":
							if (FinalisePurchase(userInput.Substring(1)))
							{
								return;
							}
							else
							{
								break;
							}
						case "C":
							ShowDrawerContent();
							break;
						case "I":
							ShowInstructions();
							break;
						case "O":
							PrintOrder(userInput.Substring(1));
							break;
						case "P":
							PrintCatalogue();
							break;
						case "Q":
							if (Program.QuitConfirm())
							{
								transactionRuns = false;
								return;
							}
							else
							{
								break;
							}
						case "R":
							RemoveFromCart(userInput.Substring(1));
							break;
						case "T":
							PrintCurrentCart();
							break;						
						default:
							AddToCart(userInput);
							break;
					}
				}
				catch (InvalidCashStructureException)
				{
					ConsoleController.Print("This form of cashitem/cashstack is not valid; please reenter");
					continue;
				}
				catch (CartModifyException)
				{
					ConsoleController.Print("Unable to add this product to the cart: please try again");
					continue;
				}
				catch (Exception)
				{
					ConsoleController.Print("An unexpected error occured, please retry what you were doing");
					continue;
				}
			}
		}

		private void PrintOrder(string orderIdStr)
		{
			if (int.TryParse(orderIdStr, out int orderID))
			{
				IOrder requestedOrder = OrderController.GetOrder(orderID);

				if(requestedOrder != null)
				{
					IShoppingCart orderCart = new ShoppingCart(requestedOrder.OrderedProducts.ToList());

					ConsoleController.Print(string.Format("The contents of Order {0} are \n{1}Ordered at {2}",
						orderIdStr,
						orderCart.ToString(),
						requestedOrder.OrderDate.ToString()));
				}
				else
				{
					ConsoleController.Print(string.Format("No order found for ID: {0}", orderIdStr));
				}
			}
			else
			{
				ConsoleController.Print("Invalid id string");
			}
		}

		/// <summary>
		/// Sets the userID for database entry
		/// </summary>
		private void SetUserID()
		{
			//TODO: determing userID at runtime

			userID = 0;
		}

		/// <summary>
		/// Prints all items that are available for purchase
		/// </summary>
		private void PrintCatalogue()
		{
			ConsoleController.Print(ProductController.GetCatalogue());
		}

		/// <summary>
		/// Prints the current contents of the shoppingcart
		/// </summary>
		private void PrintCurrentCart()
		{
			ConsoleController.Print(cart.ToString());
		}

		/// <summary>
		/// Adds an item to the shoppingcart, based on the barcode
		/// </summary>
		/// <param name="barcodeString">The identification code for the item to add</param>
		private void AddToCart(string barcodeString)
		{
			string[] codeAmount = barcodeString.Split(' ');

			if (int.TryParse(codeAmount[0], out int barCode))
			{
				int amount = codeAmount.Length > 1 ? (int.TryParse(codeAmount?[1], out int enteredNum) ? enteredNum : 1) : 1;

				Success success = cart.AddItem(barCode, amount);
				
				if (!success.Result)
				{
					ConsoleController.Print(success.ResultComment);
				}
				else
				{
					ConsoleController.Print(string.Format("\n{2}\n{0}\n{1}\n{2}\n\n", 
						cart.GetTransactionValue().ToString(), 
						success.ResultComment, 
						"========="));
				}
			}
			else
			{
				ConsoleController.Print("\nInvalid Barcode");
			}
		}

		/// <summary>
		/// Removes an item from the shoppingcart, based on the barcode
		/// </summary>
		/// <param name="barcodeString">The identification code for the item to remove</param>
		private void RemoveFromCart(string barcodeString)
		{
			string[] codeAmount = barcodeString.Split(' ');

			if(int.TryParse(codeAmount[0], out int barCode))
			{
				int amount = codeAmount.Length > 1 ? (int.TryParse(codeAmount?[1], out int enteredNum) ? enteredNum : 1) : 1;

				Success success = cart.RemoveItem(barCode, amount);

				if(!success.Result)
				{
					ConsoleController.Print(success.ResultComment);
				}
			}
			else
			{
				ConsoleController.Print("\nInvalid Barcode");
			}
		}

		/// <summary>
		/// Prints the cash present and total value in the tilldrawer
		/// </summary>
		private void ShowDrawerContent()
		{
			ConsoleController.Print(tillDrawer.ToString());
		}

		/// <summary>
		/// Stores the order in the ordersDB andprints the change to hand to the customer
		/// </summary>
		/// <param name="moneyGivenStr">A string representation of the cash given by the customer</param>
		/// <returns><see langword="true"/> if the transaction was completed successfully, <see langword="false"/>if not</returns>
		private bool FinalisePurchase(string moneyGivenStr)
		{
			if (decimal.TryParse(moneyGivenStr, out decimal moneyGiven))
			{
				Success paymentSuccess = HandlePayment(moneyGiven);
				if(paymentSuccess.Result)
				{					
					Success orderSaved = SaveOrderToDB();
					if(orderSaved.Result)
					{
						return true;
					}
					else
					{
						ConsoleController.Print(orderSaved.ResultComment);
						return false;
					}
				}
				else
				{
					ConsoleController.Print(paymentSuccess.ResultComment);
					return false;
				}
			}
			else
			{
				ConsoleController.Print("\nIncorrect payment format");
				return false;
			}
		}

		/// <summary>
		/// Handles the payment
		/// </summary>
		/// <param name="moneyGiven"></param>
		/// <returns></returns>
		private Success HandlePayment(decimal moneyGiven)
		{
			decimal valueToReturn = moneyGiven - cart.GetTransactionValue();
			if (tillDrawer.ContainsEnough(valueToReturn))
			{
				ICashSet givenSet = CashController.SmallestSetForValue(moneyGiven);

				ICashSet availableCash = new CashSet(tillDrawer.DrawerContent);
				availableCash.Add(givenSet);

				ICashSet returnSet = PaymentController.Payout(valueToReturn, availableCash);

				if (returnSet != null)
				{
					ConsoleController.Print(string.Format("\nReturn {0}, distributed as: {1}", 
						valueToReturn.ToString(), 
						returnSet.ToString()));

					tillDrawer.Add(givenSet);
					tillDrawer.Remove(returnSet);

					return new Success(true);
				}
				else
				{
					return new Success(false, "\nUnable to allocate sufficient cash; request a different payment method");
				}
			}
			else
			{
				return new Success(false, "\nNot enough money in the drawer; request a different payment method");
			}
		}

		/// <summary>
		/// Saves the current cart to the orders DB
		/// </summary>
		/// <returns>A 'Success' object containing the result</returns>
		private Success SaveOrderToDB()
		{
			try
			{
				OrderController.SaveOrder(userID, cart);

				return new Success(true);
			}
			catch (Exception e)
			{
				return new Success(false, e.Message);
			}
		}

		/// <summary>
		/// Prints the instructions for using the till
		/// </summary>
		private void ShowInstructions()
		{
			string instructions =
				"XXX\t -> Add 1 item with code XXX to the transaction\n" +
				"XXX Y\t -> Adds Y of item with code XXX to the transaction" +
				"AXXX\t -> Add 1 item with code XXX to the transaction\n" +
				"AXXX Y\t -> Adds Y of item with code XXX to the transaction" +
				"RXXX\t -> Remove 1 item with code XXX from the transaction\n" +
				"RXXX Y\t -> Remove Y items with code XXX from the transaction\n" +
				"-----------------" +
				"T\t -> Print the current transaction\n" +
				"BYYY\t -> process payment of YYY\n" +
				"P\t -> Print all available items\n" +
				"C\t -> Show the current contents of the cash drawer\n" +
				"Q\t -> Close the register\n" +
				"I\t -> Shows the instructions\n" +
				"OXXX\t -> Displays the contents of Order XXX\n" +
				"\n";

			ConsoleController.Print(instructions);
		}
	}
}
