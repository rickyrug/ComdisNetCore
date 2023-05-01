using System;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Models
{
    public class AuditFields
    {


        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_CreatedBy")]
        
        public string? CreatedBy { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Created")]
        public DateTime Cretead { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_UpdatedBy")]
        public string? UpdatedBy { get; set; }

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Updated")]
        public DateTime Updated { get; set; }

        public AuditFields()
        {

            
        }
    }
}
