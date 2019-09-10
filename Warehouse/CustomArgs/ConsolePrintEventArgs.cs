﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.CustomArgs
{
	public class ConsolePrintEventArgs : EventArgs
	{
		public string TextToDisplay { get; }

		public ConsolePrintEventArgs(string text)
		{
			TextToDisplay = text;
		}
	}
}
