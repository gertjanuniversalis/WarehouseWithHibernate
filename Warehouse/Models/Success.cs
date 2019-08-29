using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Models
{
	public class Success
	{
		public bool Result { get; set; }
		public string ResultComment { get; set; } = "";

		public Success()
		{

		}

		public Success(bool result)
		{
			Result = result;
		}

		public Success(bool result, string comment)
		{
			Result = result;
			ResultComment = comment;
		}
	}
}
