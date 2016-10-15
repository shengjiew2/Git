using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionScript : MonoBehaviour {
    public GameObject mainTextHolder;
    public GameObject me;

    Text mainText;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setMainText(string newMainText)
    {
        mainText.text = newMainText;
    }

    public void close()
    {

        //then deactivate

        me.SetActive(false);


    }

    public void open()
    {
        me.SetActive(true);

    }
    
}
