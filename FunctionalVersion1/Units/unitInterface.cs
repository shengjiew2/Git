using UnityEngine;
using System.Collections;

public class unitInterface : MonoBehaviour {


    //how this interacts with the board/pebbles will be done later, but works for now.
    public Stone currStone;
    public BoardInterface board;
    bouncyController control; //handles the detailed stuff like trajectory calculation
    public int id; //will be 0-4 and intruct which position is the home row
    public int status; //HOME = 0, FEILD = 1; TARGET = 2;
    public int player;
    public int highlight; // 0 is normal, 1 highlight1, ect. 



    public const int HOME = 0;
    public const int FEILD = 1;
    public const int TARGET = 2;


    // Use this for initialization
    void Start()
    {
        //these initialisation stuffs aren't getting executed. WHY NOT? Maybe only once per game?
        control = this.GetComponent<bouncyController>();

    }
    public void setUp()
    {
        control = this.GetComponent<bouncyController>();
        
        highlight = 0;
        status = 0;
    }

    
    // Update is called once per frame
    void Update()
    {


        //tester functins
        if (Input.GetKeyDown("n"))
        {
            print("got input n");
            movePlaces(3);
        }

        if (Input.GetKeyDown("1")) highlight1();
        if (Input.GetKeyDown("2")) highlight2();
            




        if (!control.smallBouncing && numToJump > 0)
        {
            //set target as next pebble
            Stone nextStone = board.stoneList[(currStone.index + 1)%board.stoneList.Length];
            moveTo(nextStone);
            numToJump--;
        }
    }


    public void highlight0()
    {
        //change back
        highlight = 0;

    }

    public void highlight1()
    {
        //change colour and bobble ears
        control.earBobble();
        highlight = 1;

    }

    public void highlight2()
    {
        highlight = 2;
        control.bobble();
        //change colour and bobble head
    }

    public void highlightLOCKED()
    {
        highlight = 3;
        //turn grey
    }

    public void teleportTo(Stone stone)
    {
        this.transform.position = stone.pos + Vector3.up * 3;
    }

    

    public void moveTo(Stone stone)
    {
        //if you're about to make it back to the start stone, go 
        //to your home row instead
        if (status == FEILD && stone.index == board.startStones[player])
        {
            sendToTarget();
            return;
        }

        //small set target to stone's position
        control.targetX = stone.pos.x;
        control.targetZ = stone.pos.z;
        currStone = stone;
    }

    public int numToJump = 0;

    public void sendToTarget()
    {
        Stone stone = board.targetRows[player][id];
        //small set target to stone's position
        control.targetX = stone.pos.x;
        control.targetZ = stone.pos.z;
        currStone = stone;
        status =TARGET;
        highlightLOCKED();

    }

    public void sendHome()
    {
        //currStone.occupiedBy = opponentPeice
        moveTo(board.homeRows[player][id]);
        status = HOME;
    }

    public void movePlaces(int n)
    {
        currStone.occupiedBy = null;
        if (this.status == HOME)
        {
            

            moveTo(board.stoneList[board.startStones[player]]);
            //moveTo(board.stoneList[0]);
            status = FEILD;
        }
        else
        {
            numToJump = n;
        }
        
    }

  






}
