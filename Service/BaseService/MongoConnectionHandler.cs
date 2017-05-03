using System.Configuration;
using MongoDB.Driver;
using Core.Model.BaseModel;

namespace Service.BaseService
{
    public class MongoConnectionHandler<T> where T : DoiTuongMau
    {
        public MongoConnectionHandler()
        {
           // string serverStr = "mongodb://192.168.50.32:27017";
            string databaseName = "SourceMau";
            string serverStr = "mongodb://172.29.14.66:27017";
            //string serverStr = "mongodb://221.132.18.21:27017";
            //string serverStr = ConfigurationSettings.AppSettings["PMKS_DB"]; 
            //string databaseName = "NetMDLocal";
            var mongoClient = new MongoClient(serverStr);
            var database = mongoClient.GetDatabase(databaseName);
            Collection = database.GetCollection<T>(typeof (T).Name);
        }

        public IMongoCollection<T> Collection { get; private set; } 
    }
}