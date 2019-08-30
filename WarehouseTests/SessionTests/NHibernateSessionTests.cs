using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;

using NUnit.Framework;

namespace Warehouse.SessionTests
{
	[TestFixture]
	class NHibernateSessionTests
	{
		[Test]
		public void CanCreateSession()
		{
			try
			{
				ISession session = Sessions.NHibernateSession.OpenSession();

				Assert.IsNotNull(session);
			}
			catch
			{
				Assert.Fail();
			}
		}
	}
}
