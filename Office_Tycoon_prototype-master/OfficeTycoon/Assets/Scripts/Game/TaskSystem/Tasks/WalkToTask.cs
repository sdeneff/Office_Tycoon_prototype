using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class WalkToTask : BaseTask
{
	public Vector3 position;

	public WalkToTask() : base(){
		name = "Walk To";
		interrupting = true;
		interruptable = true;
		priority = 120;
	}

	public override void OnAddedToQueue(){
		base.OnAddedToQueue();
		if(owner.awaitingTasks.Contains(this) && owner.currentTask is WalkToTask){
			owner.StartTask(this);
		}
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
		if(owner.agent.pathEndPosition == owner.transform.position){
			OnComplete();
		} else if(owner.agent.pathStatus == NavMeshPathStatus.PathInvalid){
			OnFailed();
		}
	}

	public override void OnFailed(){
		base.OnFailed();
	}
}
