using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingRealWPF.Models
{
   public class Customer
    {
        public int CustomerId {  get; set; }
        public string Name { get; set; }
        public int Phone {  get; set; }

        //constructor
        public Customer(int customerId, string name, int phone)
        {
            //initialisere instansfelterne
        CustomerId = customerId;
            Name = name;
            Phone = phone;
        
        }

    }
}
