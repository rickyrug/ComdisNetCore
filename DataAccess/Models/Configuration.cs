using System;
namespace DataAccess.Models
{
    public class Configuration
    {

        public int Id { get; set; }
        public string code { get; set; }
        public string value { get; set; }

        public Configuration()
        {
        }
    }
}
