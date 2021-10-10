using System;
using System.Collections.Generic;
using System.Linq;
using Comdis.Models;

namespace Comdis.Comdis.Models.VM
{

    public class SupplierVM: AuditFields
    {
        public int Id{get;set;}
        public string Name {get;set;}
        public string Adress {get;set;}
        public string Adress2 {get;set;}
        public string Phone1 {get;set;}
        public string Phone2 {get;set;}
        public string RFC {get;set;}
        public string email {get;set;} 
        public string BankAccount {get;set;}
        public int SuscribedBankId {get;set;}
        public SupplierVM()
        {
            
        }

    }


}