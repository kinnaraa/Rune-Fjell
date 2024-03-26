using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject droppedItem = eventData.pointerDrag;
            ItemIcon inventoryItemIcon = droppedItem.GetComponent<ItemIcon>();
            inventoryItemIcon.parentAfterDrag = transform;
        }
    }
        /*
        if (transform.childCount == 0)
        {
            ItemIcon inventoryItem = eventData.pointerDrag.GetComponent<ItemIcon>();
            inventoryItem.parentAfterDrag = transform;
        }
        */
        //throw new System.NotImplementedException();

}
