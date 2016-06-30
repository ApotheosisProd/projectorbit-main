using UnityEngine;
using System.Collections;

public class MainCharacterControl : MonoBehaviour {
    //Character controller component
    CharacterController controller;

    //Direction variable
    public string direction;

    //Forward direction vector
    Vector3 forDirVector;

    //Down direction vector
    Vector3 downDirVector;

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

    //Gravity variable
    public float gravity;

    //Initial gravity variable(editor)
    float initialGravity;

    //Movement direction variable
    float movementDir;

    float moving;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        initialMoveSpeed = moveSpeed;
        initialGravity = gravity;
    }
	
	// Update is called once per frame
	void Update () {
        #region Direction Vectors
        //These set the direction vectors based on the direction variable
        if (direction == "down")
        {
            forDirVector = transform.right;
            downDirVector = -transform.up;
        }

        if (direction == "up")
        {
            forDirVector = transform.right;
            downDirVector = transform.up;
        }

        if (direction == "left")
        {
            forDirVector = transform.up;
            downDirVector = -transform.right;
        }

        if (direction == "right")
        {
            forDirVector = transform.up;
            downDirVector = transform.right;
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
                movementDir = 1;
            }
            if (Input.GetButton("Left") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = -1;
            }
        }

        //Set speed after pressing keys
        if (direction == "left" || direction == "right")
        {
            if (Input.GetButton("Up") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = 1;
            }
            if (Input.GetButton("Down") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = -1;
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
        controller.Move( (((forDirVector * (moveSpeed / 50)) * movementDir)*moving) + (downDirVector*gravity)/3);

        if (moveSpeed > initialMoveSpeed)
        {
            moving = 1;
        }
        else
        {
            moving = 0;
        }

        if (controller.isGrounded == true)
        {
            gravity = initialGravity;
        }
        else
        {
            gravity = gravity * 1.1f;
            if (gravity > 30)
                gravity = 30;
        }
    }
}
