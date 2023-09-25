using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropertyDragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject cloneObject;
    public PhysicsProperty appliedProperty;

    RectTransform _rectTransform;
    CanvasGroup group;
    Vector2 initPos;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        initPos = _rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        _rectTransform.anchoredPosition = initPos;
    }
    GameObject Clone;
    public void OnBeginDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = false;
        Clone = Instantiate(cloneObject, canvas.transform);
        Clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //_rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Clone.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        group.blocksRaycasts = true;
        Clone.GetComponent<CanvasGroup>().blocksRaycasts = true;
        //Destroy(Clone);
    }

    public RectTransform GetClone() => Clone.GetComponent<RectTransform>();

}
