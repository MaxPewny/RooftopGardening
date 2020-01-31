using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bug : MonoBehaviour, IPointerClickHandler
{
    public Sprite BugSprite;
    public Sprite SnailSprite;

    public GameObject bugExpelVfx;
    public GameObject snailExpelVfx;

    private GameObject vfx;

    public int RemoveTapAmount = 10;
    private int removeTapCount = 0;

    private void Start()
    {
        removeTapCount = RemoveTapAmount;
        gameObject.SetActive(false);

        if (Random.Range(0,2) > 0)
        {
            GetComponent<SpriteRenderer>().sprite = BugSprite;
            vfx = bugExpelVfx;
        }
        else 
        {
            GetComponent<SpriteRenderer>().sprite = SnailSprite;
            vfx = snailExpelVfx;
        }
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
            Instantiate(vfx, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
