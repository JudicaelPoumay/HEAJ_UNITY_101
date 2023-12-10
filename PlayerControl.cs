using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;
    public float rotationSpeed = 5.0f;

    //to keep our rigid body
    Rigidbody rb;
    //to keep the collider object
    Collider coll;
    //flag to keep track of whether a jump started
    bool pressedJump = false;
    // Use this for initialization
    void Start () {
        //get the rigid body component for later use
        rb = GetComponent<Rigidbody>();
        //get the player collider
        coll = GetComponent<Collider>();
    }
  
    // Update is called once per frame
    void FixedUpdate ()
    {
        // Handle player walking
        WalkHandler();
        //Handle player jumping
        JumpHandler();

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, mouseX);
    }
    // Make the player walk according to user input
    void WalkHandler()
    {
        // Set x and z velocities to zero
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        // Distance ( speed = distance / time --> distance = speed * time)
        float distance = walkSpeed * Time.deltaTime;
        // Input on x ("Horizontal")
        float hAxis = Input.GetAxis("Horizontal");
        // Input on z ("Vertical")
        float vAxis = Input.GetAxis("Vertical");
        // Get camera's forward and right vectors
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        // Movement vector
        Vector3 movement = hAxis * right * distance +  vAxis * forward * distance;
        // Current position
        Vector3 currPosition = transform.position;
        // New position
        Vector3 newPosition = currPosition + movement;
        // Move the rigid body
        rb.MovePosition(newPosition);
    }
    // Check whether the player can jump and make it jump
    void JumpHandler()
    {
        // Jump axis
        float jAxis = Input.GetAxis("Jump");
        // Is grounded
        bool isGrounded = CheckGrounded();
        // Check if the player is pressing the jump key
        if (jAxis > 0f)
        {
            // Make sure we've not already jumped on this key press
            if(!pressedJump && isGrounded)
            {
                // We are jumping on the current key press
                pressedJump = true;
                // Jumping vector
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
                // Make the player jump by adding velocity
                rb.velocity = rb.velocity + jumpVector;
            }            
        }
        else
        {
            // Update flag so it can jump again if we press the jump key
            pressedJump = false;
        }
    }
    // Check if the object is grounded
    bool CheckGrounded()
    {
        
        // If any corner is grounded, the object is grounded
        float height = 10f;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            height = renderer.bounds.size.y;
            Debug.Log("Height of the GameObject is: " + height);
        }
        return Physics.Raycast(transform.position, new Vector3(0, -1, 0), height);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if we ran into a coin
        if (collider.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart current scene
        }
        if (collider.gameObject.tag == "Coin")
        {
            print("Grabbing coin..");

            // Destroy coin
            Destroy(collider.gameObject);
        }
    }
}