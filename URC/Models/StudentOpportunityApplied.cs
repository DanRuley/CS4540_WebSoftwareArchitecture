using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class StudentOpportunityApplied
    {
        public int ID { get; set; }
        public int StudentApplicationID { get; set; }
        public int ResearchOpportunityID { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
