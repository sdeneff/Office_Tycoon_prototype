using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameAgent : MonoBehaviour
{
    public List<BaseTask> awaitingTasks = new List<BaseTask>();
    public BaseTask currentTask;
    public NavMeshAgent agent;
    public Animator animator;

    public void Start(){
    }

    public void Update(){
    	if(currentTask != null && currentTask.GetType() == typeof(BaseTask)) currentTask = null; //TODO;

    	if(currentTask == null){
    		if(awaitingTasks.Count == 0){
    			//Laze around
    		} else {
    			StartTask(awaitingTasks[0]);
    		}
    	} else {
    		currentTask.OnUpdate();
    		if(currentTask.failed || currentTask.completed){
    			currentTask = null;
    		}
    	}
    }

    public void StartTask(BaseTask task){
    	if(awaitingTasks.Contains(task)) awaitingTasks.Remove(task);
    	currentTask = task;
    	currentTask.OnStart();
    }

    public void AddTaskToQueue(BaseTask task){
        if(task.unique && HasTaskOfType(task.name)) return;
    	task.owner = this;
    	awaitingTasks.Add(task);
    	task.OnAddedToQueue();
    	//TODO: priority
    }

    public bool HasTaskOfType(string taskName){
        if(currentTask != null && currentTask.name == taskName) return true;
        foreach(BaseTask task in awaitingTasks){
            if(task.name == taskName) return true;
        }
        return false;
    }

    private void LateUpdate()
    {
        animator.SetBool("isMoving", (agent.pathEndPosition != transform.position));
    }

}
