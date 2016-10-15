using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OKscript : MonoBehaviour {


    public bool okClicked = false;
    public GameObject me;

    public GameObject mainTextHolder;

    Text mainText;
    // Use this for initialization
    void Start () {
        mainText = mainTextHolder.GetComponent<Text>();
        print("start has happpened");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    


    public void ok()
    {
        okClicked = true;
        setMainText("Yep, that worked allright");
        
    }

    public void setMainText(string newMainText)
    {
        print("trying to print: " + newMainText);
        mainText.text = newMainText;
    }

    public void close()
    {

        //then deactivate
        okClicked = false;
        me.SetActive(false);


    }

    public void open()
    {
        me.SetActive(true);

    }
}
