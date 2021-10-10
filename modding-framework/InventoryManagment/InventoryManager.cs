using Eleon.Modding;
using EmpyrionModdingFramework;
using EmpyrionModdingFramework.Database;
using InventoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class InventoryManager
    {
        private IDatabaseManager _db;
        public InventoryManager(IDatabaseManager databaseManager)
        {
            _db = databaseManager;
        }

        public Inventory GetEmptyInventory(int playerId)
        {
            return new Inventory() { playerId = playerId, toolbelt = new ItemStack[] { }, bag = new ItemStack[] { } };
        }

        public void SaveBag(string steamId, ItemStack[] itemStack) => SaveInventoryComponent(steamId, itemStack, true);
        public void SaveBar(string steamId, ItemStack[] itemStack) => SaveInventoryComponent(steamId, itemStack, false);
        private void SaveInventoryComponent(string steamId, ItemStack[] itemStack, bool isBag = false)
        {
            _db.SaveRecords(GetFileNameExtension(steamId, isBag), itemStack.ToInventoryItems());
        }

        public bool LoadBag(string steamId, out ItemStack[] inventoryRecords) => LoadInventoryComponent(steamId, true, out inventoryRecords);
        public bool LoadBar(string steamId, out ItemStack[] inventoryRecords) => LoadInventoryComponent(steamId, false, out inventoryRecords);
        private bool LoadInventoryComponent(string steamId, bool isBag, out ItemStack[] records)
        {
            var success = _db.LoadRecords<InventoryItem>(GetFileNameExtension(steamId, isBag), out var dbRecord);
            records = success ? dbRecord.Select(e => e.ToItemStack()).ToArray() : null;
            return success;
        }

        private string GetFileNameExtension(string fileName, bool isBag)
        {
            var extension = isBag ? "bag" : "bar";
            return $"{fileName}.{extension}";
        }
    }
}
