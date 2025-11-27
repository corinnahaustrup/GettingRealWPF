using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingRealWPF.Models
{
    public class Booking
    {
        //private felter
        private int bookingId;
        private DateTime dateTime;
        private string status;

        //constructor 
        public Booking(int bookingId, DateTime dateTime, string status)
        {
            this.bookingId = bookingId;
            this.dateTime = dateTime;
            this.status = status;
        }
        public string GetStatus()
        {
            return status; 
        }

        public bool Validate()
        {
            //d>D bookingtid er i fremtiden ikke nu,
            //!stri... status er ikke null eller tom
            return dateTime > DateTime.Now && !string.IsNullOrEmpty(status);
        }

        public bool Confirm()
        {
            if (Validate())
            {
                status = "Confirmed";
                return true;
            }
            return false;
        }
       
    }
}
