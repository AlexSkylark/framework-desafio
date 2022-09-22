using FrameworkLeads.Models;

namespace FrameworkLeads.Services
{
    public interface ILeadsService
    {
        Task<int> CreateNewLead(Lead lead);
        Task DeleteLead(int id);
        Task<Lead> GetLeadByID(int id);
        Task<IEnumerable<Lead>> GetLeads();
        Task UpdateLead(Lead lead);
        Task UpdateLeadStatus(int id, LeadStatus status);
    }
}