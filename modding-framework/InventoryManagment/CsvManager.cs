using CsvHelper;
using CsvHelper.Configuration;
using EmpyrionModdingFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class CsvManager
    {
        private string _databasePath;
        private Action<string> _log;

        public CsvManager(string databasePath, Action<string> logFunc)
        {
            _databasePath = databasePath;
            _log = logFunc;
        }

        internal void SaveStackRecord(string fileName, List<InventoryItem> items)
        {
            var path = GetFilePath(fileName);

            if (File.Exists(path))
                File.Delete(path);

            using (var stream = new StreamWriter(path))
            using (var csv = new CsvWriter(stream, CultureInfo.InvariantCulture))
                csv.WriteRecords(items);
        }

        internal List<InventoryItem> LoadItemStackRecord(string fileName)
        {
            var path = GetFilePath(fileName);
            List<InventoryItem> items = null;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                Delimiter = ","
            };

            using (var stream = new StreamReader(path))
            using (var csv = new CsvReader(stream, config))
            {
                items = csv.GetRecords<InventoryItem>().ToList();
            }

            return items;
        }

        private string GetFilePath(string filename)
        {
            return Path.Combine(_databasePath, $"{filename}");
        }
    }
}
