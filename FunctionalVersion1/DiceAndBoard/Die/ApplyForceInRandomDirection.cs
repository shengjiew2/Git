using UnityEngine;
using System.Collections;

public class ApplyForceInRandomDirection : MonoBehaviour {

    public string buttonName = "Fire1";
    public float forceAmount = 10.0f;
    public float torqueAmount = 10.0f;
    public ForceMode forceMode;
   
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetButtonUp(buttonName))
        {
            
            GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * forceAmount, forceMode);
            GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * torqueAmount, forceMode);
        }*/
    
	}

    public void Throw()
    {   //instead of random force, I've changed it to random direction, so that we dont have to worry about 
        //applying it for an amount of time
        //(before, holding the button longer meant a bigger throw
        //GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * forceAmount, forceMode);

        float K = 0.5f/(GetComponent<Rigidbody>().mass);  //Ft = mv, so v = (t/m) * F. Assume t = 500ms and get mass. 
        
        GetComponent<Rigidbody>().velocity = Random.onUnitSphere * forceAmount*K;
        
        //GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * torqueAmount, forceMode);
        GetComponent<Rigidbody>().angularVelocity = Random.onUnitSphere * torqueAmount*K;
    }
}
