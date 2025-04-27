using System;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class PriceListRepository : GenericRepository<PriceList>, IPriceList
    {
        public PriceListRepository(ComdisContext context) : base(context)
        {
        }
    }
}

