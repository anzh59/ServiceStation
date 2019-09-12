namespace ServiceStation.Domain.Abstract
{
    public interface IDBAccessor
    {
        string ConnectionString { get; }
        bool CheckConnectivity();
        void SetConnectionUserInfo(string username, string password);
    }
}
