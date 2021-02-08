using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public bool Read { get; set; }
        public DateTime DateCreated { get; set; }
        public int OpportunityID { get; set; }
        public string UserID { get; set; }
    }
}
