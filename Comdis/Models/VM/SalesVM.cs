using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Comdis.Models.VM
{
    public class SalesVM:AuditFields
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Customer")]
        public int SalesToPartyId { get; set; }

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

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_tax")]
        public bool tax { get; set; }


        public string CustomerName { get; set; }

        public List<SalesItemVM> salesItem { get; set; }

        //public int[] productID { get; set; }
        //public decimal[] quantity { get; set; }
        //public decimal[] price { get; set; }

        public SalesVM(){}
    }
}
