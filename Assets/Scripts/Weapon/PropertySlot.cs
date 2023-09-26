using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] PhysicsPropertyScriptable propertyScriptable;
    [SerializeField] RectTransform ValueSlot;
    [SerializeField] GameObject RemoveBTN;

    PhysicsProperty appliedProperty;
    GameObject valueVisual;
    RectTransform propertyVisual;

    public int slotIndex;

    private void Awake()
    {
        RemoveBTN.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (appliedProperty != PhysicsProperty.None)
            {
                var _dragable = eventData.pointerDrag.GetComponent<PropertyDragable>();
                Destroy(_dragable.GetClone().gameObject);
                return;
            }

            var dragable = eventData.pointerDrag.GetComponent<PropertyDragable>();
            appliedProperty = dragable.appliedProperty;
            propertyVisual = dragable.GetClone();
            propertyVisual.SetParent(transform);
            propertyVisual.anchoredPosition = Vector2.zero;

            InitializeProperty();

        }
    }

    public bool isEmpty()
    {
        if (appliedProperty == PhysicsProperty.None) return true;

        return false;

    }

    public PhysicsProperty GetBlock()
    {
        return appliedProperty;
    }

    void InitializeProperty()
    {
        var ValueUI = propertyScriptable.GetPropertyData(appliedProperty);
        valueVisual = Instantiate(ValueUI.ValuePrefab.gameObject, ValueSlot);
        valueVisual.GetComponent<PropertyValue>().slot = this;
        valueVisual.GetComponent<PropertyValue>().init();

        RemoveBTN.SetActive(true);
    }

    public void RemoveProperty()
    {
        Destroy(valueVisual);
        Destroy(propertyVisual.gameObject);
        appliedProperty = PhysicsProperty.None;
        RemoveBTN.SetActive(false);
    }

}
