using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Controllers;
using Warehouse.CustomArgs;
using Warehouse.Interfaces;

namespace Warehouse.Models
{
	public class ShoppingCart
	{
		public event EventHandler<CartContentChangedEventArgs> CartContentChanged;
		public event EventHandler<ProvideChangeEventArgs> ChangeRequested;
		public event EventHandler<CartPayedForEventArgs> PaymentCompleted;

		private readonly ProductController productController;
		private readonly PaymentController paymentController;

		public Dictionary<IProduct, int> CartContents { get; private set; }



		public ShoppingCart(ProductController prodCon, PaymentController payCon)
		{
			productController = prodCon;
			paymentController = payCon;

			CartContents = new Dictionary<IProduct, int>();
		}
		public ShoppingCart(ProductController prodCon, PaymentController payCon, Dictionary<IProduct, int> initialContent)
		{
			productController = prodCon;
			paymentController = payCon;

			CartContents = initialContent;
		}
		public ShoppingCart(ShoppingCart cartToCopy)
		{
			this.productController = cartToCopy.productController;
			this.paymentController = cartToCopy.paymentController;

			this.CartContents = cartToCopy.CartContents;
		}

		public void EditCart(object s, BarcodeScannedEventArgs bse)
		{
			string[] codeStr = bse.BarcodeStr.Split(' ');

			if(int.TryParse(codeStr[0], out int itemCode))
			{
				if(bse.AddToCart)
				{
					AddToCart(itemCode, GetAmount(codeStr));
				}
				else
				{
					RemoveFromCart(itemCode, GetAmount(codeStr));
				}
			}
			else
			{
				Console.WriteLine("Inproper barcode format; please try again");
			}
		}

		internal void StartPayment(object s, PaymentEventArgs pe)
		{
			if(decimal.TryParse(pe.CashGivenStr, out decimal cashGiven))
			{
				decimal cartVal = GetTransactionValue();
				if(cashGiven == cartVal)
				{
					RaiseCartPayedFor();
				}
				else if (cashGiven > cartVal)
				{
					RaiseProvideChange(cashGiven, cartVal);
				}
				else
				{
					Console.WriteLine("Not enough money handed over");
				}
			}
			else
			{
				Console.WriteLine("Invalid cash format");
			}
		}

		internal void ResetContent(object sender, CartPayedForEventArgs e)
		{
			CartContents = new Dictionary<IProduct, int>();
		}

		internal void FinalizeTransaction(object sender, PaymentCompletedEventArgs pce)
		{
			paymentController.TillDrawer.Contents.Add(pce.GivenCash);
			paymentController.TillDrawer.Contents.Remove(pce.PayoutSet);			
		}

		internal void DisplayCartContent(object source, EventArgs e)
		{
			if (CartContents.Count > 0)
			{
				Console.WriteLine("\nCart contains:\n" +
					string.Join("\n\t", CartContents.Select(p => p.Value + " times " + p.Key.Description)) +
					"\n\nFor a value of: " + GetTransactionValue().ToString());

			}
			else
			{
				Console.WriteLine("Cart is empty");
			}
		}

		public decimal GetTransactionValue()
		{
			return CartContents.Sum(p => p.Key.UnitPrice * p.Value);
		}

		private int GetAmount(string[] codeStr)
		{
			if (codeStr.Length > 1)
			{
				if(int.TryParse(codeStr[1], out int amount))
				{
					return amount;
				}
			}
			return 1;
		}

		private void AddToCart(int barcode, int amount)
		{
			var enteredProd = CartContents.SingleOrDefault(p => p.Key.BarCode == barcode);

			if (enteredProd.Equals(default(KeyValuePair<IProduct,int>)))
			{
				IProduct product = productController.GetItemByCode(barcode);

				if (product != null)
				{
					CartContents.Add(product, amount);
					RaiseCartContentChanged(product, amount, true);
				}
				else
				{
					Console.WriteLine(string.Format("No item with barcode {0} found", barcode.ToString()));
				}
			}
			else
			{
				CartContents[enteredProd.Key] += amount;
				RaiseCartContentChanged(enteredProd.Key, amount, true);
			}
		}

		private void RemoveFromCart(int barcode, int amount)
		{
			var enteredProd = CartContents.SingleOrDefault(p => p.Key.BarCode == barcode);

			if (!enteredProd.Equals(default(KeyValuePair<IProduct, int>)))
			{
				if(enteredProd.Value > amount)
				{
					CartContents[enteredProd.Key] -= amount;
				}
				else
				{
					CartContents.Remove(enteredProd.Key);
				}
			}
			else
			{
				Console.WriteLine(string.Format("No product with code {0} found in the cart", barcode));
			}
		}

		

		private void RaiseCartContentChanged(IProduct product, int amount, bool added)
		{
			CartContentChanged?.Invoke(this, new CartContentChangedEventArgs(product, amount, added, GetTransactionValue()));
		}

		private void RaiseCartPayedFor()
		{
			PaymentCompleted?.Invoke(this, new CartPayedForEventArgs(CartContents));
			Console.WriteLine("Transaction completed");
		}

		private void RaiseProvideChange(decimal cashGiven, decimal cartVal)
		{
			ChangeRequested?.Invoke(this, new ProvideChangeEventArgs(cashGiven, cartVal));
			RaiseCartPayedFor();
		}
	}
}
