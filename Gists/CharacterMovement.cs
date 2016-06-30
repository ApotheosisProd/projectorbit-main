using UnityEngine;
using System.Collections;

public class MainCharacterControl : MonoBehaviour {
    //Animator component
    Animator animator;

    //Sprite component
    SpriteRenderer sprite;

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

    //Moving float
    float moving;

    //Jumping boolean
    bool jump;

    //Character rotation
    float rotation;

    //Grounded boolean
    bool ground;

    //Timer to allow jumping
    float jumpTimer;

    //Rotate Euler
    Quaternion rotationEuler;

    //Maximum Gravity
    public float maxGravity;

    //Jump height
    public float jumpHeight;

    //Whether or not character can shift gravity
    bool canShift = true;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        initialMoveSpeed = moveSpeed;
        initialGravity = gravity;
        sprite = GetComponent<SpriteRenderer>();
    }
	
    void Update()
    {
        #region Jump Input
        //Jump if grounded and pressing space
        if (ground == true)
        {
            if (Input.GetButtonDown("Jump") == true)
            {
                jump = true;
                gravity = -jumpHeight;
                ground = false;
                jumpTimer += 1;
            }
        }


        if (canShift == true)
        {
            if (Input.GetAxis("Flip Horizontal") >= 0.8 || Input.GetButton("Flip Right Button") == true)
            {
                direction = "right";
                canShift = false;
            }

            if (Input.GetAxis("Flip Horizontal") <= -0.8 || Input.GetButton("Flip Left Button") == true)
            {
                direction = "left";
                canShift = false;
            }

            if (Input.GetAxis("Flip Vertical") <= -0.8 || Input.GetButton("Flip Up Button") == true)
            {
                direction = "up";
                canShift = false;
            }

            if (Input.GetAxis("Flip Vertical") >= 0.8 || Input.GetButton("Flip Down Button") == true)
            {
                direction = "down";
                canShift = false;
            }
        }
        #endregion
    }

	// Update is called once per frame
	void FixedUpdate () {
        #region Check if Grounded
        //Raycast down to see if character is grounded
        if (Physics.Raycast(transform.position, downDirVector, 3f))
        {
            ground = true;
            canShift = true;
        }
        else
        {
            ground = false;
        }
        #endregion

        #region Direction Vectors
        //These set the direction vectors based on the direction variable
        if (direction == "down")
        {
            forDirVector = transform.right;
            downDirVector = -transform.up;
            rotation = 0;
        }

        if (direction == "up")
        {
            forDirVector = -transform.right;
            downDirVector = -transform.up;
            rotation = 180;
        }

        if (direction == "left")
        {
            forDirVector = -transform.right;
            downDirVector = -transform.up;
            rotation = -90;
        }

        if (direction == "right")
        {
            forDirVector = transform.right;
            downDirVector = -transform.up;
            rotation = 90;
        }

        //Smoothly rotate to new direction
        rotationEuler = Quaternion.Euler(0, 0, rotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationEuler,0.1f);
        //End of direcion vectors
        #endregion

        #region Speed Limits
        //Limit the lowest move speed
        if (moveSpeed < initialMoveSpeed)
            moveSpeed = initialMoveSpeed;

        //Limit the max move speed
        if (moveSpeed > maxSpeed)
            moveSpeed = maxSpeed;
        #endregion

        #region Speed from Input
        //Set speed after pressing keys
        if (direction == "down" || direction == "up")
        {
            if (Input.GetAxis("Horizontal") > 0.2 || Input.GetButton("Right") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = 1;
                if (direction == "down")
                    sprite.flipX = false;
                if (direction == "up")
                    sprite.flipX = true;
                animator.SetBool("Run", true);
            }
            if (Input.GetAxis("Horizontal") < -0.2 || Input.GetButton("Left") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = -1;
                if (direction == "down")
                    sprite.flipX = true;
                if (direction == "up")
                    sprite.flipX = false;
                animator.SetBool("Run", true);
            }
        }

        //Set speed after pressing keys
        if (direction == "left" || direction == "right")
        {
            if (Input.GetAxis("Vertical") < -0.1 || Input.GetButton("Up") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = 1;
                if (direction == "left")
                    sprite.flipX = true;
                if (direction == "right")
                    sprite.flipX = false;
                animator.SetBool("Run", true);
            }
            if (Input.GetAxis("Vertical") > 0.1 || Input.GetButton("Down") == true)
            {
                moveSpeed = moveSpeed * acceleration;
                movementDir = -1;
                if (direction == "left")
                    sprite.flipX = false;
                if (direction == "right")
                    sprite.flipX = true;
                animator.SetBool("Run", true);
            }
        }

        //Decelrate when not pressing keys
        if (direction == "down" || direction == "up")
        {
            if (Input.GetAxis("Horizontal") < 0.1 && Input.GetAxis("Horizontal") > -0.1 &&  Input.GetButton("Right") == false && Input.GetButton("Left") == false && moveSpeed > initialMoveSpeed)
            {
                moveSpeed -= moveSpeed / decelerationSlowing;
                animator.SetBool("Run", false);
            }
        }

        //Decelrate when not pressing keys
        if (direction == "left" || direction == "right")
        {
            if (Input.GetAxis("Vertical") < 0.1 && Input.GetAxis("Vertical") > -0.1 && Input.GetButton("Up") == false && Input.GetButton("Down") == false && moveSpeed > initialMoveSpeed)
            {
                moveSpeed -= moveSpeed / decelerationSlowing;
                animator.SetBool("Run", false);
            }
        }
        #endregion

        #region Move Character
        //Move character
        controller.Move( (((forDirVector * (moveSpeed / 50)) * movementDir)*moving) + ((downDirVector*gravity)/3));

        //Set whether or not to move
        if (moveSpeed > initialMoveSpeed)
        {
            moving = 1;
        }
        else
        {
            moving = 0;
        }
        #endregion

        #region Gravity
        if (ground == true && jumpTimer == 0)
        {
            // Reset gravity when grounded
            gravity = initialGravity;
            jump = false;
        }
        else
        {
            //Set gravity and scale exponentially if not jumping
            if (jump == false)
            {
                gravity = gravity * 1.1f;
                if (gravity > maxGravity)
                    gravity = maxGravity;
            }
        }
        #endregion

        #region Jump Physics

        //Change gravity if jumping
        if (jump == true){
            gravity += 0.5f;
            if (gravity > maxGravity)
                gravity = maxGravity;
        }

        //Move down once gravity is 0
        if (gravity < 0)
        {
            jump = true;
            ground = false;
        }

        //Start the jump timer
        if (jump == true)
        {
            jumpTimer += 1;
            animator.SetBool("Jump", true);
        }

        //Reset the jump timer
        if (jumpTimer > 11)
        {
            jumpTimer = 0;
            jump = false;
            animator.SetBool("Jump", false);
        }
        #endregion

    }
}
