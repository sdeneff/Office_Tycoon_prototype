using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseTask {
	public GameAgent owner;
	public string name = "dummy";
	public int priority = 0;
	public bool interrupting = false; // Always tries to interrupt current task, even if lower priority
	public bool interruptable = false; // Can be interrupted?
	public bool resumeable = false; // If interrupted, add it back to the queue
	public bool unique = false; // Removes existing tasks of this type in the queue
	public bool onlyMyOwnTargets = false; // Can be done only on the TaskTargets assigned to the owner
	public bool onlyUnownedTargets = false; // Can be done only on the TaskTargets without an owner
	public float duration = 10f;
	public float timeLeft = 10f;
	public bool completed = false;
	public bool failed = false;
	public TaskTarget target;

	public virtual void OnAddedToQueue(){
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
