using System.Collections.Generic;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data.Repositories
{
    public interface ILogRepository
    {
        void Add(LogEntry entry);
        List<LogEntry> GetAll();
        List<LogEntry> GetRecent(int count);
        void DeleteAll();
        void Delete(int id);
    }
}
