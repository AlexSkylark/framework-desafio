using Bogus;
using FrameworkLeads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLeads.Testing.Helpers
{
    internal static class ObjectMocks
    {               
        internal static Lead GetLead(int id = 0, LeadStatus status = LeadStatus.AWAITING_REVIEW)
        {
            // generate a lead with random data
            var lead = new Faker<Lead>()
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.ContactFirstName, f => f.Name.FirstName())
                .RuleFor(u => u.ContactLastName, f => f.Name.LastName())
                .RuleFor(u => u.ContactPhone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.ContactEmail, (f, u) => f.Internet.Email(u.ContactFirstName, u.ContactLastName))
                .RuleFor(u => u.DateCreated, f => f.Date.Recent())
                .RuleFor(u => u.Suburb, f => f.Address.StreetAddress())
                .RuleFor(u => u.Category, f => f.Commerce.Categories(1)[0])
                .RuleFor(u => u.Status, f => f.Random.Enum<LeadStatus>())
                .RuleFor(u => u.Description, f => f.Lorem.Sentence(8, 6))
                .RuleFor(u => u.Price, f => f.Random.Number(100, 400)).Generate();


            if (id != 0)
            {
                lead.Id = id;
            }
            
            lead.Status = status;
            
            return lead;
        }

        internal static List<Lead> GetLeadCollection()
        {
            var leadList = new List<Lead>();

            // generate between 5 and 10 leads
            var leadCount = new Random().Next(5, 10);
            for (int i = 0; i < leadCount; i++)
            {
                leadList.Add(GetLead());
            }

            return leadList;
        }
    }
}
