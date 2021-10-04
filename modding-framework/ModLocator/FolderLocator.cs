using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLocator
{
    public class FolderLocator
    {
        private Action<string> _log;
        private const string _fileName = "modfolder.path";

        public FolderLocator(Action<string> logFunc)
        {
            _log = logFunc;
        }

        public string GetDatabaseFolder(string modName, string databaseFolder = "Database")
        {
            var modFolder = GetModFolder(modName);
            return modFolder == null ? null : Path.Combine(modFolder, modName, databaseFolder);
        }

        private string GetModFolder(string modName)
        {
            _log("ModdingFramework is running from directory: " + Directory.GetCurrentDirectory());
            var modFolderFile = Path.Combine(Directory.GetCurrentDirectory(), _fileName);

            if (!File.Exists(modFolderFile)) //Check for tetherporter.dbpath
            {
                _log($"{modName} cannot locate it's database path. Please place a {_fileName} file in the folder you are running the server from: {Directory.GetCurrentDirectory()}");
                return null;
            }

            var modFolderPath = Path.GetFullPath(File.ReadAllText(modFolderFile).Trim()); //Trim spaces and parse relative paths

            if (!Directory.Exists(modFolderPath)) //Check for ../Content/Mods directory
            {
                _log($"{modName} cannot location it's database path, the full location it checked was: {modFolderPath}");
                return null;
            }

            _log("{modName} will use the database path: " + modFolderPath);
            return modFolderPath;
        }
    }
}
