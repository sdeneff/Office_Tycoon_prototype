using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class DrinkWaterTask : BaseTask
{
	public TaskTarget target;

	bool _reached = false;

	public DrinkWaterTask() : base(){
		name = "Drink Water";
		interrupting = true;
		interruptable = false;
		unique = true;
		timeLeft = 2;
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
		base.OnComplete();
		target.busy = false;
	}

	public override void OnUpdate(){
		base.OnUpdate();
		if(_reached) {
			timeLeft -= Time.deltaTime;
			owner.needs.thirst.currentAmount = 0; //TODO: Tween it?
			if(timeLeft < 0) OnInterrupted();
		} else {
			if(owner.agent.pathEndPosition == owner.transform.position && !target.busy){
				owner.agent.enabled = false;
				_reached = true;
				target.busy = true;
			} else if(owner.agent.pathStatus == NavMeshPathStatus.PathInvalid){
				OnFailed();
			}
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
