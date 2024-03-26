using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.UIElements;

public class ItemIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //[Header("UI")]
    public Image draggableImage;

    [HideInInspector] public Transform parentAfterDrag;

    public Transform tempParentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        //transform.SetParent(transform.root);
        transform.SetParent(tempParentAfterDrag);
        transform.SetAsLastSibling();
        draggableImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        //Debug.Log(Collider2D.OverlapPoint(Input.mousePosition));
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Finished Drag");
        //transform.position = parentAfterDrag.position;
        transform.SetParent(parentAfterDrag);
        draggableImage.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var other = eventData.pointerDrag.GetComponent<ItemIcon>();
        other.transform.SetParent(transform.parent);
    }

}
