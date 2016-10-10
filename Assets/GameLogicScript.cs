//I HAVE CHANGED THIS AND RE UPLOADED TO CHECK


using UnityEngine;
using System.Collections;

public class GameLogicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        setUp();
	
	}


    public GameObject board;
    public BoardInterface boardInterface; 

    public GameObject unitPrefabP1;
    public GameObject unitPrefabP2;

    public GameObject dice; //this can be a child of the board, but would be nice to access directly

    private string gameMode = "setUp";
    private string whoseTurn;
    private string subMode;

    

	// Update is called once per frame
	void Update () {
        switch (gameMode)
        {   case "setUp":
                //if we're still setting up, dont want to do anything
                break;
            case "turns":
                switch (subMode)
                {
                    case "start turn":
                        //tell player it's their turn, wait for okay
                        subMode = "waiting for okay 1";
                        break;
                    case "waiting for okay 1":
                        if (false) //if player has pressed okay
                        {
                            //close the okay menu
                            subMode = "waiting for roll";
                            
                        }
                        break;
                    case "waiting for roll":
                        if (false) //if player has shaken the device
                        {
                            //dice.roll();
                            subMode = "waiting for dice result";
                        }
                        break;
                    case "waiting for dice result":
                        if (false)//dice.finishedRolling)
                        {
                            //diceResult = dice.getResult();
                            //open instruction window to touch a moveable player
                            //highlightMovablePlayers(diceResult);
                            subMode = "waiting for unit selection";
                        }

                        break;

                        
                    
                     
                        
                }




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

        //adBoard(); //NOT HERE//do this manually (drag board object to attribute)
        boardInterface = board.GetComponent<BoardInterface>();
        boardInterface.isWorking = false;
        boardInterface.generateStoneLists();
       
        print("A " + boardInterface.isWorking);
        
        addUnitsToBoard();

        //set up all interface/instruction panels, but make them inactive
        
    }
    public GameObject[] unitListP1;
    public GameObject[] unitListP2;
    public const int numUnits = 4; //each


    void addUnitsToBoard()
    {

        print(boardInterface.isWorking);
        print("B " + boardInterface.homeRowP1[0].pos.x);
        //create units and put them on the homeRow
        print("adding units to board");
        unitListP1 = new GameObject[numUnits];
        unitListP2 = new GameObject[numUnits];

        
        for (int i =0; i< numUnits; i++)
        {
            print("adding units to board" + i);
            unitListP1[i] = Instantiate(unitPrefabP1); //instanceunitPrefabP1
            unitListP1[i].GetComponent<unitInterface>().setUp();
            unitListP1[i].GetComponent<unitInterface>().moveTo(boardInterface.homeRowP1[i]);

            //unitListP2[i] = Instantiate(unitPrefabP2);
        }

        //add units to the unit lists of player 1 and 2
        //these units should have colour settings depending on which player they belong to 
        //and should have a position based on the board's homerow list for eah player

        //use a loop to create many instances of the unitPrefab public atrribute 
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

    */


   

}
