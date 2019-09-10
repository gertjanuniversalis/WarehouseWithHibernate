using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;

using Warehouse.CustomArgs;
using Warehouse.Models;

namespace Warehouse.Controllers
{
	public class OrderController
	{
		internal void DisplayOrder(object sender, OrderEventArgs oe)
		{
			if(int.TryParse(oe.OrderIdStr, out int orderID))
			{
				Order oldOrder;
				using (ISession session = Sessions.NHibernateSession.OpenSession())
				{
					oldOrder = session.Query<Order>().Where(order => order.OrderID == orderID).FirstOrDefault();
				}

				if(oldOrder != null)
				{
					Console.WriteLine(oldOrder.AsString());
				}
				else
				{
					Console.WriteLine(string.Format("No data found for order number {0}", oe.OrderIdStr));
				}
			}
		}
	}
}
