using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : MonoBehaviour, IInteractable
{
	public List<cakeslice.Outline> outlines = new List<cakeslice.Outline>();
	public List<cakeslice.Outline> GetOutlines(){
		return outlines;
	}

    public void OnInteraction(GameAgent source){
    	DrinkWaterTask drinkWaterTask = new DrinkWaterTask();
    	drinkWaterTask.target = GetComponent<TaskTarget>();
    	source.AddTaskToQueue(drinkWaterTask);
    }
}
