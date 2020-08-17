using ToasterApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ToasterApi.Services
{
    public class ToasterService
    {
        private readonly IMongoCollection<Toaster> _toasters;

        public ToasterService(IToastersDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _toasters = database.GetCollection<Toaster>(settings.ToastersCollectionName);
        }

        public List<Toaster> Get() =>
            _toasters.Find(toaster => true).ToList();

        public Toaster Get(string id) =>
            _toasters.Find<Toaster>(toaster => toaster.Id == id).FirstOrDefault();

        public Toaster Create(Toaster toaster)
        {
            _toasters.InsertOne(toaster);
            return toaster;
        }

        public void Update(string id, Toaster toasterIn)
            {
            FilterDefinition<Toaster> filter = Builders<Toaster>.Filter.Eq(toaster => toaster.Id, id);
            UpdateDefinition<Toaster> update = Builders<Toaster>.Update.Set("Time", toasterIn.Time).Set("On", toasterIn.On).Set("Heat", toasterIn.Heat); 
            _toasters.FindOneAndUpdate(filter, update);      //(filter, update);

            //toaster => toaster.Id == id
        }
        public void Remove(Toaster toasterIn) =>
            _toasters.DeleteOne(toaster => toaster.Id == toasterIn.Id);

        public void Remove(string id) =>
            _toasters.DeleteOne(toaster => toaster.Id == id);
    }
}