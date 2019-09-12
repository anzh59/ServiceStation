using System.Web.Security;

using ServiceStation.Domain.Abstract;
using ServiceStation.WebUI.Infrastructure.Abstract;

namespace ServiceStation.WebUI.Infrastructure.Concrete
{
    public class DBAuthProvider : IAuthProvider
    {
        private IDBAccessor _dbAccessor;

        public DBAuthProvider(IDBAccessor dbAccessor)
        {
            _dbAccessor = dbAccessor;
        }

        public bool Authenticate(string username, string password)
        {
            _dbAccessor.SetConnectionUserInfo(username, password);

            bool result = _dbAccessor.CheckConnectivity();
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result;
        }
    }
}