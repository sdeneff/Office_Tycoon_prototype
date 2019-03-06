using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMenu : MonoBehaviour
{
    private GameObject[] stars;
    private GameObject[] labels;
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.transform.GetChild(0).gameObject;
        stars = new GameObject[3];
        foreach(Transform star in canvas.transform)
        {
            if (star.gameObject.name.Contains("star"))
            {
                if(stars[0] == null)
                {
                    stars[0] = star.gameObject;
                }else if ( stars[1] == null)
                {
                    stars[1] = star.gameObject;
                }else
                {
                    stars[2] = star.gameObject;
                }
            }else if (star.gameObject.name.Contains("background"))
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu()
    {
        canvas.SetActive(canvas.activeSelf ? false : true);
    }
}
