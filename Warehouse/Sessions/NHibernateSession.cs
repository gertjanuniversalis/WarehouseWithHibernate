using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Cfg;

using Warehouse.Exceptions;

namespace Warehouse.Sessions
{
	/// <summary>
	/// Provides the Hibernate connection to the database
	/// </summary>
	public static class NHibernateSession
	{
		public static ISession OpenSession()
		{
			try
			{
				string assemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

				var configuration = new Configuration();
				var configPath = assemblyPath + @"\hibernate.cfg.xml";
				configuration.Configure(configPath);

				ISessionFactory sessionFactory = configuration.BuildSessionFactory();

				return sessionFactory.OpenSession();
			}
			catch (Exception e)
			{
				throw new NHibernateSessionException(e.InnerException.ToString());
			}
		}
	}
}
