using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonGenrator : MonoBehaviour {
    public List<GameObject> rooms = new List<GameObject>();
    public int maxRooms;
    public int newRoomSide;
    GameObject door1;
    public string removeDoor;
    Vector3 newRoomPos;
    public Vector3 door2Pos;
    public Vector3 door1Pos;
    GameObject newRoom;
    GameObject lastRoom;
	// Use this for initialization
	void Start () {

        if (removeDoor != null || removeDoor != "")
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == removeDoor)
                    lastRoom.GetComponent<DungeonGenrator>().door2Pos = child.localPosition;
            }
        }

        if (GameObject.FindGameObjectsWithTag("room").Length != maxRooms)
        {
            newRoomSide = Random.Range(0, 1);
        }
        else
        {
            newRoomSide = -1;
        }
        if (removeDoor != null)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == removeDoor)
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }
        if (newRoomSide == 0)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == "doorR")
                {
                    door1 = child.gameObject;
                    door1Pos = child.localPosition;
                }
            }
            newRoomPos = door1.transform.position - new Vector3 (0,door1.transform.position.y) + door2Pos;
            Object.Destroy(door1);
        }
        if (GameObject.FindGameObjectsWithTag("room").Length != maxRooms)
        {
            newRoom = (GameObject)Instantiate(rooms[Random.Range(0, rooms.Count)],Vector3.zero,Quaternion.Euler(0,0,0));
            if(newRoomSide == 0)
            {
                newRoom.GetComponent<DungeonGenrator>().removeDoor = "doorL";
                newRoom.GetComponent<DungeonGenrator>().lastRoom = gameObject;
                newRoom.GetComponent<DungeonGenrator>().maxRooms = maxRooms;
                newRoom.GetComponent<DungeonGenrator>().rooms = rooms;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (newRoom != null)
        {
                newRoom.transform.position =new Vector3(transform.position.x + door1Pos.x - door2Pos.x,transform.position.y + door1Pos.y-door2Pos.y);
        }
    }
}
