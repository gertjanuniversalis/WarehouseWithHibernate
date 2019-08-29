using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	public interface ICatalogue
	{
		IProduct[] ProductsOnOffer { get; }

		/// <summary>
		/// Find a product based on the barcode
		/// </summary>
		/// <returns>The IProduct requested, or <c>null</c></returns>
		IProduct GetProductByCode(string code);


		/// <returns>All products in the catalogue</returns>
		IList<IProduct> GetAllProducts();

		/// <summary>
		/// Prints all items in the catalogue
		/// </summary>
		string ToString();
	}
}
