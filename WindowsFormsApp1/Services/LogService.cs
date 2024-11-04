using LiteDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Data.Repositories;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public interface ILogService
    {
        void LogError(string message, Exception ex = null);
        void LogInfo(string message);
        void LogWarning(string message);
        List<LogEntry> GetRecentLogs(int count = 100);
        void DeleteAll();
        void Delete(int id);
    }
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void LogError(string message, Exception ex = null)
        {
            var entry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = LogLevel.Error,
                Message = GetFormattedMessage(message, ex),
                StackTrace = ex?.StackTrace
            };

            _logRepository.Add(entry);
        }

        public void LogInfo(string message)
        {
            var entry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = LogLevel.Info,
                Message = message
            };

            _logRepository.Add(entry);
        }

        public void LogWarning(string message)
        {
            var entry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = LogLevel.Warning,
                Message = message
            };

            _logRepository.Add(entry);
        }

        public List<LogEntry> GetRecentLogs(int count = 100)
        {
            return _logRepository.GetRecent(count);
        }
        public void DeleteAll()
        {
            _logRepository.DeleteAll();
        }
        public void Delete(int id)
        {
            _logRepository.Delete(id);
        }
        private string GetFormattedMessage(string message, Exception ex = null)
        {
            if (ex == null)
                return message;

            return $"{message} | Exception: {ex.GetType().Name} | Message: {ex.Message}";
        }
    }
}

