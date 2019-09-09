using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.CustomArgs
{
	public class BarcodeScannedEventArgs : EventArgs
	{
		public string BarcodeStr { get; }
		public bool AddToCart { get; }

		public BarcodeScannedEventArgs(string barCode, bool addToCart = true)
		{
			this.BarcodeStr = barCode;
			this.AddToCart = addToCart;
		}
	}
}
