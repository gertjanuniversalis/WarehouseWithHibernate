using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Linq;

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

		/// <summary>
		/// Gets an existing order from the DB
		/// </summary>
		/// <param name="orderID">The order to return</param>
		internal static IOrder GetOrder(int orderID)
		{
			using (ISession session = Sessions.NHibernateSession.OpenSession())
			{
				var order = session.Query<Order>().Where(o => o.OrderID == orderID).Fetch(o => o.OrderedProducts).ToFuture();
				session.Query<OrderedProduct>().Where(op => op.Order.OrderID == orderID).Fetch(op => op.Product).ToFuture();

				return order.FirstOrDefault();
			}
		}
	}
}
