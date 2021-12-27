﻿using System;
namespace Comdis.Models.VM
{
    public class SalesVM
    {
        public int Id { get; set; }
        public int SalesToPartyId { get; set; }
        public DateTime RequestedDeliveryDate { get; set; }
        public string DeliveryAdress { get; set; }
        public string Comments { get; set; }
        public decimal discount { get; set; }
        public decimal discount2 { get; set; }
        public decimal discount3 { get; set; }
        public int[] productID { get; set; }
        public decimal[] quantity { get; set; }
        public decimal[] price { get; set; }
        public SalesVM(){}
    }
}
