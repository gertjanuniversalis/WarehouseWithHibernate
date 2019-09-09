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
		public ShoppingCart(ProductController prodCon, Dictionary<IProduct, int> initialContent)
		{
			productController = prodCon;
			CartContents = initialContent;
		}
		public ShoppingCart(ShoppingCart copyCart)
		{
			this.productController = copyCart.productController;
			this.CartContents = copyCart.CartContents;
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
				//TODO: print "incorrect code format"
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

		internal void DisplayCartContent(object source, EventArgs e)
		{
			if (CartContents.Count >= 1)
			{
				StringBuilder builder = new StringBuilder("\nCart contains:");

				foreach (KeyValuePair<IProduct, int> orderedItem in CartContents)
				{
					builder.Append(string.Format("\n\t{0} times {1}",
						orderedItem.Value,
						orderedItem.Key.Description));
				}

				builder.Append(string.Format("\n\nFor a value of:{0}\n\n", GetTransactionValue().ToString()));

				//TODO: print cartontent
				Console.WriteLine(builder.ToString());
			}
			else
			{
				Console.WriteLine("Cart is empty");
			}
		}

		public decimal GetTransactionValue()
		{
			decimal value = 0m;

			foreach (KeyValuePair<IProduct, int> itemCount in CartContents)
			{
				value += itemCount.Key.UnitPrice * itemCount.Value;
			}

			return value;
		}


		private int GetAmount(string[] codeStr)
		{
			if (codeStr.Length > 1)
			{
				if(int.TryParse(codeStr[1], out int amount))
				{
					return amount;
				}
				else
				{
					return 1;
				}
			}
			else
			{
				return 1;
			}
		}

		private void AddToCart(int barcode, int amount)
		{
			IProduct product = productController.GetItemByCode(barcode);

			if (product != null)
			{
				foreach(KeyValuePair<IProduct, int> orderedProduct in CartContents)
				{
					if(orderedProduct.Key.BarCode == barcode)
					{
						CartContents[orderedProduct.Key] += amount;
						RaiseCartContentChanged(product, amount, true);
						return;
					}
				}
				CartContents.Add(product, amount);
				RaiseCartContentChanged(product, amount, true);
				return;
			}
			else
			{
				//TODO: Print "unknown item"
			}
		}

		private void RemoveFromCart(int barcode, int amount)
		{
			IProduct product = productController.GetItemByCode(barcode);

			if(product != null)
			{
				foreach(KeyValuePair<IProduct, int> prodInCart in CartContents)
				{
					if(prodInCart.Key.BarCode == barcode)
					{
						if(prodInCart.Value <= amount)
						{
							CartContents.Remove(prodInCart.Key);
						}
						else
						{
							CartContents[prodInCart.Key] -= amount;
						}
						return;
					}
				}
				Console.WriteLine("unknown item");
			}
			else
			{
				Console.WriteLine("unknown item");
			}
		}

		

		private void RaiseCartContentChanged(IProduct product, int amount, bool added)
		{
			CartContentChanged?.Invoke(this, new CartContentChangedEventArgs(product, amount, added, GetTransactionValue()));
		}

		private void RaiseCartPayedFor()
		{
			PaymentCompleted?.Invoke(this, new CartPayedForEventArgs(CartContents));
		}
	}
}
