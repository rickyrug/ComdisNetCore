using System;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repository
{
	public class ConfigurationRepository:GenericRepository<Configuration>, IConfiguration
	{
		public ConfigurationRepository(ComdisContext context):base(context)
		{
		}
	}
}

