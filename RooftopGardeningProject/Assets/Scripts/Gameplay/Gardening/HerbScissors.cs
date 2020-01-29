using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HerbScissors : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject MovingCanvas;
    private Transform originalParent;

    public LayerMask RaycastLayerMask = 1;

    void Start()
    {
        originalParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(MovingCanvas.transform, true);
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out hitInfo, 1000, RaycastLayerMask))
        {
            Debug.Log(hitInfo.transform.name);
            if (hitInfo.transform.tag == "Plant")
            {
                hitInfo.transform.gameObject.GetComponent<Plant>().CutHerb();
            }
        }
        transform.SetParent(originalParent);
        transform.gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        transform.gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    }
}
