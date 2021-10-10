using CsvHelper;
using CsvHelper.Configuration;
using EmpyrionModdingFramework.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace InventoryManagement
{
    public class CsvManager: IDatabaseManager
    {
        private string _databasePath;

        public CsvManager(string databasePath)
        {
            _databasePath = databasePath;
        }

        public void SaveRecord<T>(string fileName, T record, bool clearExisting = true)
        {
            SaveRecords(fileName, new List<T> { record }, clearExisting);
        }

        public void SaveRecords<T>(string fileName, List<T> records, bool clearExisting = true)
        {
            var path = FormatFilePath(fileName);

            if (clearExisting && File.Exists(path))
                File.Delete(path);

            using (var stream = new StreamWriter(path))
            using (var csv = new CsvWriter(stream, CultureInfo.InvariantCulture))
                csv.WriteRecords(records);
        }

        public bool LoadRecord<T>(string fileName, out T record)
        {
            var success = LoadRecords<T>(fileName, out var records);
            record = success ? records.FirstOrDefault() : default;
            return success;
        }

        public bool LoadRecords<T>(string fileName, out List<T> records)
        {
            var path = FormatFilePath(fileName);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ","
            };

            try
            {
                using (var stream = new StreamReader(path))
                using (var csv = new CsvReader(stream, config))
                    records = csv.GetRecords<T>().ToList();
            }
            catch
            {
                records = null;
                return false;
            }

            return true;
        }

        private string FormatFilePath(string filename)
        {
            return Path.Combine(_databasePath, $"{filename}");
        }
    }
}
