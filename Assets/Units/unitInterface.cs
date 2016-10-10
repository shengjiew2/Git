using UnityEngine;
using System.Collections;

public class unitInterface : MonoBehaviour {


    //how this interacts with the board/pebbles will be done later, but works for now.
    Stone CurrentPebble;
    bouncyController control;

    // Use this for initialization
    void Start()
    {
        //these initialisation stuffs aren't getting executed. WHY NOT? Maybe only once per game?
        control = this.GetComponent<bouncyController>();

    }
    public void setUp()
    {
        control = this.GetComponent<bouncyController>();
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
            control.targetX += 1;
            numToJump--;
        }
    }


    void highlight1()
    {
        //change colour and bobble ears
    }

    void highlight2()
    {
        //change colour and bobble head
    }

    public int id; //will be 0-4 and intruct which position is the home row

    public void moveTo(Stone stone)
    {
        print("moving to stone");
        print("C "+stone.pos.x);
        print("D " + stone.pos.z);
        //small set target to stone's position
        control.targetX = stone.pos.x;
        control.targetZ = stone.pos.z;
    }

    private int numToJump;

    public void movePlaces(int n)
    {
        numToJump = n;
    }

  






}
