using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    private void OnEnable()
    {
        Battery.OnBatteryCollected += AddItem;
        Battery.OnBatteryUsed += RemoveItem;
    }

    private void OnDisable()
    {
        Battery.OnBatteryCollected -= AddItem;
        Battery.OnBatteryUsed -= RemoveItem;
    }

    public void AddItem(ItemData itemData)
    {
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddQuantity();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveQuantity();

            if(item.quantity == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }

    public InventoryItem GetItemByID(int ID)
    {
        foreach(InventoryItem item in inventory)
        {
            if(item.itemData.ID == ID)
            {
                return item;
            }
        }

        return null;
    }
}
