using UnityEngine;
using System.Collections;

public class MainCharacterControl : MonoBehaviour {
    CharacterController controller;
    public string direction;
    Vector3 forDirVector;
    public float moveSpeed;
    float initialMoveSpeed;
    public float acceleration;
    public float maxSpeed;
    float right;
    public float decelerationSlowing;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        initialMoveSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	    if(direction == "down")
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

        if (moveSpeed < initialMoveSpeed)
            moveSpeed = initialMoveSpeed;

        if (moveSpeed > maxSpeed)
            moveSpeed = maxSpeed;

        if (direction == "down" || direction == "up")
        {
            if (Input.GetButton("Right") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                right = 1;
            }
            if (Input.GetButton("Left") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                right = -1;
            }
        }

        if (direction == "left" || direction == "right")
        {
            if (Input.GetButton("Up") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                right = 1;
            }
            if (Input.GetButton("Down") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                right = -1;
            }
        }

        if (direction == "down" || direction == "up")
        {
            if (Input.GetButton("Left") == false && Input.GetButton("Right") == false && moveSpeed > initialMoveSpeed)
            {
                moveSpeed -= moveSpeed / decelerationSlowing;
            }
        }

        if (direction == "left" || direction == "right")
        {
            if (Input.GetButton("Up") == false && Input.GetButton("Down") == false && moveSpeed > initialMoveSpeed)
            {
                moveSpeed -= moveSpeed / decelerationSlowing;
            }
        }

        if (moveSpeed > initialMoveSpeed)
            controller.Move((forDirVector * (moveSpeed / 50)) * right);
    }
}
