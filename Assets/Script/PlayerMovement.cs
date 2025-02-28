using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // Distance for raycasting
    public float raycastDistance = 1f;

    // Distance moved per step
    private float distance = 1f;

    // Initial move direction
    private Vector3 moveDirection = new Vector3(1, 0, 0);

    // Reference to the Rigidbody component
    private Rigidbody rb;

    #endregion

    #region Unity Lifecycle Methods

    private void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Handle input for movement
        HandleInput();
    }

    #endregion

    #region Movement Logic

    private void MoveBySteps()
    {
        // Calculate new position based on direction and distance
        Vector3 newPosition = this.transform.position + moveDirection * distance;

        // Update the position of the object
        this.transform.position = newPosition;
    }

    private void HandleInput()
    {
        // Check for arrow key presses and update movement direction
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector3.forward; // Move forward (Z+)
            MoveBySteps();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector3.back; // Move backward (Z-)
            MoveBySteps();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector3.left; // Move left (X-)
            MoveBySteps();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector3.right; // Move right (X+)
            MoveBySteps();
        }
    }

    #endregion
}
