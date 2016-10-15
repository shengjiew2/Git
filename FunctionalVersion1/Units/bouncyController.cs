using UnityEngine;
using System.Collections;
using System;

public class bouncyController : MonoBehaviour {

    /*always small bounce to Target if distance from target is more than threshold*/


    public GameObject head;
    public GameObject body;
    public GameObject earL;
    public GameObject earR;

    public float adj = 0.3f; //adjustment thrreshold

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
    
        rb = GetComponent<Rigidbody>();
        earBobbleForce = 20;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown("e")) earBobble();

        if (Input.GetKeyDown("up")) bobble();
        if (Input.GetKeyDown("b"))
        {
            bounceTo(targetX, targetZ);
        }

        if (Input.GetKeyDown("s"))
        {
            smallBounceTo(targetX, targetZ);
        }
        */

        if (!smallBouncing && distanceFromTarget() > adj)
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

    private float distanceFromTarget()
    {
        float dx = transform.position.x - targetX;
        float dz = transform.position.z - targetZ;
        return Mathf.Sqrt(dx * dx + dz * dz);
        
    }

    private void endSmallBounce()
    {
        smallBouncing = false;
        print("finished bouncing");
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
    public bool smallBouncing = false;
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
    public void bobble()
    {
        head.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up*bobbleForce);
    }

    public float earBobbleForce = 10;
    public void earBobble()
    {
        float time = 0.5f;
        //float mass = earL.GetComponent<Rigidbody>().mass; since the spring is the main thing stopping the ears, forget the mass
        
        //what if, instead of applying a force, we apply a force*time?
        earL.GetComponent<Rigidbody>().velocity = (Vector3.left * earBobbleForce)* time;
        earR.GetComponent<Rigidbody>().velocity = (Vector3.right * earBobbleForce) * time;

        //earL.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.one* earBobbleForce);
        //earR.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.one * earBobbleForce);

    }
}
