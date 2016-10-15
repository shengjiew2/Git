using UnityEngine;
using System.Collections;
using System;

/* This should hold the list of stone positions, 
 * the index of each players' entry stone
 * and the list of each players' home and target rows.
 * 
 * JESSIE: this may be replaced/added to any board script you are working on, I just
 * needed something with basic functionality to use with the game logic. 
 * */

public class BoardInterface : MonoBehaviour {

    public const int numPlayers = 2;
    public const int numStonesInFeild = 28;
    public const int numUnitsPerPlayer = 3;

    public Stone[] stoneList; //list of stones in the main path
    //public Stone[] homeRowP1; //list of player1's homerow stones
    //public Stone[] homeRowP2; //list of player2's homerow stones
   // public Stone[] targetRowP1; //list of player1's targetrow stones
    //public Stone[] targetRowP2; //list of player2's targetrow stones

    public Stone[][] targetRows; //list of target rows for each player
    public Stone[][] homeRows; //list of home rows for each player

    public int[] startStones; //index of stoneList each player starts at
    //public int startStoneP1; //index of stoneList player 1 starts at
    //public int startStoneP2; //index of stoneList player 2 starts at

    public Boolean isWorking;
    public Boolean ready = false;
    // Use this for initialization
    void Start () {
        ready = false;
        generateStoneLists();
        ready = true;
        print("stoneLists generated");
        //isWorking = false;
        //print(isWorking);

	}

    //visualising placement of stones
    public void OnDrawGizmos()
    {
        if (!ready)
        {
            print("not ready");
            return;
        }
        else print("ready");
        //shows us where the stones we made have gone, 
        //(not seen by end users)
        

        Gizmos.color = Color.black;
        foreach (Stone stone in stoneList)
        {
            
            print("drew stones " + stone.index);
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        

        Gizmos.color = Color.blue;
        foreach (Stone stone in homeRows[0])
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        Gizmos.color = Color.green;
        foreach (Stone stone in homeRows[1])
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        Gizmos.color = Color.cyan;
        foreach (Stone stone in targetRows[0])
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        Gizmos.color = Color.yellow;
        foreach (Stone stone in targetRows[1])
        {
            Gizmos.DrawCube(stone.pos, Vector3.one*.5f);
        }
        
        print("gizmos drawn");
        
    }
    

    public void generateStoneLists()
    {

        //this is where we should calculate the stone positions based on physical 
        //stones added, if necessary. 
        
        //for now, placeholder stone positions for testing
        stoneList = new Stone[numStonesInFeild];

        int stonesPerRow = numStonesInFeild/4;
        float rowWidth = 9;
        float gap = rowWidth / (stonesPerRow+1);
        float pos = rowWidth / 2;
        float start = rowWidth / 2 - gap;

        //gap + (stonesPerRow * gap) + gap == width

        //top row, from left to right
        int c = 0;
        for (int i = 0; i < stonesPerRow; i++)
        {
            stoneList[c] = new Stone(-start+ gap*i, pos, c);
            print(stoneList[c].pos);
            c++;

        }
        
        //right col, from top to bottem
        for (int i = stonesPerRow-1; i >=0 ; i--)
        {
            stoneList[c] = new Stone(pos, -start + gap *i, c);
            c++;
        }
        //bottem row, from right to left
        for (int i = stonesPerRow-1; i >= 0; i--)
        {
            stoneList[c] = new Stone(-start + gap*i, -pos, c);
            c++;
        }

        //left col, from bottom to top
        for (int i = 0; i < stonesPerRow; i++)
        {
            stoneList[c] = new Stone(-pos, -start + gap*i, c);
            c++;
        }
        print("c = " + c);
        
        
        
        startStones = new int[numPlayers];

        startStones[0] = 24; //left middle
        startStones[1] = 10; //right middle
        homeRows = new Stone[numPlayers][];
        targetRows = new Stone[numPlayers][];
        homeRows[0] = new Stone[numUnitsPerPlayer];
        homeRows[1] = new Stone[numUnitsPerPlayer];
        
        targetRows[0] = new Stone[numUnitsPerPlayer];
        targetRows[1] = new Stone[numUnitsPerPlayer];

        Vector3 startPosP1 = stoneList[startStones[0]].pos;

        for (int i = 0; i < 3; i++)
        {
            
            //home rows: left and right "pathways" respectively //FUCK IT LETS JUST HAVE THREE
            homeRows[0][i] = new Stone(-start + gap * i, 0, i);
            homeRows[1][i] = new Stone(start - gap * i, 0, i);

            //just make the target rows the same
            isWorking = true;
            targetRows[0][i] = new Stone(-start + gap * i, 0, i);
            targetRows[1][i] = new Stone(start - gap * i, 0, i);

        }
        
        print("finished generating stone list");
        isWorking = true;
        //print("B "+ homeRows[0].pos.x);


    }

    // Update is called once per frame
    void Update () {
	
	}
}
