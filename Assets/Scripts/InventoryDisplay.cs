using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text keyCount;

    private void Awake()
    {
        PlayerInventory.OnItemAdd.AddListener(UpdateText);

        PlayerInventory.OnItemConsume.AddListener(UpdateText);

    }

    void UpdateText()
    {
        var Item = PlayerInventory.GetItem(InventoryItem.KEY);
        if (Item != null)
        {
            keyCount.text = Item.count.ToString();
        }
        else keyCount.text = "0";
    }

}
