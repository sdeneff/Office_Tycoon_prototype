using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class WorkTask : BaseTask
{
	public Vector3 position;

	bool _reached = false;

	public WorkTask() : base(){
		name = "Work";
		interrupting = false;
		interruptable = true;
		resumeable = true;
		unique = true;
		onlyMyOwnTargets = true;
		duration = 10;
		timeLeft = duration;
	}

	public override void OnAddedToQueue(){
		base.OnAddedToQueue();
	}

	public override void OnStart(){
		base.OnStart();
		owner.agent.enabled = true;
		owner.agent.destination = target.transform.position;
	}

	public override void OnComplete(){
		//base.OnComplete();
		Debug.Log("Did a work cycle");
		timeLeft = duration;
	}

	public override void OnUpdate(){
		base.OnUpdate();
		if(_reached) {
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0) OnComplete();
		} else if(owner.agent.pathEndPosition == owner.transform.position){
			owner.agent.enabled = false;
			owner.transform.position = target.transform.position;
			_reached = true;
			target.busy = true;
		} else if(owner.agent.pathStatus == NavMeshPathStatus.PathInvalid){
			OnFailed();
		}
	}

	public override void OnInterrupted(){
		base.OnInterrupted();
		_reached = false;
		target.busy = false;		
	}

	public override void OnFailed(){
		base.OnFailed();
	}
}
