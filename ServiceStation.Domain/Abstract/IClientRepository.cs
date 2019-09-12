using System.Linq;

using ServiceStation.Domain.Entities;

namespace ServiceStation.Domain.Abstract
{
    public interface IClientRepository
    {
        IQueryable<Client> Clients { get; }
        void SaveClient(Client client);
        Client DeleteClient(int clientId);
    }
}
