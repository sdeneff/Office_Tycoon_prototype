using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(cakeslice.Outline))]
[RequireComponent(typeof(PopMenu))]
public class SelectableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiElement;
    private cakeslice.Outline _outline;
    private PopMenu menu;
    public bool hoverOutline = true;

    public string objectName = "defaultObject";
    public string timeAlive = "0 days";

    // Use this for initialization
    void Start()
    {
        menu = GetComponent<PopMenu>();
        _outline = GetComponent<cakeslice.Outline>();
        _outline.enabled = false;
    }
    private void OnMouseEnter()
    {
        if (hoverOutline)
            _outline.enabled = true;
    }
    private void OnMouseExit()
    {
        if (hoverOutline)
            _outline.enabled = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(menu != null)
            {
                menu.ShowMenu();
            }            
        }
    }


}
