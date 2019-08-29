using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Interfaces;

namespace Warehouse.Controllers
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
			//TODO: implement
			throw new NotImplementedException();
		}

		/// <summary>
		/// Provides a string representation of the entire catalogue
		/// </summary>
		internal static string GetCatalogue()
		{
			//TODO: implement
			throw new NotImplementedException();
		}
	}
}
