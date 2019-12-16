using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TaskTargets are devices which generate tasks
// GameAgents complete tasks

public class TaskManager : MonoBehaviour
{
	public static Dictionary<string, List<TaskTarget>> targets = new Dictionary<string, List<TaskTarget>>();

	public static void AddTarget(TaskTarget target){
		if(targets.ContainsKey(target.taskName)){
			targets[target.taskName].Add(target);
		} else {
			targets[target.taskName] = new List<TaskTarget>();
			targets[target.taskName].Add(target);
		}
	}

	public static void RemoveTarget(TaskTarget target){
		targets[target.taskName].Remove(target);
	}

	public static List<TaskTarget> GetTargets(string taskName){
		if(!targets.ContainsKey(taskName)) return null;
		List<TaskTarget> availableTargets = new List<TaskTarget>();
		foreach(TaskTarget target in targets[taskName]){
			if(target.busy) continue;
			availableTargets.Add(target);
		}
		return availableTargets;
	}

	public static TaskTarget GetTarget(string taskName){
		if(targets.ContainsKey(taskName)) return GetTargets(taskName)[Random.Range(0, targets[taskName].Count)];
		else return null;
	}

	public static TaskTarget GetTargetWithOwner(string taskName, GameAgent owner){
		if(targets.ContainsKey(taskName)) {
			List<TaskTarget> availableTargets = new List<TaskTarget>();
			foreach(TaskTarget target in GetTargets(taskName)){
				if(target.owner != owner) continue;
				availableTargets.Add(target);
			}
			if(availableTargets.Count == 0) return null;
			else return availableTargets[Random.Range(0, availableTargets.Count)];
		} else return null;
	}

	public static TaskTarget GetTargetWithoutOwner(string taskName){
		return GetTargetWithOwner(taskName, null);
	}

}
