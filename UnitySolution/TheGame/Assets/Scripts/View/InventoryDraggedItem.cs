using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryDraggedItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public static GameObject itemBeingDragged;
    Vector3 StartPosition;
    Transform StartParent;
    Transform TempParent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        StartPosition = transform.position;
        StartParent = transform.parent;
        TempParent = transform.parent.parent.parent;
        transform.SetParent(TempParent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //if(transform.parent != StartParent)
        //{
            transform.position = StartPosition;
        //}
            if (transform.parent == TempParent)
                transform.SetParent(StartParent);
    }
}
