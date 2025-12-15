using SMApp.Models;

namespace SMApp.Services.Contracts;

public interface IOrganisationService
{
    Task<List<OrganisationModel>> GetHierarchy();
}
