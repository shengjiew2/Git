using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {
    //an invisible stepping stone for the units to navigate by.
    public Vector3 pos;
    public GameObject occupiedBy;

    public Stone(float posX, float posZ)
    {
        pos = new Vector3(posX, 0, posZ);
        occupiedBy = null;
         
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
