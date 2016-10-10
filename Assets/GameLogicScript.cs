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

    private int gameMode = 0;
    private int whoseTurn;
    private int subMode;

    //gameMode constants
    const int SETUP = 0;
    const int TURNS = 1;
    const int GAMEOVER = 2;
    //subMode constants

    const int STARTING_TURN = 0;
    const int WAITING_FOR_OKAY = 1;
    const int WAITING_FOR_ROLL = 2;
    const int WAITING_FOR_DICE_RESULTS = 3;
    const int WAITING_FOR_UNIT_SELECTION = 4;
    // Update is called once per frame
    void Update () {
        switch (gameMode)
        {   case SETUP:
                //if we're still setting up, dont want to do anything
                break;
            case TURNS:
                switch (subMode)
                {
                    case STARTING_TURN:
                        //tell player it's their turn, wait for okay
                        //move the camera 

                        subMode = WAITING_FOR_OKAY;
                        break;
                    case WAITING_FOR_OKAY:
                        if (false) //if player has pressed okay
                        {
                            //close the okay menu
                            subMode = WAITING_FOR_ROLL;
                            
                        }
                        break;
                    case WAITING_FOR_ROLL:
                        if (false) //if player has shaken the device
                        {
                            //dice.roll();
                            subMode = WAITING_FOR_DICE_RESULTS;
                        }
                        break;
                    case WAITING_FOR_DICE_RESULTS:
                        if (false)//dice.finishedRolling)
                        {
                            //diceResult = dice.getResult();
                            //open instruction window to touch a moveable player
                            //highlightMovablePlayers(diceResult);
                            subMode = WAITING_FOR_UNIT_SELECTION;
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
            case GAMEOVER:
                break;
            
        }

            
           
	}

    //unit constants

    const int HOME = 0;
    const int FEILD = 1; 
    const int TARGET = 2;

    int highlightMovableUnits(int n)
    { //return number of movable units
        int c = 0;

        switch (whoseTurn)
        {
            case 1:
                //if they roll a 6 and start pebble not occupied, highlight all in homerow
                if (n == 6 && !boardInterface.stoneList[boardInterface.startStoneP1].occupiedBy)
                {
                    foreach (GameObject unit in unitListP1)
                    {
                        unitInterface I = unit.GetComponent<unitInterface>();
                        if (I.status == HOME)
                        {
                            I.highlight1();
                            c++;
                        }
                    }
                   
                }

                //Then highlight all movable objects in feild
                foreach (GameObject unit in unitListP1)
                {
                    unitInterface I = unit.GetComponent<unitInterface>();
                    if (I.status == FEILD)
                    {  //first, check if they can make it into their target row
                        int targetIndex = (I.currStone.index + n) % boardInterface.stoneList.Length;
                        for (int i = 1; i <= n; i++)
                        {
                            if(((I.currStone.index + n) % boardInterface.stoneList.Length) == boardInterface.startStoneP1)
                            { //then they can make it to target and should be highlighted. 
                              //JESSIE: there's an extra rule that you go into the homerow and 'bounce out' if you dont land exactly, but I'm ignoring that for now.
                                I.highlight1();
                                c++;
                            
                            }
                        }
                        if (!(boardInterface.stoneList[targetIndex].occupiedBy.player == whoseTurn))
                        {
                            I.highlight1();
                            c++;
                        }


                    }
                    

                }

                break;
            case 2:
                //TODO similarly for player 2;

                break;

        }
        return c;
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

        unitInterface I;
        for (int i =0; i< numUnits; i++)
        {
            print("adding units to board" + i);
            unitListP1[i] = Instantiate(unitPrefabP1); //instanceunitPrefabP1
            I = unitListP1[i].GetComponent<unitInterface>();
            I.setUp();
            I.currStone = boardInterface.homeRowP1[i];
            I.moveTo(I.currStone);
            I.player = 1;
            I.id = i;

            unitListP2[i] = Instantiate(unitPrefabP2);

            I = unitListP2[i].GetComponent<unitInterface>();
            I.setUp();
            I.currStone = boardInterface.homeRowP2[i];
            I.moveTo(I.currStone);
            I.player = 1;
            I.id = i;

            

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
