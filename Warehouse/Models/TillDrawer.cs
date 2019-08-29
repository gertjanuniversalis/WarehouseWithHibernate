using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Warehouse.Interfaces;

namespace Warehouse.Models
{
	class TillDrawer : ITillDrawer
	{
		public ICashSet DrawerContent { get; private set; }

		public TillDrawer(ICashSet initialContent)
		{
			this.DrawerContent = initialContent;
		}

		public void Add(decimal value)
		{
			ICashSet setToAdd = Controllers.CashController.SmallestSetForValue(value);
			Add(setToAdd);
		}

		public void Add(ICashSet changeSet)
		{
			foreach (KeyValuePair<ICash, int> pair in changeSet.CashStack)
			{
				ChangeContent(pair.Key, pair.Value);
			}
		}

		public void ChangeContent(ICash cashItem, int difference)
		{
			DrawerContent.Edit(cashItem, difference);
		}

		public bool ContainsEnough(decimal changeSum)
		{
			return DrawerContent.GetSum() >= changeSum;
		}

		public decimal GetTotalValue()
		{
			decimal drawerValue = 0m;

			foreach (KeyValuePair<ICash, int> item in DrawerContent.CashStack)
			{
				drawerValue += item.Key.UnitValue * item.Value;
			}

			return drawerValue;
		}

		public ICashSet MakeChange(decimal changeAmount)
		{
			return Controllers.CashController.SmallestSetForValue(changeAmount, DrawerContent);
		}

		public void Remove(decimal value)
		{
			ICashSet setToRemove = Controllers.CashController.SmallestSetForValue(value);
			Remove(setToRemove);
		}

		public void Remove(ICashSet changeSet)
		{
			foreach (KeyValuePair<ICash, int> pair in changeSet.CashStack)
			{
				ChangeContent(pair.Key, -pair.Value);
			}
		}

		public override string ToString()
		{
			return string.Format("Drawer contains: {0}\n distrubuted as:\n{1}",
				DrawerContent.GetSum().ToString(),
				DrawerContent.ToString());
		}
	}
}
