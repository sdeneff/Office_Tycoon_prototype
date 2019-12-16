using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorBox : MonoBehaviour
{
    public Vector3 extents;
    public Transform graphic;
    public LayerMask positionMask;
    public LayerMask interactablesMask;

    public List<Collider> collidersInside = new List<Collider>();

    public static List<IInteractable> interactables = new List<IInteractable>();
    public static bool canMove = true;
    public static bool canHighLight = true;
    public static Vector3 position;

    void Start(){

    }

    void Update() {
        if(!canMove) return;

        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, positionMask)){
            transform.position = GridManager.Convert(hit.point);
        }

        List<Collider> hitColliders = new List<Collider>(
            Physics.OverlapBox(transform.position, extents/2, Quaternion.identity, interactablesMask)
        );
        foreach(Collider collider in hitColliders){
            if(collidersInside.Contains(collider)) continue;
            collidersInside.Add(collider);
            OnNewInteractable(collider.GetComponent<IInteractable>());
        }
        
        foreach(Collider collider in new List<Collider>(collidersInside)){
            if(hitColliders.Contains(collider)) continue;
            collidersInside.Remove(collider);
            OnInteractableRemoved(collider.GetComponent<IInteractable>());
        }

        position = transform.position;
    }

    void OnNewInteractable(IInteractable interactable){
        interactables.Add(interactable);
        if(canHighLight) 
            foreach(cakeslice.Outline outline in interactable.GetOutlines()) outline.enabled = true;
    }

    void OnInteractableRemoved(IInteractable interactable){
        interactables.Remove(interactable);
        if(canHighLight) 
            foreach(cakeslice.Outline outline in interactable.GetOutlines()) outline.enabled = false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, extents);
    }
}
