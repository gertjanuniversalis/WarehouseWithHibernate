﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.CustomArgs;
using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.Controllers
{
	public class PaymentController
	{
		public TillDrawer TillDrawer { get; }

		public PaymentController(TillDrawer tillDrawer)
		{
			this.TillDrawer = tillDrawer;
		}

		/// <summary>
		/// Creates a Payout cashset from a set of cashitems
		/// </summary>
		/// <param name="valueToPay">The desired value of the returned cashset</param>
		/// <param name="drawerContent">The cashset from which to return a subset for payout</param>
		/// <returns></returns>
		public ICashSet Payout(decimal valueToPay, ICashSet drawerContent)
		{
			ICashSet payOut = CashController.SmallestSetForValue(valueToPay, drawerContent);

			if (payOut != null)
			{
				//Return the payout set
				return payOut;
			}
			else
			{
				//Round the payout set to the closest 5 cents and try again
				decimal roundedValue = Math.Round(valueToPay * 20) / 20;

				return CashController.SmallestSetForValue(roundedValue, drawerContent);
			}
		}

		public void DetermineChange(object s, PaymentEventArgs pe)
		{

		}
	}
}
