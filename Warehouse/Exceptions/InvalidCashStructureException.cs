using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Exceptions
{
	class InvalidCashStructureException : Exception
	{
		public InvalidCashStructureException()
		{
		}

		public InvalidCashStructureException(string message) : base(message)
		{
		}

		public InvalidCashStructureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidCashStructureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
