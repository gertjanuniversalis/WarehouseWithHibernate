using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Exceptions
{
	class NHibernateSessionException : Exception
	{
		public NHibernateSessionException()
		{
		}

		public NHibernateSessionException(string message) : base(message)
		{
		}

		public NHibernateSessionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected NHibernateSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
