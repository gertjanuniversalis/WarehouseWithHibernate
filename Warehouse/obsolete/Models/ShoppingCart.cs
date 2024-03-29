﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.CustomArgs;
using Warehouse.Interfaces;

namespace Warehouse.Obsolete.Models
{
	public class ShoppingCart : IShoppingCart
	{
		public delegate void ProductAddedEventHandler(object source, ProductAddedEventArgs pae);
		public event ProductAddedEventHandler ProductAdded;

		public Dictionary<IProduct, int> CartItems { get; private set; }

		public ShoppingCart()
		{
			CartItems = new Dictionary<IProduct, int>();
		}

		public ShoppingCart(List<OrderedProduct> initialContent)
		{
			CartItems = new Dictionary<IProduct, int>();

			foreach (OrderedProduct product in initialContent)
			{
				CartItems.Add(product.Product, product.Quantity);
			}
		}

		public Success AddItem(int barCode, int amount = 1)
		{
			IProduct product = Controllers.ProductController.GetItemByCode(barCode);

			if(product != null)
			{
				foreach(KeyValuePair<IProduct, int> cartProd in CartItems)
				{
					if (cartProd.Key.BarCode == barCode)
					{
						CartItems[cartProd.Key] += amount;

						RaiseProductAdded(product, amount);

						return new Success(true, product.Description);
					}
				}

				CartItems.Add(product, amount);
				RaiseProductAdded(product, amount);

				return new Success(true, string.Format("{0}{1}",
					product.Description, 
					amount != 1 ? ", ("+amount.ToString()+")" : ""));
			}
			else
			{
				return new Success(false, "\nNo product found for this barcode");
			}
		}

		public Success RemoveItem(int barCode, int amount = 1)
		{
			IProduct product = Controllers.ProductController.GetItemByCode(barCode);

			if (product != null)
			{
				if (CartItems.TryGetValue(product, out int amountInCart))
				{
					if(amountInCart <= amount)
					{
						CartItems.Remove(product);
					}
					else
					{
						CartItems[product] -= amount;
					}

					return new Success(true);
				}
				else
				{
					return new Success(true, "\nNo such item in the cart");
				}
			}
			else
			{
				return new Success(false, "\nInvalid barcode");
			}
		}

		public decimal GetTransactionValue()
		{
			decimal value = 0m;

			foreach(KeyValuePair<IProduct, int> itemCount in CartItems)
			{
				value += itemCount.Key.UnitPrice * itemCount.Value;
			}

			return value;
		}

		public override string ToString()
		{
			if(CartItems.Count != 0)
			{
				StringBuilder contents = new StringBuilder("\nCart currently contains:");

				foreach (KeyValuePair<IProduct, int> itemCount in CartItems)
				{
					contents.Append(string.Format("\n{0} times {1}", itemCount.Value.ToString(), itemCount.Key.Description));
				}

				contents.Append(string.Format("\n\nFor a total value of: {0}\n\n", GetTransactionValue().ToString()));
			
				return contents.ToString();
			}
			else
			{
				return "\nCart is empty";
			}
		}

		protected virtual void RaiseProductAdded(IProduct product, int amount)
		{
			ProductAddedEventHandler copy = ProductAdded;

			if (copy != null)
			{
				copy.Invoke(this, new ProductAddedEventArgs { product = product, amount = amount });
			}
		}
	}
}
