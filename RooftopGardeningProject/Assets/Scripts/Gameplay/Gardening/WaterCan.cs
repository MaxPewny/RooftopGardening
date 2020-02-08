using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterCan : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject MovingCanvas;
    private Transform originalParent;
    private RectTransform rectTransform;
    private float timeOfTravel = 5; //time after object reach a target place 
    private float currentTime = 0;
    private float normalizedValue;

    public LayerMask RaycastLayerMask = 1;

    void Start()
    {
        originalParent = transform.parent;
        rectTransform = gameObject.GetComponent<RectTransform>();
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
            WaterPlant(hitInfo, eventData);
            //transform.SetParent(MovingCanvas.transform, true);
        }
        else 
        {
            SnapWaterCan();
        }

            
    }

    public void WaterPlant(RaycastHit HitInfo, PointerEventData EventData) 
    {
        Debug.Log(HitInfo.transform.name);
        if (HitInfo.transform.tag == "Plant")
        {
            //StartCoroutine(LerpObject(EventData));
            Debug.Log("Watering Plant");
            HitInfo.transform.gameObject.GetComponent<Plant>().WaterPlant();
            GetComponentInChildren<Animator>().Play("WaterAnim");
        }
        else
        {
            SnapWaterCan();
        }
    }

    public void SnapWaterCan() 
    {
        transform.SetParent(originalParent);
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }

    IEnumerator LerpObject(PointerEventData PointerEventData)
    {

        while (Time.deltaTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            rectTransform.position = Vector2.Lerp(PointerEventData.position, PointerEventData.position + new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2), normalizedValue);
            yield return null;
        }
        transform.SetParent(originalParent);
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }
}
