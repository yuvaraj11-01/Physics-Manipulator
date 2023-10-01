using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var key = PlayerInventory.GetItem(InventoryItem.KEY);
            if (key == null)
            {
                Debug.Log("No Key Found");
                return;
            }

            PlayerInventory.ConsumeItem(InventoryItem.KEY);
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        var colls = GetComponents<Collider2D>();
        foreach (var coll in colls)
        {
            coll.enabled = false;
        }
        gameObject.SetActive(false);
    }


}
