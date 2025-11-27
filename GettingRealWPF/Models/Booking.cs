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
        public bool Create()
        {
            return Validate();
        }
        public bool Cancel()
        { 
        if (status !="Cancelled")
            {
                status = "Cancelled";
                return true;
            }
            return false;
        }
        public string GetStatus()
        {
            return status; 
        }

        public bool Validate()
        {
            //!stri... status er ikke null eller tom
            //tjekker om bookingtid er i fremtiden og status er udfyldt
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
