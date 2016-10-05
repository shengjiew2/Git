using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YNscript : MonoBehaviour {

    public bool yesClicked = false;
    public bool noClicked = false;
    public GameObject mainTextHolder;

    Text mainText;

	// Use this for initialization
	void Start () {
        mainText = mainTextHolder.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void yes()
    {
        yesClicked = true;
        setMainText("THIS WORKED");
    }

    public void no()
    {
        noClicked = true;
    }

    public void setMainText(string newMainText)
    {
        mainText.text = newMainText;
    }
}
