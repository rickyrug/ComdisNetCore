using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;


namespace DataAccess.Models
{

    public class Customer: AuditFields
    {
        public int Id{get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Name")]
        public string Name {get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Adress")]
        public string Adress {get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Adress2")]
        public  string? Adress2 {get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Phone")]
        public string Phone1 {get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Phone2")]
        public string? Phone2 {get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_RFC")]
        public string RFC {get;set;}

        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Email")]
        public string email {get;set;} 
        
        public Customer()
        {
            
        }

    }


}