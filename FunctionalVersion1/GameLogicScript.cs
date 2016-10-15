//I HAVE CHANGED THIS AND RE UPLOADED TO CHECK


using UnityEngine;
using System.Collections;
using System;

public class GameLogicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        setUp();
	
	}
    public const int numPlayers = 2;

    public GameObject boardObject;
    private BoardInterface board;

    public GameObject instructionPromptObject;
    public GameObject okayPromptObject;
    private InstructionScript iPrompt;
    private OKscript okPrompt;
    

    public GameObject unitPrefabP1;
    public GameObject unitPrefabP2;

    public GameObject diceObject; //this can be a child of the board, but would be nice to access directly
    private diceInterface dice;
    private int gameMode = 0;
    private int whoseTurn;
    private int subMode;


    private int diceResult = 1;

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
    const int MOVING_UNIT = 5;

    public unitInterface movingUnit = null;

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
                        okPrompt.setMainText("Player " + (whoseTurn + 1) + "'s Turn");
                        okPrompt.open();
                        subMode = WAITING_FOR_OKAY;
                        print("waiting for okay");
                        break;
                    case WAITING_FOR_OKAY:
                        if (okPrompt.okClicked) //if player has pressed okay
                        {
                            okPrompt.close();
                            subMode = WAITING_FOR_ROLL;
                            print("waiting for roll");
                        }
                        break;
                    case WAITING_FOR_ROLL:
                        if (Input.GetMouseButton(0)) //if player has shaken the device
                        {
                            print("rolling");
                            dice.roll();
                    
                            subMode = WAITING_FOR_DICE_RESULTS;
                        }
                        break;
                    case WAITING_FOR_DICE_RESULTS:
                        if (dice.hasStopped())
                        {
                            diceResult = dice.getResult();
                            print("result: " + diceResult);
                            int numMovable = highlightMovableUnits(diceResult);
                            print(numMovable + " movable units");
                            if (numMovable == 0)
                            {
                                endTurn();
                            }
                            else
                            {
                                subMode = WAITING_FOR_UNIT_SELECTION;
                                //open instruction window to touch a moveable player
                            }
                        }

                        break;
                    case WAITING_FOR_UNIT_SELECTION:
                        if (Input.GetMouseButton(0))
                        {
                            unitInterface U;
                            print("clicked");
                            if ((U = (unitInterface)getClickedOnUnit()) != null)
                            {
                                //endTurn();
                                movingUnit = U;
                                subMode = MOVING_UNIT;
                                print("found unit");       
                                U.highlight2();
                                unlightAllUnits1();
                                U.movePlaces(diceResult);
                            }
                            
                        }
                        break;
                    case MOVING_UNIT:
                        {
                            if (movingUnit.numToJump == 0)
                            {   

                                



                                if (movingUnit.currStone.occupiedBy != null)
                                {
                                    movingUnit.currStone.occupiedBy.sendHome();
                                }
                                movingUnit.currStone.occupiedBy = movingUnit;
                                movingUnit.highlight0();
                                endTurn();
                            }
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
                //switch to the congratulations sceen, etc.
                break;
            
        }

            
           
	}


    /*unlight all highlighted 1 units*/
    private void unlightAllUnits1()
    {
        foreach (GameObject unit in unitLists[whoseTurn])
        {
            unitInterface I = unit.GetComponent<unitInterface>();
            if (I.highlight == 1) ;
            I.highlight0();
        }
    }

    private int winner;

    void endTurn()
    {
        //check if the current player has won
        Boolean won = true;
        foreach (GameObject unit in unitLists[whoseTurn])
        {
            unitInterface I = unit.GetComponent<unitInterface>();
            if (I.status != TARGET)
            {
                won = false;
                break;
            }
        }

        if (won)
        {
            winner = whoseTurn;
            gameMode = GAMEOVER;
        }

        whoseTurn = (whoseTurn + 1) % numPlayers;
        subMode = STARTING_TURN;
    }

    //unit constants

    unitInterface getClickedOnUnit()
    {
        //if clicked on object has no unitInterface (ie, is not a unit) will return null
        //or if nothing hit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 1000))
        {
            //print("got nothing");
            return null;
        }
       // print("got something");

        GameObject hitObject = hit.transform.gameObject;
        //print(hit.transform);

        unitInterface I = hitObject.GetComponentInParent<unitInterface>();

        if (I == null) return null;
        if (I.highlight == MOVEABLE) return I;
        return null;

        
        

        
    }

    const int HOME = 0;
    const int FEILD = 1; 
    const int TARGET = 2;

    const int MOVEABLE = 1;

    int highlightMovableUnits(int n)
    { 
        //return number of movable units
        int c = 0;

        //if they roll a 6 and start pebble not occupied, highlight all in homerow
        if (n == 6 && (!(board.stoneList[board.startStones[whoseTurn]].occupiedBy) || board.stoneList[board.startStones[whoseTurn]].occupiedBy.player != whoseTurn))
        {
            foreach (GameObject unit in unitLists[whoseTurn])
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

        foreach (GameObject unit in unitLists[whoseTurn])

        {

            unitInterface I = unit.GetComponent<unitInterface>();

            if (I.status == FEILD)

            {
                //first, check if they can make it into their target row
                print("checking stone: " + I.id);
                print("currIndex: " + I.currStone.index);
               int targetIndex = (I.currStone.index + n) % (board.stoneList.Length);

                print("target Index: " + targetIndex);

                for (int i = 1; i <= n; i++)
                {
                    if (((I.currStone.index + i) % board.stoneList.Length) == board.startStones[whoseTurn])

                    { //then they can make it to target and should be highlighted. 
                      //JESSIE: there's an extra rule that you go into the homerow and 'bounce out' if you dont land exactly, but I'm ignoring that.
                        if (I.highlight != 1)
                        {
                            I.highlight1();
                            c++;
                        }
                    }

                }

                //if target stone is not occupied, or occupied by opponent players only
                if (!(board.stoneList[targetIndex].occupiedBy) || !(board.stoneList[targetIndex].occupiedBy.player == whoseTurn))
                {
                    if (I.highlight != 1)
                    {
                        I.highlight1();
                        c++;
                    }
                }
            }
        }
        print("there are " + c + " movable units");
        return c;       
    }




    void setUp()
    {



        //adBoard(); //NOT HERE//do this manually (drag board object to attribute)
        board = boardObject.GetComponent<BoardInterface>();
        board.isWorking = false;
        board.generateStoneLists();

        //set up all interface/instruction panels, but make them inactive

        dice = diceObject.GetComponent<diceInterface>();

        iPrompt = instructionPromptObject.GetComponent<InstructionScript>();
        
        iPrompt.close();
        okPrompt = okayPromptObject.GetComponent<OKscript>();
        
        okPrompt.close();


        addUnitsToBoard();


        //if finished setting up, start game
        startGame();
    }

    private void startGame()
    {
        gameMode = TURNS;
        whoseTurn = 0;
        subMode = STARTING_TURN;
    }
    

    public GameObject[][] unitLists;
    //public GameObject[] unitListP1;
    //public GameObject[] unitListP2;
    public const int numUnits = 3; //each


    void addUnitsToBoard()
    { 

        print(board.isWorking);
        print("B " + board.homeRows[0][0].pos.x);
        //create units and put them on the homeRow
        print("adding units to board");

        unitLists = new GameObject[numPlayers][];
        unitLists[0] = new GameObject[numUnits];
        unitLists[1] = new GameObject[numUnits];

        GameObject[] unitPrefabs  = new GameObject[numPlayers];
        unitPrefabs[0] = unitPrefabP1;
        unitPrefabs[1] = unitPrefabP2;

        unitInterface I;
        for (int p = 0; p < numPlayers; p++)
        {
            for (int i = 0; i < numUnits; i++)
            {
                print("adding units to board" + i);
                unitLists[p][i] = Instantiate(unitPrefabs[p]); //instanceunitPrefabP1
                I = unitLists[p][i].GetComponent<unitInterface>();
                I.setUp();
                I.board = board;
                I.currStone = board.homeRows[p][i];
                I.teleportTo(I.currStone);
                I.moveTo(I.currStone);
                I.player = p;
                I.id = i;

            }
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
