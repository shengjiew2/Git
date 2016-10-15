using UnityEngine;
using System.Collections;

public class diceInterface : MonoBehaviour {

    private DisplayCurrentValue value;
    private ApplyForceInRandomDirection thrower;
    //public GameObject dice;
	// Use this for initialization
	void Start () {
	    value = this.GetComponent<DisplayCurrentValue>();
        thrower = this.GetComponent<ApplyForceInRandomDirection>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void roll()
    {
        thrower.Throw();
    }

    public bool hasStopped()
    {
        return (GetComponent<Rigidbody>().velocity.magnitude < stillnessThreshold);
    }

    public int getResult()
    {
        
        return value.currentValue;
       
    }

    public float stillnessThreshold;

}
