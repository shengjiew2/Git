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

    public Stone[] stoneList; //list of stones in the main path
    public Stone[] homeRowP1; //list of player1's homerow stones
    public Stone[] homeRowP2; //list of player2's homerow stones
    public Stone[] targetRowP1; //list of player1's targetrow stones
    public Stone[] targetRowP2; //list of player2's targetrow stones

    public int startStoneP1; //index of stoneList player 1 starts at
    public int startStoneP2; //index of stoneList player 2 starts at

    public Boolean isWorking;

    // Use this for initialization
    void Start () {
        //generateStoneLists();
        //print("stoneLists generated");
        //isWorking = false;
        //print(isWorking);

	}

    //visualising placement of stones
    public void OnDrawGizmos()
    {
        //shows us where the stones we made have gone, 
        ///(not seen by end users)


        Gizmos.color = Color.black;
        foreach (Stone stone in stoneList)
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }

        Gizmos.color = Color.blue;
        foreach (Stone stone in homeRowP1)
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        Gizmos.color = Color.green;
        foreach (Stone stone in homeRowP2)
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        Gizmos.color = Color.cyan;
        foreach (Stone stone in targetRowP1)
        {
            Gizmos.DrawCube(stone.pos, Vector3.one * .5f);
        }
        Gizmos.color = Color.yellow;
        foreach (Stone stone in targetRowP2)
        {
            Gizmos.DrawCube(stone.pos, Vector3.one*.5f);
        }

    }

    public void generateStoneLists()
    {

        //this is where we should calculate the stone positions based on physical 
        //stones added, if necessary. 
        
        //for now, placeholder stone positions for testing
        stoneList = new Stone[28];
        for (int i = 0; i < 28; i++)
        {
            stoneList[i] = new Stone(i, Mathf.Abs(13 - i));
        }

        startStoneP1 = 0;
        startStoneP2 = 13;
        homeRowP1 = new Stone[4];
        homeRowP2 = new Stone[4];
        targetRowP1 = new Stone[4];
        targetRowP2 = new Stone[4];

        for (int i = 0; i < 4; i++)
        {
            homeRowP1[i] = new Stone(-1, i);
            homeRowP2[i] = new Stone(29, i);

            print("homerow: " + i + homeRowP1[i].pos.z);
            isWorking = true;
            targetRowP1[i] = new Stone(30, i);
            targetRowP2[i] = new Stone(-2, i);

        }
        print("finished generating stone list");
        isWorking = true;
        print("B "+ homeRowP1[0].pos.x);


    }

    // Update is called once per frame
    void Update () {
	
	}
}
