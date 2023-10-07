using System;
using System.Collections.Generic;
using Comdis.Models.VM;
using DataAccess.UnitOfWork;

namespace Comdis.BusinessRules
{
	public class SalesOrdersBusinessRules
	{
        private readonly UnitOfWork unitOfWork;

        public SalesOrdersBusinessRules(UnitOfWork punitOfWork)
		{
            this.unitOfWork = punitOfWork;
		}

		public SalesVM GetSalesOrderItemDetails(int id)
		{
            SalesVM SOI = new SalesVM();
            SOI.salesItem = new List<SalesItemVM>();

            try
            {
                var SO = this.unitOfWork.Sales.GetByID(id);
                //_context.Sales
                //.Include(soi => soi.SalesItems)
                //.ThenInclude(soi => soi.Product)
                //.Include(cu => cu.SalesToParty)
                //.AsNoTracking()
                //.Where(sa => sa.Id == id).FirstOrDefault();

                if (SO != null)
                {
                    SOI.Comments = SO.Comments;
                    SOI.DeliveryAdress = SO.DeliveryAdress;
                    SOI.discount = SO.discount;
                    SOI.discount2 = SO.discount2;
                    SOI.discount3 = SO.discount3;
                    SOI.Id = SO.Id;
                    SOI.RequestedDeliveryDate = SO.RequestedDeliveryDate;
                    SOI.SalesToPartyId = SO.SalesToParty.Id;
                    SOI.CustomerName = SO.SalesToParty.Name;
                    SOI.CreatedBy = SO.CreatedBy;
                    SOI.Cretead = SO.Cretead;
                    SOI.Updated = SO.Updated;
                    SOI.UpdatedBy = SO.UpdatedBy;


                    if (SO.SalesItems != null)
                    {
                        SOI.salesItem = new List<SalesItemVM>();

                        foreach (var item in SO.SalesItems)
                        {

                            SOI.salesItem.Add(new SalesItemVM
                            {
                                Price = item.Price,
                                ProductId = item.Product.Id,
                                ProductName = item.Product.Name,
                                Quantity = item.Quantity,
                                Cretead = item.Cretead,
                                CreatedBy = item.CreatedBy,
                                Updated = item.Updated,
                                UpdatedBy = item.UpdatedBy,
                                uid = item.Id
                            });
                        }
                    }
                }
                return SOI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

