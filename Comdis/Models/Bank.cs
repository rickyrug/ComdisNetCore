using System;
using System.ComponentModel;

namespace Comdis.Models
{
    public class Bank: AuditFields
    {

        public int Id { get; set; }

       
       [DisplayName("Bank Name")] 
        public string Name { get; set; }

        public Bank(){}

        
    }
}
