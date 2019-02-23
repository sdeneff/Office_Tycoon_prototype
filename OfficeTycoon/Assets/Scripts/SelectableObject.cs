using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(cakeslice.Outline))]
public class SelectableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiElement;
    private cakeslice.Outline _outline;
    public bool hoverOutline = true;

    public string name = "defaultObject";
    public string timeAlive = "0 days";

    // Use this for initialization
    void Start()
    {
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
}
