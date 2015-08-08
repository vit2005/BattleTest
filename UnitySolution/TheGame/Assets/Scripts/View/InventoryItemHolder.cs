using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryItemHolder : MonoBehaviour, IDropHandler
{



    public Item.ItemType TypeHolder;

    public GameObject Item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            return null;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!Item)
        {
            if (TypeHolder == global::Item.ItemType.other ||
                TypeHolder == InventoryDraggedItem.itemBeingDragged.GetComponent<InventoryDraggedItem>().item.itrmType)
            {
                InventoryDraggedItem.itemBeingDragged.transform.SetParent(transform);
                InventoryWindowScript.Instance.FillStats();
            }
                
        }

    }
}
