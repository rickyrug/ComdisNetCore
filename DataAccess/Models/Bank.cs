using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace DataAccess.Models
{
    public class Bank: AuditFields
    {

        public int Id { get; set; }


        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Bank")]
        public string Name { get; set; }

        public Bank(){}

        
    }
}
