using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.Services
{
    public class Repository
    {
        private readonly LiteDatabase _database;

        public Repository(LiteDatabase database)
        {
            _database = database;
        }

        public void Save<T>(T item)
        {
            GetCollection<T>().Upsert(item);
        }

        public T Get<T>(Guid id)
        {
            return GetCollection<T>().FindById(id);
        }

        public void Delete<T>(Guid id)
        {
            GetCollection<T>().Delete(id);
        }

        public IEnumerable<T> FindAll<T>()
        {
            return GetCollection<T>().FindAll();
        }

        private ILiteCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>();
        }
    }
}
