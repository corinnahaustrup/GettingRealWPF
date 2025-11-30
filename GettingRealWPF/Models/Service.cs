using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingRealWPF.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
    //Constructors
    public Service(int serviceId, string name, decimal price, TimeSpan duration) 
        {
            ServiceId = serviceId;
            Name = name;
            Price = price;
            Duration = duration;
        }
    }
}
