using FrameworkLeads.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLeads.Models
{
    public enum LeadStatus
    {
        AWAITING_REVIEW = 0,
        ACCEPTED = 1,
        REJECTED = 2
    }

    public class Lead
    {
        public int Id { get; set; }

        [Column("CONTACT_FIRSTNAME")]
        public string ContactFirstName { get; set; }
        
        [Column("CONTACT_LASTNAME")]
        public string ContactLastName { get; set; }
        
        [Column("CONTACT_PHONE")] 
        public string ContactPhone { get; set; }
        
        [Column("CONTACT_EMAIL")] 
        public string ContactEmail { get; set; }
        
        public DateTime DateCreated { get; set; }
        public string Suburb { get; set; }
        public string Category { get; set; }
        public LeadStatus Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
