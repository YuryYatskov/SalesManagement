using SMApp.Data;
using SMApp.Extensions;
using SMApp.Models;
using SMApp.Services.Contracts;

namespace SMApp.Services;

public class OrganisationService(SalesManagementDbContext _dbContext) : IOrganisationService
{
    public async Task<List<OrganisationModel>> GetHierarchy()
    {
        try
        {
            return await _dbContext.Employees.ConvertToHierarchy(_dbContext);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
