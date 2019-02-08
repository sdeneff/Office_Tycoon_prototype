using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossDesk : MonoBehaviour, IPointerDownHandler {
    public GameObject bossDeskUI;
    private void OnMouseDown()
    {
        if (GameManager.Instance.currentHover == gameObject)
        {
        }
    }
    private void Update()
    {
        if (bossDeskUI.activeSelf && (Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.Escape)))
        {
            bossDeskUI.SetActive(false);
            GameManager.Instance.currentSelection = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("double click");
            bossDeskUI.SetActive(true);
        }
    }
}
