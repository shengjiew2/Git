using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {
    private Rigidbody rb;

    public float flySpeed = 50;
    public float rollSpeed = 10;
    public float turnSpeed = 20;
    public float pitchYawThreshold = 0.1f;
    public float pitchYawSpeed = 20;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update () {
        float moveForward = Input.GetAxis("Forward");
        float moveRight = Input.GetAxis("Right");

        float moveRoll = Input.GetAxis("Roll");
        float movePitch;
        float moveYaw;

        float mousePosX = Input.mousePosition.x / Screen.width - 0.5f;
        float mousePosY = Input.mousePosition.y / Screen.height - 0.5f;
        
        if (Mathf.Abs(mousePosX)< pitchYawThreshold)
        {
            moveYaw = 0;
        }
        else
        {
            moveYaw = mousePosX;
        }

        if (Mathf.Abs(mousePosY) < pitchYawThreshold)
        {
            movePitch = 0;
        }
        else
        {
            movePitch = mousePosY;
        }



        //Vector3 movement = new Vector3(0, 0, moveForward);
        //Vector3 rotation = new Vector3(movePitch*pitchYawSpeed, moveYaw*pitchYawSpeed, -moveRoll * rollSpeed);
        //rb.AddRelativeForce(movement*flySpeed);


       // rb.AddRelativeTorque(rotation);
        rb.velocity = transform.forward * moveForward * flySpeed + transform.right * moveRight * flySpeed;
        transform.Rotate(Vector3.up * Time.deltaTime*moveYaw*pitchYawSpeed);
        transform.Rotate(-Vector3.right * Time.deltaTime * movePitch * pitchYawSpeed);
        transform.Rotate(Vector3.forward * Time.deltaTime * moveRoll * pitchYawSpeed);
        //transform.Translate(movement);
        //transform.Rotate(rotation);
    }
}
