using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenrator : MonoBehaviour {
    //Create a list of usable rooms for the dungeon
    public List<GameObject> rooms = new List<GameObject>();

    //Set the max number of rooms for the dungeon
    public int maxRooms;

    //Set what side the next room will be
    public int newRoomSide;

    //Set the first day
    GameObject door1;

    //Set the door to remove
    public string removeDoor;

    //Set the positions of the current room door and the next room door
    public Vector3 door2Pos;
    public Vector3 door1Pos;

    //Set the last room and the new room
    GameObject newRoom;
    GameObject lastRoom;

	// Use this for initialization
	void Start () {

        //Check if removeDoor is not null
        if (removeDoor != null || removeDoor != "")
        {
            //Find all tiles in the room
            foreach (Transform child in transform)
            {
                //Find all objects tagged to be removed and set
                if (child.gameObject.tag == removeDoor)
                    lastRoom.GetComponent<DungeonGenrator>().door2Pos = child.localPosition;
            }
        }

        //Set the new rooms side if the max rooms hasn't been reached
        if (GameObject.FindGameObjectsWithTag("room").Length != maxRooms)
        {
            newRoomSide = Random.Range(0, 1);
        }
        else
        {
            newRoomSide = -1;
        }

        //Check if removed door can be done
        if (removeDoor != null)
        {
            //Find each tile in the room
            foreach (Transform child in transform)
            {
                //If any tile is tagged to be removed remove them
                if (child.gameObject.tag == removeDoor)
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }

        //If the rooms side is to the right...
        if (newRoomSide == 0)
        {
            //.. find every tile in the room
            foreach (Transform child in transform)
            {
                //Set exit door to the right door and store its position
                if (child.gameObject.tag == "doorR")
                {
                    door1 = child.gameObject;
                    door1Pos = child.localPosition;
                }
            }
            //Remove the exit door
            Object.Destroy(door1);
        }

        //If the rooms side is to the right...
        if (newRoomSide == 1)
        {
            //.. find every tile in the room
            foreach (Transform child in transform)
            {
                //Set exit door to the left door and store its position
                if (child.gameObject.tag == "doorL")
                {
                    door1 = child.gameObject;
                    door1Pos = child.localPosition;
                }
            }
            //Remove the exit door
            Object.Destroy(door1);
        }

        //If the max number of rooms hasn't been reached...
        if (GameObject.FindGameObjectsWithTag("room").Length != maxRooms)
        {
            //... create a new room
            newRoom = (GameObject)Instantiate(rooms[Random.Range(0, rooms.Count)],Vector3.zero,Quaternion.Euler(0,0,0));

            //If its side is to the right
            if(newRoomSide == 0)
            {
                //Set a the left door for removal->Store the last room as this one->Set the max rooms->Update the rooms list
                newRoom.GetComponent<DungeonGenrator>().removeDoor = "doorL";
                newRoom.GetComponent<DungeonGenrator>().lastRoom = gameObject;
                newRoom.GetComponent<DungeonGenrator>().maxRooms = maxRooms;
                newRoom.GetComponent<DungeonGenrator>().rooms = rooms;
            }

            //If its side is to the left
            if (newRoomSide == 1)
            {
                //Set a the right door for removal->Store the last room as this one->Set the max rooms->Update the rooms list
                newRoom.GetComponent<DungeonGenrator>().removeDoor = "doorR";
                newRoom.GetComponent<DungeonGenrator>().lastRoom = gameObject;
                newRoom.GetComponent<DungeonGenrator>().maxRooms = maxRooms;
                newRoom.GetComponent<DungeonGenrator>().rooms = rooms;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        //If the new room is usable...
        if (newRoom != null)
        {
            //...and if it's direction is to the right set the position of the new room
            if(newRoomSide == 0)
                newRoom.transform.position =new Vector3(transform.position.x + door1Pos.x - door2Pos.x,transform.position.y + door1Pos.y-door2Pos.y);
            if (newRoomSide == 1)
                newRoom.transform.position = new Vector3(transform.position.x - door2Pos.x, transform.position.y + door1Pos.y - door2Pos.y);
        }
    }
}
