using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {
    private NavMeshAgent m_agent;
    //private Vector3 m_mousePos;
    private RaycastHit m_hit;
    private Animator m_animator;
    
    // Use this for initialization
    void Awake () {
        m_agent = GetComponent<NavMeshAgent>();
        //m_mousePos = Vector3.zero;
        m_animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out m_hit))
        {
            Debug.Log("clicked on: " + m_hit.point);
            Debug.Log("agent destination: " + m_agent.destination);
            m_agent.SetDestination(m_hit.point);
            Debug.Log("agent destination: " + m_agent.destination);
        }
    }
    private void LateUpdate()
    {
        m_animator.SetBool("isMoving", (m_agent.pathEndPosition != transform.position));
    }
}
