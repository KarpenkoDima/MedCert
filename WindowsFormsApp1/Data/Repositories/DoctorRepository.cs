using LiteDB;
using System;
using System.Collections.Generic;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Data.Repositories
{
    public class DoctorRepository : BaseRepository<MDoctor>, IDoctorRepository
    {
        public DoctorRepository(
            ILiteDatabase liteDatabase,
            ILogService logService,
            DatabaseOptions dbOptions)  // Изменили тип инъекции
            : base(liteDatabase, "MDoctors", logService, dbOptions)
        {
        }

        public override List<MDoctor> GetAll()
        {
            try
            {
                var collection = _db.GetCollection<MDoctor>(CollectionName);
                return collection.Query()
                               .OrderBy(x => x.LastName)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logService.LogError("Error getting all doctors", ex);
                throw;
            }
        } 
    }
}
