using System;
using Comdis.Comdis.Models;

namespace Comdis.Models
{
    public class Sales : AuditFields
    {

        public int Id { get; set; }
        public Customer SalesToParty { get; set; }
        public DateTime RequestedDeliveryDate { get; set; }
        public string DeliveryAdress { get; set; }
        public string Comments { get; set; } 
        public decimal discount { get; set; }
        public decimal discount2 { get; set; }
        public decimal discount3 { get; set; }
        public Sales(){ }
    }
}
