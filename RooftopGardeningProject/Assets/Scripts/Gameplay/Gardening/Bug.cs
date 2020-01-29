using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bug : MonoBehaviour, IPointerClickHandler
{
    public GameObject bugExpelVfx;

    public int RemoveTapAmount = 10;
    private int removeTapCount = 0;

    private void Start()
    {
        removeTapCount = RemoveTapAmount;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        removeTapCount = RemoveTapAmount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        removeTapCount--;
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
        if (removeTapCount <= 0)
        {
            transform.GetComponentInParent<Plant>().BugRemoved();
            Instantiate(bugExpelVfx, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
