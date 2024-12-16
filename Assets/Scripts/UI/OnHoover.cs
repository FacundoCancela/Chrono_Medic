using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject attack;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (attack != null)
        {
            attack.SetActive(true); 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (attack != null)
        {
            attack.SetActive(false); 
        }
    }
}
