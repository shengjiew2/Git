using UnityEngine;
using System.Collections;

public class OKscript : MonoBehaviour {


    public bool okClicked = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ok()
    {
        okClicked = true;
    }

    public void close()
    {
        okClicked = false;
        //then deactivate
    }
}
