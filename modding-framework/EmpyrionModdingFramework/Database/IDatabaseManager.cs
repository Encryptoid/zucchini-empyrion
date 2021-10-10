using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionModdingFramework.Database
{
    public interface IDatabaseManager
    {
        void SaveRecord<T>(string recordId, T record, bool clearExisting = true);
        void SaveRecords<T>(string recordId, List<T> records, bool clearExisting = true);
        bool LoadRecord<T>(string recordId, out T record);
        bool LoadRecords<T>(string recordId, out List<T> records);
    }
}
