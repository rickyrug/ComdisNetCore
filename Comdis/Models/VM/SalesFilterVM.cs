using System;
namespace Comdis.Models.VM
{
    public class SalesFilterVM
    {
        
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }
        public int salesnumber { get; set; }
        public int customerList { get; set; }

        public SalesFilterVM()
        {
        }
    }
}
