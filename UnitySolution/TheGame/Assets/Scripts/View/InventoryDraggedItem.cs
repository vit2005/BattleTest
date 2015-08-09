using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryDraggedItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public static GameObject itemBeingDragged;
	public static Item ItemInfo;
    Vector3 StartPosition;
    Transform StartParent;
    Transform TempParent;

	// Use this for initialization
	void Start () {
		transform.GetComponent<Button>().onClick.AddListener(Info);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Info()
	{
		ItemInfo = this.item;
		ItemInfoDialog.Instance.Initiate ();
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
