using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class Companies
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string OwnershipType { get; set; }
        public string Adress { get; set; }
        public string LegalAdress { get; set; }
        public int CheckingAccount { get; set; }
        public string BankBin { get; set; }
        public int UNP { get; set; }
        public int OKPO { get; set; }
        public int ONPF { get; set; }
        public string FolderLanguage { get; set; }
    }
}
