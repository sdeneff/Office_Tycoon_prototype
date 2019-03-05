using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float transitionSpeed;
    public Transform targetView;
    private Transform m_originalView;
    public Camera fpCamera;
    private Camera m_originalCamera;
    //private Transform m_currentView;
	// Use this for initialization
	void Start () {
        m_originalCamera = GetComponent<Camera>();
        m_originalView = new GameObject().transform;
        m_originalView.position = transform.position;
        m_originalView.rotation = transform.rotation;
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fpCamera.enabled = true;
            m_originalCamera.enabled = false;
            //m_currentView = m_originalView;
            //StartCoroutine(SmoothTransition(m_originalView, true));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) )
        {
            fpCamera.enabled = false;
            m_originalCamera.enabled = true;
            //m_currentView = targetView;
            //StartCoroutine(SmoothTransition(targetView, false));
        }

    }

    private IEnumerator SmoothTransition(Transform pTarget, bool pOrthographic)
    {
        StopCoroutine("SmoothTransition");
        GetComponent<Camera>().orthographic = pOrthographic;
        while (Vector3.Distance(transform.position, pTarget.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, pTarget.position, transitionSpeed * Time.deltaTime);
            //transform.LookAt(pTarget.forward);
            //transform.rotation = Quaternion.Lerp(transform.rotation, pTarget., transitionSpeed * Time.deltaTime);
            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, pTarget.transform.root.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, pTarget.transform.root.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, pTarget.transform.root.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
            transform.eulerAngles = currentAngle;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
