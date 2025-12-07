using SMApp.Models;

namespace SMApp.Services.Contracts;

public interface IClientService
{
    Task<List<ClientModel>> GetCliants();
}
