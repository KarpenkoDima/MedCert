using LiteDB;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Data.Repositories
{
    // LogRepository.cs
    public class LogRepository : ILogRepository  // Не наследуется от BaseRepository
    {
        private readonly string _connectionString;
        private const string COLLECTION_NAME = "log";

        public LogRepository(DatabaseOptions dbOptions)
        {
            _connectionString = dbOptions.ConnectionString;
        }

        public void Add(LogEntry entry)
        {
            try
            {
                using (var db = new LiteDatabase(_connectionString))
                {
                    var collection = db.GetCollection<LogEntry>(COLLECTION_NAME);
                    collection.Insert(entry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"\"Произошла ошибка при попытке распечатать сертификат. \n\rВозможно открыт файл с сертификатом\nЛучше перезапустите программу.",
                    $"Failed to write logs: {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DeleteAll()
        {
            try
            {
                using (var db = new LiteDatabase(_connectionString))
                {
                    var collection = db.GetCollection<LogEntry>(COLLECTION_NAME);
                    collection.DeleteAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"\"Произошла ошибка при попытке распечатать сертификат. \n\rВозможно открыт файл с сертификатом\nЛучше перезапустите программу.",
                     $"Failed to delete all logs: {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<LogEntry> GetAll()
        {
            try
            {
                using (var db = new LiteDatabase(_connectionString))
                {
                    var collection = db.GetCollection<LogEntry>(COLLECTION_NAME);
                    return collection.Query()
                                   .OrderByDescending(x => x.Timestamp)
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"\"Произошла ошибка при попытке распечатать сертификат. \n\rВозможно открыт файл с сертификатом\nЛучше перезапустите программу.",
                    $"Failed to read log: {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                return new List<LogEntry>();
            }
        }

        public List<LogEntry> GetRecent(int count)
        {
            try
            {
                using (var db = new LiteDatabase(_connectionString))
                {
                    var collection = db.GetCollection<LogEntry>(COLLECTION_NAME);
                    return collection.Query()
                                   .OrderByDescending(x => x.Timestamp)
                                   .Limit(count)
                                   .ToList();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"\"Произошла ошибка при попытке распечатать сертификат. \n\rВозможно открыт файл с сертификатом\nЛучше перезапустите программу.",
                    $"Failed to read logs: {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return new List<LogEntry>();
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var db = new LiteDatabase(_connectionString))
                {
                    var collection = db.GetCollection<LogEntry>(COLLECTION_NAME);
                    collection.Delete(id);
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show($"\"Произошла ошибка при попытке распечатать сертификат. \n\rВозможно открыт файл с сертификатом\nЛучше перезапустите программу.",
                    $"Failed to DELETE logs: {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
