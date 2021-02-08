using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InventoryManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        public string EquippedItem { get; private set; }

        private Dictionary<string, int> _items;

        public void Initialize()
        {
            Status = ManagerStatus.Started;
            _items = new Dictionary<string, int>();

            Debug.Log("Inventory manager started...");
        }

        public void DisplayItems()
        {
            foreach(var item in _items)
            {
                Debug.Log($"{item.Key}({item.Value})");
            }
        }

        public void AddItem(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName]++;
            }
            else
            {
                _items.Add(itemName, 1);
            }

            DisplayItems();
        }

        public List<string> GetItemList()
        {
            var items = new List<string>(_items.Keys);

            return items;
        }

        public int GetItemCount(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                return _items[itemName];
            }
            else
            {
                return 0;
            }
        }

        public bool EquipItem(string itemName)
        {
            if (_items.ContainsKey(itemName) && EquippedItem != itemName)
            {
                EquippedItem = itemName;
                Debug.Log($"Equipped item: {itemName}");
                return true;
            }

            EquippedItem = null;
            Debug.Log("Item unequipped");
            return false;
        }

        public bool ConsumeItem(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName]--;
                if (_items[itemName] <= 0)
                {
                    _items.Remove(itemName);
                }

                Debug.Log($"Item consumed: {itemName}");
            }
            else
            {
                Debug.Log($"Cannot consume item: {itemName}");
                return false;
            }

            DisplayItems();
            return true;
        }
    }
}