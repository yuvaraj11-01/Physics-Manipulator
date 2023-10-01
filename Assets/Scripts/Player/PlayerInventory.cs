using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public static class PlayerInventory
{
    static List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public static UnityEvent OnItemAdd = new UnityEvent();
    public static UnityEvent OnItemConsume = new UnityEvent();

    public static void Init()
    {
        OnItemAdd.AddListener(OnItemAdded);
        OnItemConsume.AddListener(OnItemConsumed);
    }

    static void OnItemAdded()
    {

    }

    static void OnItemConsumed()
    {

    }

    public static void AddItem(InventoryItem item)
    {

        var existingItem = GetItem(item);
        if(existingItem != null)
        {
            existingItem.count++;
            OnItemAdd?.Invoke();

            return;
        }
        inventorySlots.Add(new InventorySlot() { item = InventoryItem.KEY, count = 1 });
        OnItemAdd?.Invoke();

    }


    public static void ConsumeItem(InventoryItem item)
    {
        var existingItem = GetItem(item);
        if (existingItem != null)
        {
            existingItem.count--;
            OnItemConsume?.Invoke();
            if (existingItem.count <= 0) inventorySlots.Remove(existingItem);
            return;
        }
    }

    public static InventorySlot GetItem(InventoryItem item)
    {
        var _item = inventorySlots.Where(e => e.item == item).ToList();
        if (_item.Count > 0) 
            return _item[0];
        return null;
    }

}

public enum InventoryItem
{
    KEY
}

[System.Serializable]
public class InventorySlot
{
    public InventoryItem item;
    public int count;
}