using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URC.Models
{
    public class OpportunityApplication
    {
        public int ID { get; set; }
        public int OppotunityID { get; set; }
        public string OpportunityName { get; set; }
        public List<StudentApplication> StudentApplication { get; set; }
    }
}

