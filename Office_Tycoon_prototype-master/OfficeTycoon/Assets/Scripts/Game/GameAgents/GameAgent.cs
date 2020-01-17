using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameAgent : MonoBehaviour
{
    public List<BaseTask> awaitingTasks = new List<BaseTask>();
    public BaseTask currentTask;
    public NavMeshAgent agent;
    public PhysiologicalNeeds needs;
    public Animator animator;

    public void Start(){
    }

    public void Update(){
    	if(currentTask != null && currentTask.GetType() == typeof(BaseTask)) currentTask = null; //TODO;

    	if(currentTask == null){
    		if(awaitingTasks.Count == 0){
    			//Laze around
    		} else {
                UpdateQueue();
    			StartTask(awaitingTasks[0]);
    		}
    	} else {
            UpdateQueue();
    		currentTask.OnUpdate();
    		if(currentTask.failed || currentTask.completed){
                if(awaitingTasks.Count > 0) StartTask(awaitingTasks[0]);
                else currentTask = null;
    		}
    	}
    }

    public void UpdateQueue(){
        if(awaitingTasks.Count == 0 || currentTask == null) return;

        awaitingTasks.Sort((x, y) => y.priority - x.priority);
        if(awaitingTasks[0].priority > currentTask.priority || awaitingTasks[0].interrupting) {
            if(currentTask.interruptable){
                currentTask.OnInterrupted();
                StartTask(awaitingTasks[0]);
            }
        }
    }

    public void StartTask(BaseTask task){
    	if(awaitingTasks.Contains(task)) awaitingTasks.Remove(task);
    	currentTask = task;
    	currentTask.OnStart();
    }

    public void AddTaskToQueue(BaseTask task){
        if( task.unique ){
            if(currentTask != null && currentTask.name == task.name){
                currentTask.priority = task.priority;
                return;
            } else if(HasTaskOfType(task.name)) {
                awaitingTasks.Remove(GetTaskOfType(task.name));
            }
        }

    	task.owner = this;
    	awaitingTasks.Add(task);
        task.OnAddedToQueue();
        UpdateQueue();
    }

    public BaseTask GetTaskOfType(string taskName){
        if(currentTask != null && currentTask.name == taskName) return currentTask;
        foreach(BaseTask task in awaitingTasks){
            if(task.name == taskName) return task;
        }
        return null;
    }

    public bool HasTaskOfType(string taskName){
        return GetTaskOfType(taskName) != null;
    }

    private void LateUpdate()
    {
        animator.SetBool("isMoving", (agent.pathEndPosition != transform.position));
    }

}
