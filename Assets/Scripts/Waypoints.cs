using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] WaypointsArray;

    private void Awake()
    {
        int count = transform.childCount;
        WaypointsArray = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            WaypointsArray[i] = transform.GetChild(i);
        }
    }
}
