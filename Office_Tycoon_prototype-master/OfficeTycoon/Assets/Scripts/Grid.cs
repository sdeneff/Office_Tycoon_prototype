using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject prefabPlacementObject;
    public GameObject prefabOK;
    public GameObject prefabFail;

    public float gridResolution = 2.0f;

    public LayerMask mask = -1;

    bool[,] usedSpace;

    private GameObject m_placementObject = null;
    private GameObject m_areaObject = null;

    private Bounds placementBounds;

    private Quaternion m_objectRotation;

    private bool _mouseClick = false;
    private Vector3 _lastPosition;

    private void Start()
    {
        if (GetComponent<Terrain>() != null)
        {
            placementBounds = GetComponent<Terrain>().terrainData.bounds;
        }
        else if (GetComponent<Renderer>() != null)
        {
            placementBounds = GetComponent<Renderer>().bounds;
        }

        Vector3 tiles = placementBounds.size / gridResolution;
        usedSpace = new bool[Mathf.CeilToInt(tiles.x), Mathf.CeilToInt(tiles.z)];
        for (int x = 0; x < Mathf.CeilToInt(tiles.x); x++)
        {
            for (int z = 0; z < Mathf.CeilToInt(tiles.z); z++)
            {
                usedSpace[x, z] = false;
            }
        }

        m_objectRotation = Quaternion.identity;
    }
    private void Update()
    {
        Vector3 point;

        if (getTargetLocation(out point))
        {
            Vector3 halfTiles = placementBounds.size / 2.0f;

            int x = (int)Math.Round(Math.Round(point.x - transform.position.x + halfTiles.x - gridResolution / 2.0f) / gridResolution);
            int z = (int)Math.Round(Math.Round(point.z - transform.position.z + halfTiles.z - gridResolution / 2.0f) / gridResolution);

            point.x = (float)(x) * gridResolution - halfTiles.x + transform.position.x + gridResolution / 2.0f;
            point.y += prefabPlacementObject.transform.localScale.y / 2.0f;
            point.z = (float)(z) * gridResolution - halfTiles.z + transform.position.z + gridResolution / 2.0f;

            if (_lastPosition.x != x || _lastPosition.z != z || m_areaObject == null)
            {
                _lastPosition.x = x;
                _lastPosition.z = z;
                if (m_areaObject != null)
                {
                    Destroy(m_areaObject);
                }
                m_areaObject = Instantiate(usedSpace[x, z] == false ? prefabOK : prefabFail, point, m_objectRotation);
                Debug.Log("Object Rotation: " + m_objectRotation.eulerAngles.y);
            }

            if (!m_placementObject)
            {
                m_placementObject = Instantiate(prefabPlacementObject, point, m_objectRotation);
            }
            else
            {
                m_placementObject.transform.position = point;
            }

            if (Input.GetMouseButtonDown(0) && _mouseClick == false)
            {
                _mouseClick = true;
                if (usedSpace[x, z] == false)
                {
                    Debug.Log("Placement position: " + x + ", " + z);
                    usedSpace[x, z] = true;

                    Instantiate(prefabPlacementObject, point, m_objectRotation);
                    m_objectRotation = Quaternion.identity;
                }
            }
            else if (Input.GetMouseButton(1))
            {
                m_objectRotation *= Quaternion.AngleAxis(90,Vector3.up);
                
            }
            else if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                _mouseClick = false;
            }

        }
        else
        {
            if (m_placementObject)
            {
                Destroy(m_placementObject);
                m_placementObject = null;
            }
            if (m_areaObject)
            {
                Destroy(m_areaObject);
                m_areaObject = null;
            }
        }
    }

    private bool getTargetLocation(out Vector3 pPoint)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, mask))
        {
            if (hitInfo.collider == GetComponent<Collider>())
            {
                pPoint = hitInfo.point;
                return true;
            }
        }
        pPoint = Vector3.zero;
        return false;
    }
}

//public class Grid : MonoBehaviour
//{
//[SerializeField]
//private float m_distance = 1f;
//[SerializeField]
//private int m_size = 20;

//public Vector3 GetNearestPosition(Vector3 pPosition)
//{
//    //pPosition -= transform.position;
//    //int xCount = Mathf.RoundToInt(pPosition.x / m_distance);
//    //int yCount = Mathf.RoundToInt(pPosition.y / m_distance);
//    //int zCount = Mathf.RoundToInt(pPosition.z / m_distance);

//    //Vector3 result = new Vector3(
//    //    (float)xCount * m_distance,
//    //    (float)yCount * m_distance,
//    //    (float)zCount * m_distance);

//    //return result += transform.position;
//    return Vector3.zero;
//}

//private void OnDrawGizmos()
//{
//    Gizmos.color = Color.yellow;
//    for (int x = -m_size; x < m_size; x++)
//    {
//        for (int z = -m_size; z < m_size; z++)
//        {
//            Vector3 point = GetNearestPosition(new Vector3(x,0f,z));
//            Gizmos.DrawSphere(point, 0.1f);
//        }
//    }
//}
//[SerializeField]
//private int m_columns = 5;
//[SerializeField]
//private int m_rows = 5;
//[SerializeField]
//private int m_tileSize = 5;
//private bool[,] m_tileData;
//private void Awake()
//{
//    m_tileData = new bool[m_columns, m_rows];
//    ResetFree();
//}
//public void ResetFree()
//{
//    for (int i = 0; i < m_columns * m_rows; i++)
//    {
//        m_tileData[i % m_columns, i / m_columns] = true;
//    }
//}
//public void SetFree(Vector3 pPosition, bool pFree)
//{
//    int column = GetColumnFromPosition(pPosition);
//    int row = GetRowFromPosition(pPosition);
//    if (column < 0 || column >= m_columns || row < 0 || row >= m_rows) return;
//    m_tileData[column, row] = pFree;
//}
//public bool IsFree(Vector3 pPosition)
//{
//    int column = GetColumnFromPosition(pPosition);
//    int row = GetRowFromPosition(pPosition); 
//    if (column < 0 || column >= m_columns || row < 0 || row >= m_rows) return false;

//    return m_tileData[column, row];
//}
//public Vector3 GetNearestPosition(Vector3 pPosition)
//{
//    //pPosition -= transform.position;
//    //int xCount = Mathf.RoundToInt(pPosition.x / m_distance);
//    //int yCount = Mathf.RoundToInt(pPosition.y / m_distance);
//    //int zCount = Mathf.RoundToInt(pPosition.z / m_distance);

//    //Vector3 result = new Vector3(
//    //    (float)xCount * m_distance,
//    //    (float)yCount * m_distance,
//    //    (float)zCount * m_distance);

//    //return result += transform.position;
//    return new Vector3(
//        GetColumnFromPosition(pPosition),
//        0, 
//        GetRowFromPosition(pPosition));

//}
//private void OnDrawGizmos()
//{
//    Gizmos.color = Color.yellow;
//    for (int x = 0; x < m_rows; x++)
//    {
//        for (int z = 0; z < m_columns; z++)
//        {
//            Vector3 point = GetNearestPosition(new Vector3(x, 0f, z));
//            Gizmos.DrawSphere(point, 0.1f);
//        }
//    }
//}
//private int GetColumnFromPosition(Vector3 pPosition)
//{
//    return Mathf.RoundToInt(pPosition.x - transform.position.x)/  m_tileSize;
//}
//private int GetRowFromPosition(Vector3 pPosition)
//{
//    return Mathf.RoundToInt(pPosition.z - transform.position.z) / m_tileSize;
//}
//}
