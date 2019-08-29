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
	class ManagementScreen : IConsole
	{
		private readonly ITillDrawer tillDrawer;

		private IShoppingCart cart;

		private bool transactionRuns;

		public ManagementScreen(ITillDrawer tillDrawer)
		{
			this.tillDrawer = tillDrawer;
		}

		public void PerformTransaction()
		{
			transactionRuns = true;

			cart = new ShoppingCart();

			ShowInstructions();

			while(transactionRuns)
			{
				try
				{
					Print("Input code");
					string userInput = Console.ReadLine();

					switch (userInput.ToUpper().Substring(0,1))
					{
						case "B":
							if (FinalisePurchase(userInput.Substring(1)))
							{
								return;
							}
							else
							{
								break;
							}
						case "P":
							PrintCatalogue();
							break;
						case "T":
							PrintCurrentCart();
							break;
						case "R":
							RemoveFromCart(userInput.Substring(1));
							break;
						case "A":
							AddToCart(userInput.Substring(1));
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
						case "C":
							ShowDrawerContent();
							break;
						default:
							AddToCart(userInput);
							break;

					}
				}
				catch (InvalidCashStructureException)
				{
					//TODO: handle exception
					continue;
				}
				catch (CartModifyException)
				{
					//TODO: Handle Exception
					continue;
				}
				catch (Exception)
				{
					continue;
				}
			}
		}

		private void PrintCatalogue()
		{
			Print(ProductController.GetCatalogue());
		}

		private void PrintCurrentCart()
		{
			Print(cart.ToString());
		}

		private void AddToCart(string barcodeString)
		{
			if(int.TryParse(barcodeString, out int barCode))
			{
				Success success = cart.AddItem(barCode);
				
				if (!success.Result)
				{
					Print(success.ResultComment);
				}
			}
			else
			{
				Print("\nInvalid Barcode");
			}
		}

		private void RemoveFromCart(string barcodeString)
		{
			if(int.TryParse(barcodeString, out int barCode))
			{
				Success success = cart.RemoveItem(barCode);

				if(!success.Result)
				{
					Print(success.ResultComment);
				}
			}
			else
			{
				Print("\nInvalid Barcode");
			}
		}

		private void ShowDrawerContent()
		{
			Print(tillDrawer.ToString());
		}

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
						Print(orderSaved.ResultComment);
						return false;
					}
				}
				else
				{
					Print(paymentSuccess.ResultComment);
					return false;
				}
			}
			else
			{
				Print("\nIncorrect payment format");
				return false;
			}
		}

		private Success HandlePayment(decimal moneyGiven)
		{
			decimal valueToReturn = moneyGiven - cart.GetTransactionValue();
			if (tillDrawer.ContainsEnough(valueToReturn))
			{
				ICashSet givenSet = CashController.SmallestSetForValue(moneyGiven);

				ICashSet availableCash = tillDrawer.DrawerContent;
				availableCash.Add(givenSet);

				ICashSet returnSet = PaymentController.Payout(valueToReturn, availableCash);

				if (returnSet != null)
				{
					Print(string.Format("\nReturn {0}, distributed as {1}", valueToReturn.ToString(), returnSet.ToString()));

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

		private Success SaveOrderToDB()
		{
			try
			{



				return new Success(true);
			}
			catch (Exception e)
			{
				return new Success(false, e.Message);
			}
		}

		private void ShowInstructions()
		{
			string instructions =
				"XXX \t-> Add 1 item with code XXX to the transaction\n" +
				"AXXX\t -> Add 1 item with code XXX to the transaction\n" +
				"T\t -> Print the current transaction\n" +
				"BYYY\t -> process payment of YYY\n" +
				"P\t -> Print all available items\n" +
				"RXXX\t -> Remove 1 item with code XXX from the transaction\n" +
				"C\t -> Show the current contents of the cash drawer\n" +
				"Q\t -> Close the register\n" +
				"\n";

			Print(instructions);
		}

		public void Print(string info)
		{
			Console.WriteLine(info);
		}
	}
}
