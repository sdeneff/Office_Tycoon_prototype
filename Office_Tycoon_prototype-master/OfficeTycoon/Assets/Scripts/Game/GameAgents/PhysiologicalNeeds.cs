using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysiologicalNeeds : MonoBehaviour
{
    public GameAgent gameAgent;

    [System.Serializable]
	public abstract class Need {
		public PhysiologicalNeeds source;
		public float currentAmount = 0;
		public float maxAmount = 100;
		public float gainPerSecond = 1;

		public AnimationCurve taskChanceCurve;

		public virtual void Tick(float delta){
			currentAmount = Mathf.Min(currentAmount + gainPerSecond * delta, maxAmount);
			if(taskChanceCurve.Evaluate(currentAmount/maxAmount) > Random.value) TryFulfill();
		}

		public abstract void TryFulfill();
	}
	
    [System.Serializable]
	public class Thirst : Need {
		public override void TryFulfill(){
			DrinkWaterTask drinkWaterTask = new DrinkWaterTask();
            drinkWaterTask.target = TaskManager.GetTarget("Drink Water");//GetComponent<TaskTarget>();
            source.gameAgent.AddTaskToQueue(drinkWaterTask);
		}
	}

	public Thirst thirst = new Thirst();
	List<Need> _needs = new List<Need>();
	float _counter;

	void Start(){
		thirst.source = this;
		_needs.Add(thirst);
	}

	void Update(){
		_counter += Time.deltaTime;
		if(_counter > 1){
			foreach(Need need in _needs) need.Tick(1);
			_counter -= 1;
		}
	}
}
