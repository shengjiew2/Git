using UnityEngine;
using System.Collections;

public class StoneHandlerScript : MonoBehaviour {
    public GameObject board;
    public BoardInterface boardInt;
    
	// Use this for initialization
	void Start () {
        boardInt = board.GetComponent<BoardInterface>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
