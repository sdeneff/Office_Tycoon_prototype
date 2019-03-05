using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossDesk : MonoBehaviour, IPointerDownHandler {
    public GameObject bossDeskUI;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("double click");
            bossDeskUI.SetActive(true);
        }
    }
}
