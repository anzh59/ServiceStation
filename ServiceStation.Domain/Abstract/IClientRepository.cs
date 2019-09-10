using ServiceStation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Domain.Abstract
{
    public interface IClientRepository
    {
        IQueryable<Client> Clients { get; }
        void SaveClient(Client client);
        Client DeleteClient(int clientId);
    }
}
