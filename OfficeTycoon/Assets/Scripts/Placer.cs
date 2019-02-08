//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Placer : MonoBehaviour
//{
//    private Grid m_grid;
//    // Use this for initialization
//    void Awake()
//    {
//        m_grid = FindObjectOfType<Grid>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            RaycastHit hitInfo;
//            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
//            {
//                PlaceCubeNear(hitInfo.point);
//            }
//        }
//        if (Input.GetMouseButtonDown(1))
//        {
//            ClearObjects();
//        }
//    }

//    private void PlaceCubeNear(Vector3 pNearPoint)
//    {
//        Vector3 finalPosition = m_grid.GetNearestPosition(pNearPoint);
//        if (!m_grid.IsFree(finalPosition)) return;

//        GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
//        temp.transform.position = new Vector3(finalPosition.x, finalPosition.y + temp.transform.localScale.y / 2, finalPosition.z);
//        temp.transform.SetParent(transform);
//        m_grid.SetFree(finalPosition, false);
//    }
//    private void ClearObjects()
//    {
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            m_grid.ResetFree();
//            Destroy(transform.GetChild(i).gameObject);
//        }
//    }
//}
