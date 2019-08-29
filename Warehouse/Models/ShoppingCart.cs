using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	public class ShoppingCart : IShoppingCart
	{
		public Dictionary<IProduct, int> CartItems { get; private set; }

		public ShoppingCart()
		{
			CartItems = new Dictionary<IProduct, int>();
		}

		public Success AddItem(int barCode, int amount = 1)
		{
			IProduct product = Controllers.ProductController.GetItemByCode(barCode);

			if(product != null)
			{
				if(CartItems.ContainsKey(product))
				{
					CartItems[product] += amount;
				}
				else
				{
					CartItems.Add(product, amount);
				}

				return new Success(true);
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

				return contents.ToString();
			}
			else
			{
				return "\nCart is empty";
			}
		}
	}
}
