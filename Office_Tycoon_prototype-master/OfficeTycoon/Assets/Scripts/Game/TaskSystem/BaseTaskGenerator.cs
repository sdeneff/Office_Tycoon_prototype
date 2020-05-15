using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTaskGenerator : MonoBehaviour
{
    public float time = 10;
    public float timeMultiplier = 1f;
    float _time;

    public void Start(){
    	_time = time;
    }

    public void Update(){
    	_time -= Time.deltaTime * timeMultiplier;
    	if(_time < 0f){
    		_time = time - _time;
            GenerateTask();
    	}
    }

    public virtual void GenerateTask(){}
}
