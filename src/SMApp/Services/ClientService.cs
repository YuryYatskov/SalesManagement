using SMApp.Data;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class ClientService(SalesManagementDbContext _dbContext) : IClientService
{
    public async Task<List<ClientModel>> GetClients()
    {
        try
        {
            return await _dbContext.Clients.Convert(_dbContext);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
