using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	//TODO: Determine usefulness of this class
	class Catalogue : ICatalogue
	{
		public IProduct[] ProductsOnOffer { get; private set; }

		public Catalogue()
		{
			RefleshProductList();
		}
		public IList<IProduct> RefleshProductList()
		{
			throw new NotImplementedException();
		}

		public IProduct GetProductByCode(string code)
		{
			throw new NotImplementedException();
		}
	}
}
