using GettingRealWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GettingRealWPF.Repositories
{
    internal class ServiceRepository
    {
        private readonly List<Service> services;
        //Låseobjektet
        private readonly object sync = new();
        public ServiceRepository()
        {
            services = new List<Service>
            {
                new Service(1, "Hårklip", 200m, TimeSpan.FromMinutes(30)),
                new Service(2, "Hårklip - Hår + skæg", 250m, TimeSpan.FromMinutes(30)),
                new Service(3, "Hårklip - Barbering", 70m, TimeSpan.FromMinutes(30)),
                new Service(4, "Hårklip - Hår + skæg + maske", 270m, TimeSpan.FromMinutes(30)),
                new Service(5, "Hårklip - Hår + maske", 230m, TimeSpan.FromMinutes(30)),
                new Service(6, "Hårklip - Pension/børn u12", 150m, TimeSpan.FromMinutes(30)),
                new Service(7, "Hårklip - All in", 300m, TimeSpan.FromMinutes(30)),
            };
        }

        //Hent alle services 
        public List<Service> GetAllServices()
        {
            return services;
        }

        //Hent en service efter id
        //GetById(int id) leder efter servicen med det angivne serviceId
        //Service? betyder at metoden enten returnere et service-objekt eller null
        //FirstOrDefault finder første element i en liste som matcher betingelsen ellers default
        //s parameternavn der repræsenterer ét element i List<Service> 
        //=> går til eller betyder at vi definerer funktionen sådan
        //s.ServiceId == id er betingelsen, er s.ServiceId lig med det id der søges
        public Service? GetById(int id)
            {
            return services.FirstOrDefault(s => s.ServiceId == id);
            }
    
            }

        }




