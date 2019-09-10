using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Domain.Abstract
{
    public interface IDBAccessor
    {
        string ConnectionString { get; }
        bool CheckConnectivity();
        void SetConnectionUserInfo(string username, string password);
    }
}
