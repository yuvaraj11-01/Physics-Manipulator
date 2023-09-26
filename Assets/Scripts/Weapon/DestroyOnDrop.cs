using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyOnDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var _dragable = eventData.pointerDrag.GetComponent<PropertyDragable>();
            Destroy(_dragable.GetClone().gameObject);
        }
    }

}
