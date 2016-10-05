using UnityEngine;
using System.Collections;
using System;

public class bouncyController : MonoBehaviour {

    public GameObject head;
    public GameObject body;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("up")) bobble();
        if (Input.GetKeyDown("b"))
        {
            bounceTo(targetX, targetZ);
        }

        if (Input.GetKeyDown("s"))
        {
            smallBounceTo(targetX, targetZ);
        }


        if (bouncing && Time.time >= bounceEndTime)
        {
            endBounce();
            if (smallBouncing)
            {
                smallBounceCount++;
                if (smallBounceCount < smallBounceList.Length)
                {
                    bounceTo(smallBounceList[smallBounceCount].x, smallBounceList[smallBounceCount].z);
                }

                else endSmallBounce();
            }
        }
	}

    private void endSmallBounce()
    {
        smallBouncing = false;
    }

    public void endBounce()
    {
        bouncing = false;
        body.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(currBounceTargetX, transform.position.y, currBounceTargetZ);
    }

    public float targetX = 1;
    public float targetZ = 0;

    //public float bounceHeight = 1;
    public float horizontalSpeed = 1;
    private float bounceEndTime;
    private bool bouncing = false;
    //private float U;

    private float currBounceTargetX;
    private float currBounceTargetZ;
    void bounceTo(float x, float z)
    {

        print("bouncing to: " + x + ", " + z);
        currBounceTargetX = x;
        currBounceTargetZ = z;

        bouncing = true;
        Vector3 direction = getDirection(transform.position.x, transform.position.z, x, z);
        float distance = getDistance(transform.position.x, transform.position.z, x, z);
        float t = distance / horizontalSpeed;
        float U = -Physics.gravity.y * t / 2;
        //debug
        print("current position: " + transform.position);
        print("target position: " + targetX + ", " + targetZ);
        print("direcion: " + direction);
        print("distance: " + distance);
        print("horizontal speed: " + horizontalSpeed);
        print("time: " + t);
        print("gravity: " + Physics.gravity);
        print("U: " + U);
        
        this.GetComponent<Rigidbody>().velocity = direction * horizontalSpeed;
        body.GetComponent<Rigidbody>().velocity = Vector3.up * U;

        bounceEndTime = Time.time + t;
    }
    float getDistance(float fromX, float fromY, float toX, float toY)
    {
        return Mathf.Sqrt((toX - fromX) * (toX - fromX) + (toY - fromY) * (toY - fromY));
    }

        Vector3 getDirection(float fromX, float fromY, float toX, float toY)
    {
        Vector3 r = new Vector3(toX - fromX, 0, toY - fromY);
        r.Normalize();
        return r;
    }

    public float maxBounceDistance = 2;

    private Vector3[] smallBounceList;
    private bool smallBouncing = false;
    private int smallBounceCount;
    void smallBounceTo(float x, float z)
    {
        
        float distance = getDistance(x, z, transform.position.x, transform.position.z);
        Vector3 direction = getDirection(transform.position.x, transform.position.z, x, z);
        int n = calcNumBounces(distance, maxBounceDistance);
        float distPerBounce = distance / n;
        smallBounceList = new Vector3[n];
        smallBounceList[0] = transform.position + direction * distPerBounce;
        for (int i = 1; i < n; i++)
        {
            smallBounceList[i] = smallBounceList[i - 1] + direction * distPerBounce;
        }
        smallBouncing = true;
        smallBounceCount = 0;
        bounceTo(smallBounceList[0].x, smallBounceList[0].z);
    }

    private int calcNumBounces(float distance, float maxDistance)
    {
        int i = 1;
        while (distance /i > maxDistance)
        {
            i++;
        }
        return i;
    }

    public float bobbleForce = 100;
    void bobble()
    {
        head.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up*bobbleForce);
    }
}
