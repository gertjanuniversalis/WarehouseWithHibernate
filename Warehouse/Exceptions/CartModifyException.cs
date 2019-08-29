using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Exceptions
{
	class CartModifyException : Exception
	{
		public CartModifyException()
		{
		}

		public CartModifyException(string message) : base(message)
		{
		}

		public CartModifyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CartModifyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
