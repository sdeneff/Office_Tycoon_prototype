using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTarget : MonoBehaviour
{
    public GameAgent owner;
    public bool busy = false;
    public string taskName = "dummy";

    public void Awake(){
    	TaskManager.AddTarget(this);
    }
}
