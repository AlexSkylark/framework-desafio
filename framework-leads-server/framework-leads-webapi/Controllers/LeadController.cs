using FrameworkLeads.Models;
using FrameworkLeads.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrameworkLeads.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadsService _leadsService;

        public LeadController(ILeadsService leadsService)
        {
            _leadsService = leadsService;
        }

        /// <summary>
        /// Recupera todas as leads do sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Lead>> Get()
        {
            return await _leadsService.GetLeads();
        }
        /// <summary>
        /// Recupera uma Lead do sistema a partir do seu ID.
        /// </summary>
        /// <param name="id">ID da lead a ser recuperada.</param>
        /// <returns>Lead recuperada.</returns>
        [HttpGet("{id}")]
        public async Task<Lead> GetById(int id)
        {
            return await _leadsService.GetLeadByID(id);
        }

        /// <summary>
        /// Cria uma lead
        /// </summary>
        /// <param name="lead">JSON da lead a ser criada. Não informar "id" ou "dateCreated"</param>
        /// <returns>ID da lead criada</returns>
        [HttpPost]
        public async Task<int> Create(Lead lead)
        {
            return await _leadsService.CreateNewLead(lead);
        }

        /// <summary>
        /// Modifica uma lead
        /// </summary>
        /// <param name="id">ID da lead a ser modificada</param>
        /// <param name="lead">JSON com os novos dados da lead a ser modificada</param>        
        [HttpPut("{id}")]
        public async Task Update(int id, Lead lead)
        {
            lead.Id = id;
            await _leadsService.UpdateLead(lead);
        }
        /// <summary>
        /// apaga uma lead do sistema
        /// </summary>
        /// <param name="id">ID da lead a ser apagada.</param>
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _leadsService.DeleteLead(id);
        }

        /// <summary>
        /// Aceita uma lead.
        /// </summary>
        /// <param name="id">ID da lead a ser aceita.</param>
        [HttpPut("{id}/accept")]
        public async Task acceptLead(int id)
        {
            await _leadsService.UpdateLeadStatus(id, LeadStatus.ACCEPTED);
        }

        /// <summary>
        /// Rejeita uma lead.
        /// </summary>
        /// <param name="id">ID da lead a ser rejeitada.</param>
        [HttpPut("{id}/reject")]
        public async Task rejectLead(int id)
        {
            await _leadsService.UpdateLeadStatus(id, LeadStatus.REJECTED);
        }
    }
}