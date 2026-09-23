using System;
using System.Collections.Generic;
using LiteDB;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Data.Repositories
{
    public class TextTemplateRepository : BaseRepository<TextTemplate>, ITextTemplateRepository
    {
        public TextTemplateRepository(
            ILiteDatabase liteDatabase, 
            string collectionName, 
            ILogService logService, 
            DatabaseOptions dbOptions) 
            : base(liteDatabase, collectionName, logService, dbOptions)
        { }

        public override List<TextTemplate> GetAll()
        {
            try
            {
                var collection = _db.GetCollection<TextTemplate>(CollectionName);
                return collection.Query().OrderBy(x => x.Category).ToList();
            }
            catch (Exception ex)
            {
                _logService.LogError("Error getting all texst templates", ex);
                throw;
            }
        }
    }
}