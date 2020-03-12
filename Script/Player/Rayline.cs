using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayline : MonoBehaviour
{
     void Update()
    {
        //Ray ray = new Ray(transform.position, transform.forward);
        //Debug.DrawLine(ray.origin, ray.direction, Color.black);
        //print("OK");

        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), ray.direction, Color.black);
    }

}
