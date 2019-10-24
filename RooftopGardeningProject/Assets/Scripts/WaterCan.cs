using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterCan : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
#if UNITY_ANDROID
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            transform.position = Input.GetTouch(0).position;
        }
#elif UNITY_IOS
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            transform.position = Input.GetTouch(0).position;
        }
#else
        transform.position = Input.mousePosition;
#endif
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        RaycastHit hitInfo;

#if UNITY_ANDROID
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#elif UNITY_IOS
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#else
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif

        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            if (hitInfo.transform.tag == "Plant")
            {
                Debug.Log("Watering Plant");
            }
            transform.localPosition = Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
