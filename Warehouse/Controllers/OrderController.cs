using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.Controllers
{
	/// <summary>
	/// Provides methods to manipulate orders and the Orders/OrderedProducts Database tables
	/// </summary>
	public static class OrderController
	{
		internal static Success SaveOrder(int userID, IShoppingCart cart)
		{
			IOrderTransaction order = new OrderTransaction();

			try
			{
				order.StartTransaction(userID);

				foreach (KeyValuePair<IProduct, int> product in cart.CartItems)
				{
					order.AddToOrder(product.Key, product.Value);
				}

				order.Commit();

				return new Success(true, "Order saved successfully");
			}
			catch
			{
				order.Revert();

				return new Success(false, "Order failed, try again");
			}
		}
	}
}
