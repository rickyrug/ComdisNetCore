using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Bank: AuditFields
    {

        public int Id { get; set; }


        [Display(ResourceType = typeof(Resources.Resources), Name = "Label_Bank")]
        [Required]
        public string Name { get; set; }

        public Bank(){}

        
    }
}
