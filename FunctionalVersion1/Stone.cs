using UnityEngine;
using System.Collections;

public class Stone : Object {
    //an invisible stepping stone for the units to navigate by.
    public Vector3 pos;
    public unitInterface occupiedBy;
    public int index;
    

    public Stone(float posX, float posZ, int index)
    {
        pos = new Vector3(posX, 0, posZ);
        occupiedBy = null;
        this.index = index;
         
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
