using UnityEngine;
using System.Collections;

public class MainCamController : MonoBehaviour
{
    public float speed = 1.5f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float roll = 0.0f;


    void Start()
    {

        this.transform.position = new Vector3(50, 75, -15);
        this.transform.rotation = Quaternion.Euler(60, 0, 0);

    }
    void Update()
    {

        // The update of mouse control the yaw and picth
        yaw += speed * Input.GetAxis("Mouse X");
        pitch += speed * Input.GetAxis("Mouse Y");

        //The update of keyboard WSAD
        Vector3 positionChange = WSADInput();
        transform.Translate(positionChange);

        //The update of rotation by QE;
        roll += QEInput();
        transform.eulerAngles = new Vector3(60 + pitch, yaw, roll);


    }
    private float QEInput()
    {
        float value = 0.0f;
        if (Input.GetKey(KeyCode.E))
        {
            value += speed;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            value -= speed;
        }
        return value;
    }
    private Vector3 WSADInput()
    {
        Vector3 dirChanges = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            dirChanges += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            dirChanges += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            dirChanges += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            dirChanges += new Vector3(-1, 0, 0);
        }
        return dirChanges;
    }
}
