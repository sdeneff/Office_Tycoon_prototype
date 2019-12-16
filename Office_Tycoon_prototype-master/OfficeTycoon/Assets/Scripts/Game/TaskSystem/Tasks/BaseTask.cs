using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseTask {
	public GameAgent owner;
	public string name = "dummy";
	public int priority = 0;
	public bool interrupting = false;
	public bool interruptable = false;
	public bool unique = false; // Only one of this tasks in the queue
	public bool onlyMyOwnTargets = false; // Can be done only on the TaskTargets assigned to the owner
	public bool onlyUnownedTargets = false; // Can be done only on the TaskTargets without an owner
	public float duration = 10f;
	public float timeLeft = 10f;
	public bool completed = false;
	public bool failed = false;
	public TaskTarget target;

	public virtual void OnAddedToQueue(){
		if(
			interrupting && 			
			owner.awaitingTasks.Contains(this) && 
			owner.currentTask != null &&
			owner.currentTask.interruptable
		){
			owner.currentTask.OnInterrupted();
			owner.StartTask(this);
		}
	}

	public virtual void OnStart(){

	}

	public virtual void OnComplete(){
		completed = true;
	}

	public virtual void OnUpdate(){

	}

	public virtual void OnInterrupted(){

	}

	public virtual void OnFailed(){
		failed = true;
	}
}
