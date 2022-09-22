using FrameworkLeads.Models;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FrameworkLeads.Data
{
    public class LeadsRepository : ILeadsRepository
    {
        private readonly IDapperBase _dapperBase;
        private readonly IConfiguration _configuration;
        string connString = "";

        public LeadsRepository(IConfiguration configuration, IDapperBase dapperBase)
        {
            _dapperBase = dapperBase;
            _configuration = configuration;

            connString = configuration["DatabaseConnectionString"];
        }

        public async Task<Lead> QueryById(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                var result = await _dapperBase.QueryAsync<Lead>(connection, "select * from Leads where id = @Id", new { Id = id });

                if (result == null || result.Count() == 0) throw new KeyNotFoundException("Lead Not Found");

                return result.First();
            }
        }

        public async Task<IEnumerable<Lead>> QueryAll()
        {
            using (var connection = new SqlConnection(connString))
            {
                var result = await _dapperBase.QueryAsync<Lead>(connection, "select * from Leads");

                if (result == null) throw new KeyNotFoundException("Could not find any Leads");

                return result;
            }
        }

        public async Task<int> CreateNew(Lead lead)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sql = @"INSERT INTO Leads
                                ([CONTACT_FIRSTNAME], [CONTACT_LASTNAME], [CONTACT_PHONE], [CONTACT_EMAIL], [DATE_CREATED], [SUBURB], [CATEGORY], [DESCRIPTION], [PRICE])
                            VALUES
                                (@ContactFirstName, @ContactLastName, @ContactPhone, @ContactEmail, GETDATE(), @Suburb, @Category, @Description, @Price);
                                 SELECT CAST(SCOPE_IDENTITY() as int)";

                return await _dapperBase.QuerySingleAsync<int>(connection, sql, lead);
            }
        }

        public async Task Update(Lead lead)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sql = @"UPDATE Leads
                                SET [CONTACT_FIRSTNAME] = @ContactFirstName,
                                    [CONTACT_LASTNAME] = @ContactLastName,
                                    [CONTACT_PHONE] = @ContactPhone,
                                    [CONTACT_EMAIL] = @ContactEmail,
                                    [SUBURB] = @Suburb,
                                    [CATEGORY] = @Category,
                                    [STATUS] = @Status,
                                    [DESCRIPTION] = @Description, 
                                    [PRICE] = @Price
                                
                                WHERE [ID] = @Id";

                await _dapperBase.ExecuteAsync(connection, sql, lead);
            }
        }       

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(connString))
            {
                var sql = @"DELETE FROM Leads WHERE [ID] = @Id";

                var result = await _dapperBase.ExecuteAsync(connection, sql, new { Id = id });

                if (result == 0) throw new KeyNotFoundException("Lead ID not found");
            }
        }
    }
}


