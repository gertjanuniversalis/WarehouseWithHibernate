﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Cfg;

using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.Obsolete.Controllers
{
	/// <summary>
	/// Provides access to the Products table in the database
	/// </summary>
	class ProductController
	{
		/// <summary>
		/// Finds the first item with a specific barcode in the database
		/// </summary>
		/// <param name="barCode"></param>
		internal static IProduct GetItemByCode(int barCode)
		{
			using (ISession session = Sessions.NHibernateSession.OpenSession())
			{
				return session.QueryOver<Product>().Where(p => p.BarCode == barCode).List<Product>().FirstOrDefault();
			}
		}

		/// <summary>
		/// Provides a string representation of the entire catalogue
		/// </summary>
		internal static string GetCatalogue()
		{
			IProduct[] availableProducts = GetAll();

			if (availableProducts != null && availableProducts.Count() != 0)
			{
				StringBuilder catalogue = new StringBuilder("\nThe following products are available:");
				catalogue.Append("\nBarcode\tPrice\tDescription");

				foreach (IProduct product in availableProducts)
				{
					catalogue.Append(string.Format("\n{0}\t{1}\t{2}", product.BarCode.ToString(), product.UnitPrice, product.Description));
				}

				return catalogue.ToString();
			}
			else
			{
				return "\nNo products available for purchase";
			}
		}

		/// <summary>
		/// Gets all entries in the 'products' table
		/// </summary>
		internal static IProduct[] GetAll()
		{
			using (ISession session = Sessions.NHibernateSession.OpenSession())
			{
				return session.QueryOver<Product>().List<Product>().ToArray();
			}
		}

#pragma warning disable CS0246 // The type or namespace name 'OrderedProduct' could not be found (are you missing a using directive or an assembly reference?)
		internal static List<OrderedProduct> GetProductsByOrder(IOrder order)
#pragma warning restore CS0246 // The type or namespace name 'OrderedProduct' could not be found (are you missing a using directive or an assembly reference?)
		{
			using (ISession session = Sessions.NHibernateSession.OpenSession())
			{
				return (List<OrderedProduct>)session.QueryOver<OrderedProduct>().Where(op => op.Order == order).List<OrderedProduct>();
			}
		}
	}
}
