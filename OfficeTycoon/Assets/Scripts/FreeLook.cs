using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLook : MonoBehaviour
{
    public float maxYRot = 80.0f;
    public float sensitivity = 1.0f;
    public bool freeLook = false;

    private float m_rotationY = 0.0f;
    private float m_rotationX = 0.0f;
    private Quaternion m_originalRotation;

    //private float m_moveX = 0.0f;
    //private float m_moveY = 0.0f;
    //private float m_moveZ = 0.0f;
    private float m_speed = 1.0f;
    private Vector3 m_originalPosition;

    private float m_originalOrthographicSize;
    public float m_orthographicSizeSensitivity = 0.1f;

    private GameObject parent;
    private Quaternion m_parentOriginalRotation;
    private void Awake()
    {
        m_originalRotation = transform.localRotation;
        m_originalPosition = transform.position;
        parent = GetComponentInParent<Dummy>().gameObject;
        m_parentOriginalRotation = parent.transform.localRotation;
        m_originalOrthographicSize = GetComponent<Camera>().orthographicSize;
    }
    // Update is called once per frame
    void Update()
    {
        if (!freeLook)
        {
            ResetPosition();
            ResetRotation();
            return;
        }
        else if (freeLook)
        {
            UpdateRotation();
            UpdateMovement();
        }
    }
    private void UpdateMovement()
    {
        //m_moveX = Input.GetAxis("Horizontal");
        //m_moveZ = -Input.GetAxis("Vertical");
        //transform.position += new Vector3(m_moveX, 0, m_moveZ);
        if (Camera.main.orthographic)
        {
            if (Input.GetKey(KeyCode.W))
            {
                parent.transform.position += transform.up * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                parent.transform.position += -transform.up * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                parent.transform.position += -transform.right * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                parent.transform.position += transform.right * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                GetComponent<Camera>().orthographicSize -= m_orthographicSizeSensitivity * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                GetComponent<Camera>().orthographicSize += m_orthographicSizeSensitivity * Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                parent.transform.position += transform.forward * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                parent.transform.position += -transform.forward * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                parent.transform.position += -transform.right * Time.deltaTime * m_speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                parent.transform.position += transform.right * Time.deltaTime * m_speed;
            }
        }
    }
    private void ResetPosition()
    {
        parent.transform.position = m_originalPosition;
        GetComponent<Camera>().orthographicSize = m_originalOrthographicSize;
    }
    private void ResetRotation()
    {
        transform.localRotation = m_originalRotation;
        parent.transform.localRotation = m_parentOriginalRotation;
        m_rotationX = 0.0f;
        m_rotationY = 0.0f;
    }
    private void UpdateRotation()
    {
        m_rotationX += Input.GetAxis("Mouse X") * sensitivity;
        m_rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        m_rotationX = ClampAngle(m_rotationX, -360.0f, 360.0f);
        m_rotationY = ClampAngle(m_rotationY, -maxYRot, maxYRot);

        Quaternion xQuaternion = Quaternion.AngleAxis(m_rotationX, Vector3.up);
        parent.transform.localRotation = m_originalRotation * xQuaternion/* * yQuaternion*/;

        Quaternion yQuaternion = Quaternion.AngleAxis(m_rotationY, Vector3.left);
        transform.localRotation = m_parentOriginalRotation * yQuaternion;

    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360F;
        if (angle > 360.0f)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    public void SetSensivity(float value)
    {
        sensitivity = value;
    }
    public void SetFreeLook(bool value)
    {
        freeLook = value;
    }
    public void SetOrthographicSizeSens(float value)
    {
        m_orthographicSizeSensitivity = value;
    }
    public void SetMoveSpeed(float value)
    {
        m_speed = value;
    }

}
