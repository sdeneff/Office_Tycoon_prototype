using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workstation : BaseTaskGenerator, IInteractable
{
	public TaskTarget target;
	public List<cakeslice.Outline> outlines = new List<cakeslice.Outline>();
	public List<cakeslice.Outline> GetOutlines(){
		return outlines;
	}

    public void OnInteraction(GameAgent source){
    	WorkTask workTask = new WorkTask();
    	workTask.target = target;
    	source.AddTaskToQueue(workTask);
    }

    public override void GenerateTask(){
    	if(!target.owner) return;
        if(target.busy) return;
    	WorkTask workTask = new WorkTask();
    	workTask.target = GetComponent<TaskTarget>();
    	target.owner.AddTaskToQueue(workTask);
    }
}