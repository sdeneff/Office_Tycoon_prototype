using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMenu : MonoBehaviour
{
    private GameObject[] stars;
    private GameObject[] labels;
    private GameObject canvas;

    private void Awake()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 camDirection = Vector3.Normalize(gameObject.transform.position - camPos);
        canvas = Instantiate(Resources.Load("Objects_UI", typeof(GameObject)) as GameObject, Camera.main.ScreenToWorldPoint(gameObject.transform.position), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        stars = new GameObject[3];
        foreach (Transform star in canvas.transform)
        {
            if (star.gameObject.name.Contains("star"))
            {
                if (stars[0] == null)
                {
                    stars[0] = star.gameObject;
                }
                else if (stars[1] == null)
                {
                    stars[1] = star.gameObject;
                }
                else
                {
                    stars[2] = star.gameObject;
                }
            }
            else if (star.gameObject.name.Contains("background"))
            {
                star.gameObject.SetActive(true);
            }
        }
        foreach (GameObject star in stars)
        {
            star.SetActive(true);
        }
        canvas.SetActive(false);
    }

    public void ShowMenu()
    {
        canvas.SetActive(canvas.activeSelf ? false : true);
    }
}
