using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Domain.Implementation
{
    public class EFClientRepository : IClientRepository
    {
        private EFDbContext _context;

        public EFClientRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Client> Clients
        {
            get { return _context.Clients; }
        }

        public void SaveClient(Client client)
        {
            if (client.Id == 0)
            {
                _context.Clients.Add(client);
            }
            else
            {
                Client dbEntry = _context.Clients.Find(client.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = client.Id;
                    dbEntry.FirstName = client.FirstName;
                    dbEntry.LastName = client.LastName;
                    dbEntry.Address = client.Address;
                    dbEntry.DateOfBirth = client.DateOfBirth;
                    dbEntry.Email = client.Email;
                    dbEntry.Phone = client.Phone;
                }
            }
            _context.SaveChanges();
        }

        public Client DeleteClient(int clientId)
        {
            Client dbEntry = _context.Clients.Find(clientId);
            if (dbEntry != null)
            {
                _context.Clients.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
