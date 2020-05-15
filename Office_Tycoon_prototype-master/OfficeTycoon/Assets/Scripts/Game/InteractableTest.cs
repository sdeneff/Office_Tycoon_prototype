using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : MonoBehaviour, IInteractable
{
	public TaskTarget target;
	public List<cakeslice.Outline> outlines = new List<cakeslice.Outline>();
	public List<cakeslice.Outline> GetOutlines(){
		return outlines;
	}

    public void OnInteraction(GameAgent source){
    	SitDownTask sitDownTask = new SitDownTask();
    	sitDownTask.target = target;
    	source.AddTaskToQueue(sitDownTask);
    }
}
