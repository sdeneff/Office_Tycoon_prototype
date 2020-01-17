using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameAgent gameAgent;

   	void Update() {
        if (Input.GetMouseButtonDown(0)) {
        	if(SelectorBox.interactables.Count > 0){
        		foreach(IInteractable interactable in SelectorBox.interactables){
        			interactable.OnInteraction(gameAgent);
        		}
    		} else {
	        	WalkToTask walkToTask = new WalkToTask();
	        	walkToTask.position = SelectorBox.position;
	        	gameAgent.AddTaskToQueue(walkToTask);
    		}
        }
        if (Input.GetMouseButtonDown(1)){
        }
    }
}
