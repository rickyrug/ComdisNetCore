using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Comdis.Comdis.Models;

namespace Comdis.Models
{
    public class Sales : AuditFields
    {

        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Customer")]
        public Customer SalesToParty { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_RequestedDeliveryDate")]
        public DateTime RequestedDeliveryDate { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_DeliveryAdress")]
        public string DeliveryAdress { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Comments")]
        public string Comments { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Discount1")]
        public decimal discount { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Discount2")]
        public decimal discount2 { get; set; }


        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Discount3")]
        public decimal discount3 { get; set; }
        public List<SalesItems> SalesItems { get; set; }


        public Sales(){ }
    }
}
