using System;
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
		public event EventHandler<ConsolePrintEventArgs> ChangeFound;
		public event EventHandler<PaymentCompletedEventArgs> PaymentPossible;
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

		public void DetermineChange(object s, ProvideChangeEventArgs pce)
		{
			CashSet transactionSet = new CashSet(TillDrawer.Contents);
			CashSet givenSet = (CashSet)CashController.SmallestSetForValue(pce.CashGiven);
			transactionSet.Add(givenSet);

			decimal valueToPayout = pce.CashGiven - pce.TransactionValue;

			CashSet setToReturn = MakeChangeSet(valueToPayout, transactionSet);

			if (setToReturn != null)
			{
				RaisePrintReturnCash(setToReturn);
				RaisePaymentPossible(givenSet, setToReturn);
			}
			else
			{
				RaiseNoChangeFound();
			}
		}
		
		private CashSet MakeChangeSet(decimal valueToPayout, CashSet transactionSet)
		{
			decimal remainder = valueToPayout;
			CashSet changeSet = new CashSet();

			foreach(var cashType in transactionSet.CashStack)
			{
				int amountNeeded = (int)(remainder / cashType.Key.UnitValue);

				if(amountNeeded == 0 || cashType.Value == 0)
				{
					//we either don't need this type of cash, or we don't have any
					continue;
				}
				else if (cashType.Value >= amountNeeded)
				{
					changeSet.Add(cashType.Key, amountNeeded);
					remainder -= cashType.Key.UnitValue * amountNeeded;
				}
				else
				{
					changeSet.Add(cashType.Key, cashType.Value);
					remainder -= cashType.Key.UnitValue * cashType.Value;
				}

				if(remainder == 0)
				{
					return changeSet;
				}
			}

			if (remainder <= 0.05m)
			{
				decimal roundedSet = (Math.Round(10 * remainder, 2, MidpointRounding.AwayFromZero)) / 10;
				return MakeChangeSet(roundedSet, transactionSet);
			}
			else
			{
				return null;
			}
		}


		private void RaisePrintReturnCash(CashSet setToReturn)
		{
			string changeNotification = string.Format("Return {0}, distributed as {1}",
				setToReturn.GetSum().ToString(),
				setToReturn.AsString());

			ChangeFound?.Invoke(this, new ConsolePrintEventArgs(changeNotification));

		}
		private void RaiseNoChangeFound()
		{
			ChangeFound?.Invoke(this, new ConsolePrintEventArgs("Unable to attribute change"));
		}

		private void RaisePaymentPossible(CashSet cashGiven, CashSet setToReturn)
		{
			PaymentPossible?.Invoke(this, new PaymentCompletedEventArgs(cashGiven, setToReturn));
		}
	}
}
