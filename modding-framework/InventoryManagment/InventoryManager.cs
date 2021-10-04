using Eleon.Modding;
using EmpyrionModdingFramework;
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
        private CsvManager _db;
        public InventoryManager(CsvManager fileManager)
        {
            _db = fileManager;
        }

        public Inventory GetEmptyInventory(int playerId)
        {
            return new Inventory() { playerId = playerId, toolbelt = new ItemStack[] { }, bag = new ItemStack[] { } };
        }

        public void SaveBag(string steamId, ItemStack[] itemStack) => SaveInventoryComponent(steamId, itemStack, true);
        public void SaveBar(string steamId, ItemStack[] itemStack) => SaveInventoryComponent(steamId, itemStack, false);
        private void SaveInventoryComponent(string steamId, ItemStack[] itemStack, bool isBag = false)
        {
            _db.SaveStackRecord(GetFileNameExtension(steamId, isBag), itemStack.ToInvetoryItems());
        }

        public ItemStack[] LoadBag(string steamId) => LoadInventoryComponent(steamId, true);
        public ItemStack[] LoadBar(string steamId) => LoadInventoryComponent(steamId, false);
        private ItemStack[] LoadInventoryComponent(string steamId, bool isBag)
        {
            return _db.LoadItemStackRecord(GetFileNameExtension(steamId, isBag)).Select(e => e.ToItemStack()).ToArray();
        }

        private string GetFileNameExtension(string fileName, bool isBag)
        {
            var extension = isBag ? "bag" : "bar";
            return $"{fileName}.{extension}";
        }
    }
}
