using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	public class OrderTransaction
	{
		public IOrder Order { get; set; }
		public int CustomerID { get; set; }
		public DateTime OrderDate { get; set; }

		private ISession session;
		private ITransaction transaction;

		public OrderTransaction()
		{

		}

		~OrderTransaction()
		{
			if (!session.IsOpen)
			{
				session.Clear();
				session.Close();
			}
		}

		public void StartTransaction(int userID)
		{
			this.CustomerID = userID;
			this.OrderDate = DateTime.Now;

			session = Sessions.NHibernateSession.OpenSession();
			transaction = session.BeginTransaction();

			CreateOrder();
		}

		private void CreateOrder()
		{
			Order = new Order { CustomerID = this.CustomerID, OrderDate = OrderDate };

			session.Save(Order);
		}

		public void AddToOrder(IProduct product, int amount)
		{
			var thisproduct = new OrderedProduct { Order = (Order)Order, Product = (Product)product, Quantity = amount };
			session.Save(thisproduct);

			Order.OrderedProducts.Add(thisproduct);
		}

		public void Commit()
		{
			transaction.Commit();
			session.Close();
		}

		public void Revert()
		{
			transaction.Rollback();
			session.Close();
		}
	}
}
