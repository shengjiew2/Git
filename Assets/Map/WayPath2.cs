using UnityEngine;
using System.Collections;

public class WayPath2 : MonoBehaviour
{

    public static Transform[] wayPoints;

    void Awake()
    {   //make the WayPath for Player
        wayPoints = new Transform[transform.childCount];
        //wayPoints will contain all the wayPoint for this way path
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}
