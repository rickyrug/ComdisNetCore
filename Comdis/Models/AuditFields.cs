using System;
namespace Comdis.Models
{
    public class AuditFields
    {
        public string CreatedBy { get; set; }
        public DateTime Cretead { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }

        public AuditFields()
        {

            
        }
    }
}
