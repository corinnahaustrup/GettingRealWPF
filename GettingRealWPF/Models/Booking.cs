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
        public int BookingId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }

        //constructor 
        public Booking(int bookingId, DateTime dateTime, string status)
        {
            this.BookingId = bookingId;
            this.DateTime = dateTime;
            this.Status = status;
        }

        public string GetStatus()
        {
            return Status; 
        }


        public bool Validate()
        {
            //!stri... status er ikke null eller tom
            //tjekker om bookingtid er i fremtiden og status er udfyldt
            return DateTime > DateTime.Now && !string.IsNullOrEmpty(Status);
        }


        public bool Confirm()
        {
            if (Validate())
            {
                Status = "Confirmed";
                return true;
            }
            return false;
        }

        public bool Cancel()
        {  //kan annuleres uanset status, bortset fra hvis den allerede er annulleret
            if (Status != "Cancelled")
            {
                Status = "Cancelled";
                return true;
            }
            return false; //allerede annulleret
        }


    }
}
