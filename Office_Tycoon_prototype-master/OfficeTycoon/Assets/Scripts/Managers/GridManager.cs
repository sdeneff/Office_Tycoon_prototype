using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	public static GridManager instance;
    public float gridSize = 1f;

    public void Awake(){
    	instance = this;
    }

    public static Vector3 Convert(Vector3 globalPos){
    	return new Vector3(
    		Mathf.Round(globalPos.x/instance.gridSize)*instance.gridSize, 
    		Mathf.Round(globalPos.y/instance.gridSize)*instance.gridSize,
    		Mathf.Round(globalPos.z/instance.gridSize)*instance.gridSize
    	);
    }
}
