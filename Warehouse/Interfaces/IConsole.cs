﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Interfaces
{
	public interface IConsole
	{
		void PerformTransaction();
		void Print(string info);
	}
}
