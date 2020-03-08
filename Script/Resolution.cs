using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    public int xResolution = 1280, yResolution = 720;

    void Awake()
    {
        ResolutionOnAwake();
    }

    public void ResolutionOnAwake()
    {
        Screen.SetResolution(xResolution, yResolution, true);

        Camera.main.aspect = 16f / 9f;
    }
}
