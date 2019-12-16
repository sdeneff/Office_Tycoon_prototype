using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : MonoBehaviour, IInteractable
{
	public List<cakeslice.Outline> outlines = new List<cakeslice.Outline>();
	public List<cakeslice.Outline> GetOutlines(){
		return outlines;
	}

    public void OnInteraction(GameAgent source){
    	SitDownTask sitDownTask = new SitDownTask();
    	sitDownTask.position = transform.position;
    	source.AddTaskToQueue(sitDownTask);
    }
}
