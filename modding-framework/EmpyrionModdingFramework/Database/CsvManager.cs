using CsvHelper;
using CsvHelper.Configuration;
using EmpyrionModdingFramework.Database;
using EmpyrionModdingFramework.Teleport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace InventoryManagement
{
    public class CsvManager: IDatabaseManager
    {
        public string _databasePath { get; }

        public CsvManager(string databasePath)
        {
            _databasePath = databasePath;
        }

        public List<T> LoadRecords<T>(string fileName)
        {
            return LoadCsvRows<T>(fileName);
        }

        private T LoadSingularCsvRow<T>(string fileName)
        {
            var rows = LoadCsvRows<T>(fileName);
            if (rows.Count == 0)
            {
                return default(T);
            }

            return rows.FirstOrDefault();
        }

        private List<T> LoadCsvRows<T>(string fileName)
        {
            var path = FormatFilePath(fileName);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ","
            };

            var rows = new List<T>();

            try
            {
                using (var stream = new StreamReader(path))
                using (var csv = new CsvReader(stream, config))
                    rows = csv.GetRecords<T>().ToList();
            }
            catch {
                return null;
            }

            return rows;
        }

        public void SaveRecord<T>(string fileName, T record, bool clearExisting)
        {
            var path = FormatFilePath(fileName);

            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.NewLine = "\r\n";

            using (var stream = new StreamWriter(path))
            using (var csv = new CsvWriter(stream, config))
            {
                csv.WriteHeader(typeof(T));
                csv.NextRecord();
                csv.WriteRecord(record);
            }
        }

        private string FormatFilePath(string filename)
        {
            return Path.Combine(_databasePath, filename);
        }

        public void SaveRecords<T>(string fileName, List<T> records, bool clearExisting)
        {
            var path = FormatFilePath(fileName);
            if (clearExisting && File.Exists(path))
                File.Delete(path);

            foreach (T record in records)
                SaveRecord<T>(fileName, record, false);
        }
    }
}
