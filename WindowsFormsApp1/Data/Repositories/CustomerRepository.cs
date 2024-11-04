using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(
         ILogService logService,
         DatabaseOptions dbOptions)  // Изменили тип инъекции
         : base("customers", logService, dbOptions)
        {
        }
        public override List<Customer> GetAll()
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionString))
                {
                    var collection = db.GetCollection<Customer>(CollectionName);
                    return collection.Query()
                                   .OrderBy(x => x.FIO)
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                _logService.LogError("Error getting all customers", ex);
                throw;
            }
        }

        public List<Customer> Search(string searchText)
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionString))
                {
                    var collection = db.GetCollection<Customer>(CollectionName);
                    return collection.Find(Query.StartsWith("FIO", searchText))
                                   .OrderBy(x => x.FIO)
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error searching customers with text: {searchText}", ex);
                throw;
            }
        }
    }
}
