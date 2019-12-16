using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SitDownTask : BaseTask
{
	public Vector3 position;

	bool _reached = false;

	public SitDownTask() : base(){
		name = "Sit Down";
		interrupting = true;
		interruptable = true;
	}

	public override void OnAddedToQueue(){
		base.OnAddedToQueue();
	}

	public override void OnStart(){
		base.OnStart();
		owner.agent.enabled = true;
		owner.agent.destination = position;
	}

	public override void OnComplete(){
		base.OnComplete();
	}

	public override void OnUpdate(){
		base.OnUpdate();
		if(_reached) return;
		if(owner.agent.pathEndPosition == owner.transform.position){
			owner.agent.enabled = false;
			owner.transform.position = position;
			_reached = true;
		} else if(owner.agent.pathStatus == NavMeshPathStatus.PathInvalid){
			OnFailed();
		}
	}

	public override void OnInterrupted(){
		_reached = false;
		OnComplete();
	}

	public override void OnFailed(){
		base.OnFailed();
	}
}
