using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menus : MonoBehaviour {


    public Transform canvas;

    public GameObject OKAYpromptPrefab;
    GameObject OKAYpromptCopy;
    GameObject b;
    // Use this for initialization
    void Start () {
        OKAYpromptCopy = Instantiate(OKAYpromptPrefab);
        OKAYpromptCopy.transform.parent = canvas;
        b = OKAYpromptCopy.transform.GetChild(0).gameObject;
        Button b2 = b.GetComponent<Button>();
        b2.onClick.AddListener(() => pressedOKAY());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pressedOKAY()
    {
        print("yup, got it");
        Destroy(OKAYpromptCopy);
        //now do whatever you want with it.
    }

    

    
}
