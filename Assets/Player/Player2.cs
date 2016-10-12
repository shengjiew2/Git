using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour
{

    public float speed = 15.0f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        //The start point will go to the first way point
        target = WayPath2.wayPoints[0];
    }

    void Update()
    {   //find out the dirction of next place
        Vector3 dir = target.position - transform.position;

        //Make sure the speed is constains, now it will move auto depends on time, we will change it to dice number later on
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //check whether arrive the next way point or not
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

    }
    void GetNextWayPoint()
    {   //check does it get to the end
        if (wavepointIndex >= WayPath2.wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        //if not the end it will move towards the next point
        wavepointIndex++;
        target = WayPath2.wayPoints[wavepointIndex];
    }
}
