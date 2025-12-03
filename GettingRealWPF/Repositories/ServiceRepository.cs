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
                new Service(1, "Hårklip - Hår + skæg", 250m, TimeSpan.FromMinutes(30)),
                new Service(2, "Hårklip - Barbering", 70m, TimeSpan.FromMinutes(30)),
                new Service(3, "Hårklip - Hår + skæg + maske", 270m, TimeSpan.FromMinutes(30)),
                new Service(2, "Hårklip - Hår + maske", 230m, TimeSpan.FromMinutes(30)),
                new Service(2, "Hårklip - Pension/børn u12", 150m, TimeSpan.FromMinutes(30)),
                new Service(2, "Hårklip - All in", 300m, TimeSpan.FromMinutes(30)),

            };

        }

        //Returnerer en sikker snapshot-kopi af alle services
        public IEnumerable<Service> GetAllServices()
        {
            lock (sync)
            {
        //ToList() laver en ny liste (snaphot). Det er normalt det bedste valg for GetAll().
                return services.ToList();
            }
        }

        //Hent service efter id. Retunerer null hvis ikke fundet.
        public Service? GetById(int id)
        {
             lock (sync)
             {
                    return services.FirstOrDefault(s => s.ServiceId == id);
             }
        }

        public Service Add(Service service)
        {
            if (service is null) throw new ArgumentNullException(nameof(service));

            lock (sync)
            {
                var nextId = services.Any() ? services.Max(s => s.ServiceId) + 1 : 1;
                service.ServiceId = nextId;
                services.Add(service);
                return service;
            }

        }

    }
}


