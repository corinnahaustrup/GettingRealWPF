using GettingRealWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GettingRealWPF.Repositories
{
    internal class BookingRepository
    {
        public bool SaveBooking()
        {
            return true;
        }
        public List<Booking> GetBookings()
        {
            return new List<Booking>
            {
                new Booking(1, DateTime.Now, "confirmed"),
                new Booking(2, DateTime.Now, "confirmed"),
                new Booking(3, DateTime.Now, "cancelled"),
            };
        }
        public List<DateTime> GetAvailableTimeSlots()
        {
            return (List<DateTime>)[];
        }

    }
}
