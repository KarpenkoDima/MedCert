using LiteDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Data.Repositories
{

    public abstract class BaseRepository<T> where T : class
    {
        protected readonly string ConnectionString;
        protected readonly ILogService _logService;
        protected readonly string CollectionName;
        protected readonly DatabaseOptions _dbOptions;

        protected BaseRepository(
         string collectionName,
         ILogService logService,
         DatabaseOptions dbOptions)  // Изменили тип с IOptions<DatabaseOptions>
        {
            CollectionName = collectionName;
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _dbOptions = dbOptions ?? throw new ArgumentNullException(nameof(dbOptions));
            ConnectionString = _dbOptions.ConnectionString;
        }

        public virtual List<T> GetAll()
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionString))
                {
                    var collection = db.GetCollection<T>(CollectionName);

                    if (_dbOptions.EnableLogging)
                    {
                        _logService.LogInfo($"Getting all records from {CollectionName}");
                    }

                    return collection.FindAll().ToList();
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error getting all records from {CollectionName}", ex);
                throw;
            }
        }

        public virtual void Add(T entity)
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionString))
                {
                    var collection = db.GetCollection<T>(CollectionName);
                    collection.Insert(entity);
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error adding record to {CollectionName}", ex);
                throw;
            }
        }

        public virtual void Delete(int id)
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionString))
                {
                    var collection = db.GetCollection<T>(CollectionName);
                    collection.Delete(id);
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error deleting record from {CollectionName}", ex);
                throw;
            }
        }
        public virtual void Update(T item)
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionString))
                {
                    var collection = db.GetCollection<T>(CollectionName);
                    collection.Update(item);
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error deleting record from {CollectionName}", ex);
                throw;
            }
        }
    }
}
