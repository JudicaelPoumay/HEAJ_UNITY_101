using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveHeight = 5f; // Height to move up
    public float moveSpeed = 2f; // Speed of movement

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;

    void Start()
    {
        // Store the original position
        originalPosition = transform.position;
        // Calculate the target position
        targetPosition = new Vector3(originalPosition.x, originalPosition.y + moveHeight, originalPosition.z);
    }

    void Update()
    {
        if (movingUp)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // Check if reached the target position
            if (transform.position == targetPosition)
            {
                movingUp = false;
            }
        }
        else
        {
            // Move back towards the original position
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            
            // Check if returned to the original position
            if (transform.position == originalPosition)
            {
                movingUp = true;
            }
        }
    }
}
