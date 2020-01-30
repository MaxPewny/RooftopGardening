using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weed : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public List<Sprite> WeedSprites;

    [SerializeField]
    private float swipeDistanceY = 1, swipeOffsetX = 1;

    private Vector3 originalPosition;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = WeedSprites[Random.Range(0, WeedSprites.Count)];

        originalPosition = transform.position;
        //gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, Camera.main.ScreenToWorldPoint(eventData.position).y, transform.position.z);
        
        if (transform.position.y > originalPosition.y + swipeDistanceY)
        {
            transform.position = originalPosition;

            //transform.GetComponentInParent<Plant>().WeedRemoved();
            gameObject.SetActive(false);
            GameplayController.Instance.GardenDatas[GetComponentInParent<Garden>().GardenNumber].WeedCounter--;
        }
        else if(transform.position.x > originalPosition.x + swipeOffsetX || transform.position.x < originalPosition.x - swipeOffsetX) 
        {
            eventData.Reset();
            transform.position = originalPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPosition;
    }
}
