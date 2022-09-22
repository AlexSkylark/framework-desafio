using FrameworkLeads.Data;
using FrameworkLeads.Helpers;
using FrameworkLeads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLeads.Services
{
    public class LeadsService : ILeadsService
    {

        private readonly ILeadsRepository _leadsRepository;

        public LeadsService(ILeadsRepository leadsRepository)
        {
            _leadsRepository = leadsRepository;
        }

        public async Task<Lead> GetLeadByID(int id)
        {
            return await _leadsRepository.QueryById(id);
        }

        public async Task<IEnumerable<Lead>> GetLeads()
        {
            return await _leadsRepository.QueryAll();
        }

        public async Task<int> CreateNewLead(Lead lead)
        {
            return await _leadsRepository.CreateNew(lead);
        }

        public async Task UpdateLead(Lead lead)
        {
            await _leadsRepository.Update(lead);
        }

        public async Task DeleteLead(int id)
        {
            await _leadsRepository.Delete(id);
        }

        public async Task UpdateLeadStatus(int id, LeadStatus status)
        {
            var lead = await _leadsRepository.QueryById(id);

            // validações
            if (lead.Status != LeadStatus.AWAITING_REVIEW)
            {
                throw new MalformedDataException("The lead has already been reviewed.");
            }            

            lead.Status = status;
            if (status == LeadStatus.ACCEPTED && lead.Price >= 500)
                lead.Price = Math.Round(lead.Price * 0.9m, 2);

            await _leadsRepository.Update(lead);

            SendEmail(lead);
        }

        private void SendEmail(Lead lead)
        {
            // TODO: implementar rotina de e-mail
        }
    }
}
