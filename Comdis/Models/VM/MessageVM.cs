using System;
namespace Comdis.Models.VM
{
    public class MessageVM<T>
    {

        public bool hasError { get; set; }
        public T Message {get;set;}

        public MessageVM()
        {
        }
    }
}
