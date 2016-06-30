using UnityEngine;
using System.Collections;

public class MainCharacterControl : MonoBehaviour {
    //Character controller component
    CharacterController controller;

    //Direction variable
    public string direction;

    //Direction vector
    Vector3 forDirVector;

    //Movement speed
    public float moveSpeed;

    //Minimum move speed(set in editor)
    float initialMoveSpeed;

    //Acceleration speed
    public float acceleration;

    //Maximum speed
    public float maxSpeed;

    //Deceleration length
    public float decelerationSlowing;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        initialMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        #region Direction Vectors
        //These set the direction vectors based on the direction variable
        if (direction == "down")
        {
            forDirVector = transform.right;
        }

        if (direction == "up")
        {
            forDirVector = transform.right;
        }

        if (direction == "left")
        {
            forDirVector = transform.up;
        }

        if (direction == "right")
        {
            forDirVector = transform.up;
        }
        //End of direcion vectors
        #endregion

        //Limit the lowest move speed
        if (moveSpeed < initialMoveSpeed)
            moveSpeed = initialMoveSpeed;

        //Limit the max move speed
        if (moveSpeed > maxSpeed)
            moveSpeed = maxSpeed;

        //Set speed after pressing keys
        if (direction == "down" || direction == "up")
        {
            if (Input.GetButton("Right") == true)
            {
                moveSpeed = moveSpeed * acceleration;
            }
            if (Input.GetButton("Left") == true)
            {
                moveSpeed = moveSpeed * acceleration;
            }
        }

        //Set speed after pressing keys
        if (direction == "left" || direction == "right")
        {
            if (Input.GetButton("Up") == true)
            {
                moveSpeed = moveSpeed * acceleration;
            }
            if (Input.GetButton("Down") == true)
            {
                moveSpeed = moveSpeed * acceleration;
            }
        }

        //Decelrate when not pressing keys
        if (direction == "down" || direction == "up")
        {
            if (Input.GetButton("Left") == false && Input.GetButton("Right") == false && moveSpeed > initialMoveSpeed)
            {
                moveSpeed -= moveSpeed / decelerationSlowing;
            }
        }

        //Decelrate when not pressing keys
        if (direction == "left" || direction == "right")
        {
            if (Input.GetButton("Up") == false && Input.GetButton("Down") == false && moveSpeed > initialMoveSpeed)
            {
                moveSpeed -= moveSpeed / decelerationSlowing;
            }
        }

        //Move character
        if (moveSpeed > initialMoveSpeed)
            controller.Move((forDirVector * (moveSpeed / 50)) * right);
    }
}
