using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearUI : MonoBehaviour
{

    public float rotateSpeed = 1.0f;
    private static bool m_isRotating = false;
    public void ToggleUIElement(GameObject pTarget)
    {
        if (!m_isRotating)
        {
            StartCoroutine(RotateGear());
            pTarget.SetActive(!pTarget.activeSelf);
        }
    }

    private IEnumerator RotateGear()
    {
        m_isRotating = true;
        transform.eulerAngles = new Vector3(
               transform.eulerAngles.x,
               transform.eulerAngles.y,
               0);
        while (true)
        {
            float currentRotation = Mathf.LerpAngle(transform.eulerAngles.z, 180, Time.deltaTime * rotateSpeed);
                
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                currentRotation);
            if (currentRotation >= 179.5f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        m_isRotating = false;
    }
}
