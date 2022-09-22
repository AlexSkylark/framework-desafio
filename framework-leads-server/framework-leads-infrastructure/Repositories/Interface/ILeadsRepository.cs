using FrameworkLeads.Models;

namespace FrameworkLeads.Data
{
    public interface ILeadsRepository
    {
        Task<int> CreateNew(Lead lead);
        Task Delete(int id);
        Task<IEnumerable<Lead>> QueryAll();
        Task<Lead> QueryById(int id);
        Task Update(Lead lead);
    }
}