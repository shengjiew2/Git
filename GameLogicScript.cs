//I HAVE CHANGED THIS AND RE UPLOADED TO CHECK


using UnityEngine;
using System.Collections;

public class GameLogicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        setUp();
	
	}


    public GameObject board;
    

    private string gameMode = "setUp";
    
	// Update is called once per frame
	void Update () {
        switch (gameMode)
        {   case "setUp":
                //if we're still setting up, dont want to do anything
                break;
            case "turns":
                //tell player to roll dice
                //understand that player has pressed okay
                //wait for player to roll dice
                //get result of dice roll
                //tell player to select unit
                    //highlight selectable units
                    //if no selectable units, tell player
                    //wait for okay,
                    //next players turn           
                //wait for player to select unit
                //ask player if sure
                //wait for player to confirm or cancel
                //if cancel, go back
                //else, move unit dice places
                //if land on other, 
                    //move other back home
                //wait for unit to finish
                //next players turn


                break;
            case "winner":
                break;
            
        }

            
            




	
	}


    void setUp()
    {
        
        addBoard();
        addCharactersToBoard();
        
    }

    /*
    Board:
    has an array of stones for the path
    has an index of start/finish stones for player one and two
    has arrays of start/end stones for player one and two
    
    these arrays should have xy co-oridnates, relative to the board or not? i dont know

    has a dice
    has a roll dice method
    has finished rolling attribute
    has result attibe
    sends dice answer message to main script




   

}
